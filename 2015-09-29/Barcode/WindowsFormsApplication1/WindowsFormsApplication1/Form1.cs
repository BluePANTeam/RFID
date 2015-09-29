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
    public partial class Form1 : Form
    {
 

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text.ToString();

            Form2 f2 = new Form2(this);
            f2.Passvalue = textBox1.Text;
            f2.ShowDialog();

        }
        public Label getLabel1()
        {
            return label1;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // label1.Text = textBox1.Text.ToString();
          
        }

       /* private void TextBox1_OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("You have entered the correct key.");
            }


        }*/
        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
   
            if (e.KeyCode == Keys.Enter) 
            {
                //MessageBox.Show("You have entered the correct key.");
                // label1.Text = textBox1.Text.ToString();
                textBox1.Enabled = false;
                textBox1.Visible = false;

                Form2 f2 = new Form2();
                f2.Passvalue = textBox1.Text;
                f2.ShowDialog();
            }
        }
       
        private void label1_Keydown(object sender, EventArgs e)
        {
            textBox1.Text = label1.Text.ToString();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label2.Text = "Welcome :)\n" + "Please click on new button";
          //  MessageBox.Show("Please input barcode scanner.");
            label3.Text = "Student ID :";

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime theDate;
            theDate = DateTime.Now;

            label4.Text = theDate.ToString();
            //MessageBox.Show(theDate.ToString());
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    
    }
}
