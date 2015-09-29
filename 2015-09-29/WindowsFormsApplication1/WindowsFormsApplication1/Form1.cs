using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]

        static extern void Sleep(int dwMilliseconds);

        [DllImport("MasterRD.dll")]

        static extern int lib_ver(ref uint pVer);

        [DllImport("MasterRD.dll")]

        static extern int rf_init_com(int port, int baud);

        [DllImport("MasterRD.dll")]

        static extern int rf_ClosePort();

        [DllImport("MasterRD.dll")]

        static extern int rf_antenna_sta(short icdev, byte mode);

        [DllImport("MasterRD.dll")]

        static extern int rf_init_type(short icdev, byte type);

        [DllImport("MasterRD.dll")]

        static extern int rf_request(short icdev, byte mode, ref ushort pTagType);

        [DllImport("MasterRD.dll")]

        static extern int rf_anticoll(short icdev, byte bcnt, IntPtr pSnr, ref byte pRLength);

        [DllImport("MasterRD.dll")]

        static extern int rf_select(short icdev, IntPtr pSnr, byte srcLen, ref sbyte Size);

        [DllImport("MasterRD.dll")]

        static extern int rf_halt(short icdev);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_authentication2(short icdev, byte mode, byte secnr, IntPtr key);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_initval(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_increment(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_decrement(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_readval(short icdev, byte adr, ref Int32 pValue);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_read(short icdev, byte adr, IntPtr pData, ref byte pLen);

        [DllImport("MasterRD.dll")]

        static extern int rf_M1_write(short icdev, byte adr, IntPtr pData);

        [DllImport("MasterRD.dll")]

        static extern int rf_beep(short icdev, int msec);

        [DllImport("MasterRD.dll")]

        static extern int rf_light(short icdev, int color);

        bool bConnectedDevice;
        int Check = 0;

        static char[] hexDigits = {'0','1','2','3','4','5','6','7',
                                   '8','9','A','B','C','D','E','F'};

        public Form1()
        {
            InitializeComponent();
        }
        int delay = 1000;
        int port = 4;
        int baud = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (bConnectedDevice)
            {
                DisconnectPort();
            }
            else
            {
                ConnectPort();
               // ReadTagData();
                rf_beep(0, 10);
                rf_beep(0, 0);
                rf_beep(0, 10);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //rf_beep(0, 20);
        }

        public static byte GetHexBitsValue(byte ch)
        {

            byte sz = 0;

            if (ch <= '9' && ch >= '0')

                sz = (byte)(ch - 0x30);

            if (ch <= 'F' && ch >= 'A')

                sz = (byte)(ch - 0x37);

            if (ch <= 'f' && ch >= 'a')

                sz = (byte)(ch - 0x57);

            return sz;

        }

        public static string ToHexString(byte[] bytes)
        {

            char[] chars = new char[bytes.Length * 2];

            for (int i = 0; i < bytes.Length; i++)
            {

                int b = bytes[i];

                chars[i * 2] = hexDigits[b >> 4];

                chars[i * 2 + 1] = hexDigits[b & 0xF];

            }

            return new string(chars);

        }

        public static byte[] ToDigitsBytes(string theHex)
        {

            byte[] bytes = new byte[theHex.Length / 2 + (((theHex.Length % 2) > 0) ? 1 : 0)];

            for (int i = 0; i < bytes.Length; i++)
            {

                char lowbits = theHex[i * 2];

                char highbits;

                if ((i * 2 + 1) < theHex.Length)

                    highbits = theHex[i * 2 + 1];

                else

                    highbits = '0';

                int a = (int)GetHexBitsValue((byte)lowbits);

                int b = (int)GetHexBitsValue((byte)highbits);

                bytes[i] = (byte)((a << 4) + b);

            }

            return bytes;

        }

        public static String byteHEX(Byte ib)
        {
            String _str = String.Empty;
            try
            {
                char[] Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A',
			    'B', 'C', 'D', 'E', 'F' };
                char[] ob = new char[2];
                ob[0] = Digit[(ib >> 4) & 0X0F];
                ob[1] = Digit[ib & 0X0F];
                _str = new String(ob);
            }
            catch (Exception)
            {
                ;
            }
            return _str;

        }

        private void ConnectPort()
        {
            int status;
            status = rf_init_com(3, 9600);

            if (0 == status)
            {
                bConnectedDevice = true;
                button1.Text = "Disconnect";
                label2.Text = "Ready";
                rf_light(0, 2);
              
               // comboBox1.Enabled = false;
              //  comboBox2.Enabled = false;
                //textBox1.Enabled = false;
                timer1.Interval = Convert.ToInt16(1000);
                timer1.Enabled = true;
            }

            else
            {
                bConnectedDevice = false;
                button1.Text = "Connect";
               // rf_light(0, 3);
               
              //  label5.Text = "No Device";
            }

        }
        public void DisconnectPort()
        {
            bConnectedDevice = false;
            button1.Text = "Connect";
            label2.Text = "Not Ready";
            label4.Text = "0000000";
            rf_light(0, 1);
           // comboBox1.Enabled = true;
            //comboBox2.Enabled = true;
            //textBox1.Enabled = true;
           timer1.Enabled = false;
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void re1()
        {

            short icdev = 0x0000;

            int status;

            byte type = (byte)'A';//mifare one

            byte mode = 0x52;

            ushort TagType = 0;

            byte bcnt = 0x04;//mifare มีการใช้บัตร4

            IntPtr pSnr;

            byte len = 255;

            sbyte size = 0;

            if (!bConnectedDevice)
            {
                MessageBox.Show("Not connect to device!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pSnr = Marshal.AllocHGlobal(1024);

            for (int i = 0; i < 2; i++)
            {

                status = rf_antenna_sta(icdev, 0);//เสาอากาศปิด

                if (status != 0)

                    continue;

                Sleep(20);

                status = rf_init_type(icdev, type);

                if (status != 0)

                    continue;

                Sleep(20);

                status = rf_antenna_sta(icdev, 1);//เปิดเสาอากาศ

                if (status != 0)

                    continue;

                Sleep(50);

                status = rf_request(icdev, mode, ref TagType);

                if (status != 0)

                    continue;

                status = rf_anticoll(icdev, bcnt, pSnr, ref len);

                if (status != 0)

                    continue;

                status = rf_select(icdev, pSnr, len, ref size);

                if (status != 0)

                    continue;

                byte[] szBytes = new byte[len];

                for (int j = 0; j < len; j++)
                {
                    szBytes[j] = Marshal.ReadByte(pSnr, j);
                }

                String m_cardNo = String.Empty;

                for (int q = 0; q < len; q++)
                {
                    m_cardNo += byteHEX(szBytes[q]);
                }
                label5.Text = m_cardNo;

                break;

            }

            Marshal.FreeHGlobal(pSnr);
        }

        private void ReadTagData()
        {

            short icdev = 0x0000;

            int status;

            byte mode = 0x60;

            byte secnr = 0x00;

            if (!bConnectedDevice)
            {

                MessageBox.Show("Not connect to device!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            secnr = Convert.ToByte(12); //กำหนดsector

            IntPtr keyBuffer = Marshal.AllocHGlobal(256);
            if (Check == 0)
            {
                byte[] bytesKey = ToDigitsBytes("FFFFFFFFFFFFF"); //กำหนด KEY

                for (int i = 0; i < bytesKey.Length; i++)

                    Marshal.WriteByte(keyBuffer, i * Marshal.SizeOf(typeof(Byte)), bytesKey[i]);

                status = rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);

                Marshal.FreeHGlobal(keyBuffer);

                //TextBox Clear
                // label4.Text = "0000000";
                // tb1.Text = "";
                // tb2.Text = "";
                // tb3.Text = "";

              //  IntPtr dataBuffer = Marshal.AllocHGlobal(256);
            }
            else
            {
                byte[] bytesKey = ToDigitsBytes("AAAAAAAAAAAA"); //กำหนด KEY

                for (int i = 0; i < bytesKey.Length; i++)

                    Marshal.WriteByte(keyBuffer, i * Marshal.SizeOf(typeof(Byte)), bytesKey[i]);

                status = rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);

                Marshal.FreeHGlobal(keyBuffer);

                //TextBox Clear
                // label4.Text = "0000000";
                // tb1.Text = "";
                // tb2.Text = "";
                // tb3.Text = "";
            }
                IntPtr dataBuffer = Marshal.AllocHGlobal(256);
            

           
           

                int j;

                byte cLen = 0;

                status = rf_M1_read(icdev, (byte)((secnr * 4) + 1), dataBuffer, ref cLen);

                if (status != 0 || cLen != 16)
                {

                   label4.Text = "00000000";
                   label5.Text = "xxxxxxxx";
                   rf_light(0, 1);
                  // rf_beep(0, 20);

                   //.Text = "";

                    Marshal.FreeHGlobal(dataBuffer);

                    return;

                }
               // rf_beep(0, 20);
                byte[] bytesData = new byte[4];

                for (j = 0; j < bytesData.Length; j++){

                    bytesData[j] = Marshal.ReadByte(dataBuffer, j);}

            
                    label4.Text = ToHexString(bytesData);
                    label5.Text = "ABCDEFG";

                

            

            Marshal.FreeHGlobal(dataBuffer);
            // rf_beep(0, 20); //เสียง Beep เมื่ออ่านเจอ
             rf_light(0, 2);
        }
   
        private void button2_Click(object sender, EventArgs e)
        {

            short icdev = 0x0000;
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte adr;
            int i;
           // int j;

            if (!bConnectedDevice)
            {
                MessageBox.Show("Not connect to device!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

       
            secnr = Convert.ToByte(12);
            adr = (byte)(Convert.ToByte(1) + secnr * 4);

           // if (cbxSubmass2.SelectedIndex == 3)
          /*  {
                if (DialogResult.Cancel == MessageBox.Show("Be sure to write block3!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    return;
            }*/

            IntPtr keyBuffer = Marshal.AllocHGlobal(1024);
           // mode = 0x60;
            byte[] bytesKey = ToDigitsBytes("FFFFFFFFFFFF");
            for (i = 0; i < bytesKey.Length; i++)
                Marshal.WriteByte(keyBuffer, i, bytesKey[i]);
            status = rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);
            Marshal.FreeHGlobal(keyBuffer);
            if (status != 0)
            {
                label5.Text = "การเขียนผิดพลาด";
                MessageBox.Show("rf_M1_authentication2 failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else { label5.Text = "Success";}
            //
            byte[] bytesBlock;
                    
            
                bytesBlock = ToDigitsBytes(textBox1.Text);
           
           /*     String strCompont = txtKeyA2.Text;
                strCompont += txtKey2.Text;
                strCompont += txtKeyB2.Text;
                bytesBlock = ToDigitsBytes(strCompont);*/
        
            IntPtr dataBuffer = Marshal.AllocHGlobal(1024);

            for (i = 0; i < bytesBlock.Length; i++)
                Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
            status = rf_M1_write(icdev, adr, dataBuffer);
            //เปลี่ยนคีย์

            adr = (byte)(Convert.ToByte(3) + secnr * 4);
            String strCompont = "AAAAAAAAAAAA";
            strCompont += "FF078069";
            strCompont += "FFFFFFFFFFFF";
            bytesBlock = ToDigitsBytes(strCompont);
            for (i = 0; i < bytesBlock.Length; i++)
            {
                Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
            }
            status = rf_M1_write(icdev, adr, dataBuffer);
            Marshal.FreeHGlobal(dataBuffer);

            if (status == 0) { Check = 1; }
          

            //เปลี่ยนคีย์
         //   secnr = Convert.ToByte(12);
          //  adr = (byte)(Convert.ToByte(3) + secnr * 4);

            
          /* bytesKey = ToDigitsBytes("FFFFFFFFFFFF");
            for (i = 0; i < bytesKey.Length; i++)
                Marshal.WriteByte(keyBuffer, i, bytesKey[i]);
            status = rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);
            Marshal.FreeHGlobal(keyBuffer);*/

            /*    String strCompont = "AAAAAAAAAAAA";
                strCompont += "FF0F0069";
                strCompont += "FFFFFFFFFFFF";
                bytesBlock = ToDigitsBytes(strCompont);*/

              /*  for (i = 0; i < bytesBlock.Length; i++)
                Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
                status = rf_M1_write(icdev, adr, dataBuffer);
                Marshal.FreeHGlobal(dataBuffer);*/


           /* bytesBlock = ToDigitsBytes("AAAAAAAAAAAA");
            dataBuffer = Marshal.AllocHGlobal(1024);

            for (i = 0; i < bytesBlock.Length; i++)
                Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
            status = rf_M1_write(icdev, adr, dataBuffer);
            Marshal.FreeHGlobal(dataBuffer);*/
            /*  secnr = Convert.ToByte(13);
              adr = (byte)(Convert.ToByte(3) + secnr * 4);
            bytesBlock = ToDigitsBytes("AAAAAAAAAAAA");

            IntPtr keyABuffer = Marshal.AllocHGlobal(1024);

            for (i = 0; i < bytesBlock.Length; i++)
                Marshal.WriteByte(keyABuffer, i, bytesBlock[i]);
            status = rf_M1_write(icdev, adr, keyABuffer);
            Marshal.FreeHGlobal(keyABuffer);*/


            if (status != 0)
            {
                label5.Text = "การเขียนผิดพลาด";
               // MessageBox.Show("rf_M1_write failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            re1();
            ReadTagData();
           // rf_beep(0, 20);
        }

        private void label4_CausesValidationChanged(object sender, EventArgs e)
        {
          
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {
            if (label4.Text == "00000000") {
                rf_beep(0, 0);
            }

            else rf_beep(0, 40);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            short icdev = 0x0000;
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte adr;
            int i;
            IntPtr dataBuffer = Marshal.AllocHGlobal(1024);
            IntPtr keyBuffer = Marshal.AllocHGlobal(1024);
            byte[] bytesBlock;

            secnr = Convert.ToByte(12);
   
            adr = (byte)(Convert.ToByte(3) + secnr * 4);
            String strCompont = "FFFFFFFFFFFF";
            strCompont += "FF078069";
            strCompont += "FFFFFFFFFFFF";
            bytesBlock = ToDigitsBytes(strCompont);
            for (i = 0; i < bytesBlock.Length; i++)
            {
                Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
            }
            status = rf_M1_write(icdev, adr, dataBuffer);
            Marshal.FreeHGlobal(dataBuffer);
        }

        private void label5_TextChanged(object sender, EventArgs e)
        {
            if (label5.Text == "xxxxxxxx")
            {
                rf_beep(0, 0);
            }

           // else rf_beep(0, 40);
        }
     



    }
}
