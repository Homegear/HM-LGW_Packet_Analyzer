using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace HMLGWPacketAnalyzer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DecryptTCPStream();
        }

        private void txtEncryptedData_TextChanged(object sender, EventArgs e)
        {
            DecryptTCPStream();
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            DecryptTCPStream();
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void DecryptTCPStream()
        {
            try
            {
                txtDecryptedData1.Text = "";
                txtDecryptedData2.Text = "";
                if (txtEncryptedData.Text.Length == 0) return;
                string[] Packets = txtEncryptedData.Text.Split(";".ToCharArray());
                int Peer0Size = 0;
                int Peer1Size = 0;
                List<Packet> PacketsPeer0 = new List<Packet>();
                List<Packet> PacketsPeer1 = new List<Packet>();
                int StartIndex = 0;
                int EndIndex = 0;
                for (int i = 0; i < Packets.Length; i++)
                {
                    StartIndex = Packets[i].IndexOf("{") + 1;
                    EndIndex = Packets[i].IndexOf("}");
                    if (StartIndex < 0 || EndIndex < 0) continue;
                    string Packet = Packets[i].Substring(StartIndex, EndIndex - StartIndex);
                    Packet p = new Packet();
                    p.Data = GetBytearrayFromHex(Packet);
                    p.Index = i;
                    if (Packets[i].StartsWith("\r\nchar peer0_") || Packets[i].StartsWith("char peer0_"))
                    {
                        if (PacketsPeer0.Count() > 0) Peer0Size += p.Data.Length;
                        PacketsPeer0.Add(p);
                    }
                    if (Packets[i].StartsWith("\r\nchar peer1_") || Packets[i].StartsWith("char peer1_"))
                    {
                        if (PacketsPeer1.Count() > 0) Peer1Size += p.Data.Length;
                        PacketsPeer1.Add(p);
                    }
                }

                byte[] Key;
                if (txtKey.Text.Length != 32)
                {
                    MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(txtKey.Text);
                    Key = MD5.ComputeHash(keyBytes, 0, keyBytes.Length);
                }
                else
                {
                    Key = GetBytearrayFromHex(txtKey.Text);
                }
                string Temp = Encoding.UTF8.GetString(PacketsPeer1[0].Data);
                StartIndex = Temp.IndexOf("V");
                Temp = Temp.Substring(StartIndex + 4).Replace("\r\n", "");
                byte[] RemoteIV = GetBytearrayFromHex(Temp);

                Temp = Encoding.UTF8.GetString(PacketsPeer0[0].Data);
                StartIndex = Temp.IndexOf("V");
                Temp = Temp.Substring(StartIndex + 4).Replace("\r\n", "");
                byte[] MyIV = GetBytearrayFromHex(Temp);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.Mode = CipherMode.CFB;
                cipher.Padding = PaddingMode.Zeros;
                cipher.KeySize = 128;
                cipher.IV = MyIV;
                cipher.Key = Key;

                ICryptoTransform remoteMeDecryptor = cipher.CreateDecryptor();

                cipher = new RijndaelManaged();
                cipher.Mode = CipherMode.CFB;
                cipher.Padding = PaddingMode.Zeros;
                cipher.KeySize = 128;
                cipher.IV = RemoteIV;
                cipher.Key = Key;

                ICryptoTransform meRemoteDecryptor = cipher.CreateDecryptor();

                List<Packet> DecryptedPacketsPeer0 = DecryptTCPStream(PacketsPeer0, Peer0Size, meRemoteDecryptor);
                List<Packet> DecryptedPacketsPeer1 = DecryptTCPStream(PacketsPeer1, Peer1Size, remoteMeDecryptor);

                int MaxIndex = Math.Max(DecryptedPacketsPeer0.Last().Index, DecryptedPacketsPeer1.Last().Index);
                int IndexPeer0 = 0;
                int IndexPeer1 = 0;
                for (int i = 2; i < MaxIndex; i++)
                {
                    if (IndexPeer0 < DecryptedPacketsPeer0.Count() && DecryptedPacketsPeer0[IndexPeer0].Index == i)
                    {
                        txtDecryptedData1.Text += "CCU => LGW:\r\n";
                        txtDecryptedData1.Text += GetHexFromBytearray(DecryptedPacketsPeer0[IndexPeer0].Data) + "\r\n";
                        txtDecryptedData2.Text += "CCU => LGW:\r\n";
                        txtDecryptedData2.Text += ASCIIToString(DecryptedPacketsPeer0[IndexPeer0].Data) + "\r\n";
                        IndexPeer0++;
                    }
                    else if (IndexPeer1 < DecryptedPacketsPeer1.Count() && DecryptedPacketsPeer1[IndexPeer1].Index == i)
                    {
                        txtDecryptedData1.Text += "LGW => CCU:\r\n";
                        txtDecryptedData1.Text += GetHexFromBytearray(DecryptedPacketsPeer1[IndexPeer1].Data) + "\r\n";
                        txtDecryptedData2.Text += "LGW => CCU:\r\n";
                        txtDecryptedData2.Text += ASCIIToString(DecryptedPacketsPeer1[IndexPeer1].Data) + "\r\n";
                        IndexPeer1++;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Packet> DecryptTCPStream(List<Packet> Packets, int Size, ICryptoTransform Decryptor)
        {
            List<Packet> DecryptedPackets = new List<Packet>();
            try
            {
                byte[] EncryptedDataPeer = new byte[Size];

                int Pos = 0;
                for (int i = 1; i < Packets.Count(); i++)
                {
                    Array.Copy(Packets[i].Data, 0, EncryptedDataPeer, Pos, Packets[i].Data.Length);
                    Pos += Packets[i].Data.Length;
                }
                byte[] DecryptedData = Decrypt(EncryptedDataPeer, Decryptor);

                Pos = 0;
                for (int i = 1; i < Packets.Count(); i++)
                {
                    Packet p = new Packet();
                    p.Index = Packets[i].Index;
                    p.Data = new byte[Packets[i].Data.Length];
                    Array.Copy(DecryptedData, Pos, p.Data, 0, Packets[i].Data.Length);
                    Pos += Packets[i].Data.Length;
                    DecryptedPackets.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return DecryptedPackets;
        }

        private byte[] Decrypt(byte[] EncryptedPacket, ICryptoTransform Decryptor)
        {
            try
            {
                if (EncryptedPacket == null) return new byte[16];
                byte[] PaddedPacket;
                if(EncryptedPacket.Length % 16 != 0)
                {
                    PaddedPacket = new Byte[((EncryptedPacket.Length / 16) * 16) + 16];
                }
                else
                {
                    PaddedPacket = new Byte[EncryptedPacket.Length];
                }
                Array.Copy(EncryptedPacket, PaddedPacket, EncryptedPacket.Length);

                byte[] Output = new byte[PaddedPacket.Length];
                for (int i = 0; i < PaddedPacket.Length; i += 16)
                {
                    Decryptor.TransformBlock(PaddedPacket, i, 16, Output, i);
                }

                byte[] UnpaddedOutput = new Byte[EncryptedPacket.Length];
                Array.Copy(Output, UnpaddedOutput, UnpaddedOutput.Length);

                return UnpaddedOutput;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new byte[16];
        }

        private byte[] GetBytearrayFromHex(String Hex)
        {
            Hex = Hex.Replace("0x", "").Replace(",", "").Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
            if (Hex.Length == 0 || Hex.Length % 2 != 0) return null;
            byte[] Array = new byte[Hex.Length / 2];
            for (int i = 0; i < Hex.Length / 2; i++)
            {
                try
                {
                    Array[i] = Convert.ToByte(Hex.Substring(i * 2, 2), 16);
                }
                catch
                {
                    txtDecryptedData1.Text = "String is not a valid hexadecimal number: " + Hex;
                }
            }
            return Array;
        }

        private String GetHexFromBytearray(byte[] Bytearray)
        {
            return GetHexFromBytearray(Bytearray, false);
        }

        private String GetHexFromBytearray(byte[] Bytearray, bool SpaceBetweenBytes)
        {
            String Hex = "";
            for (int i = 0; i < Bytearray.Length; i++)
            {
                Hex += String.Format("{0:x2}", Bytearray[i]);
                if (SpaceBetweenBytes && i < Bytearray.Length - 1) Hex += " ";
            }
            return Hex;
        }

        String ASCIIToString(byte[] Bytes)
        {
            StringBuilder sb = new StringBuilder(Bytes.Length);
            foreach (char c in Bytes.Select(b => (char)b))
            {
                if (c < '\u0020' || c >= '\u007F') { sb.Append("."); }
                else { sb.Append(c); }
            }
            return sb.ToString();
        }
    }
}
