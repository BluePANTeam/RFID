using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private Form1 panza;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 f)
        {
            panza = f;
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private string Nm;

        public string Passvalue
        {
            get { return Nm; }
            set { Nm = value; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PassNameLbl.Text = "Please input by Barcode scanner";
            label1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
         

                if (e.KeyCode == Keys.Enter)
                {
                    int number;
                    if (textBox1.TextLength != 8 || !Int32.TryParse(textBox1.Text, out number))
                    {
                       MessageBox.Show("กรอกเฉพาะตัวเลขจำนวน 8 ตัวเท่านั้น ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                    else 
                    {
                        textBox1.Enabled = false;
                        textBox1.Visible = false;

                        panza.getLabel1().Text = textBox1.Text;
                        this.Close();

                   }
                       
                }
            
        }

        
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
