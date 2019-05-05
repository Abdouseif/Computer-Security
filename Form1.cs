using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuritySender;
using System.IO;

namespace SecuritySender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex.Equals(0))
            {
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();

            }
            else if (comboBox1.SelectedIndex.Equals(1))
            {
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();

            }
            else if (comboBox1.SelectedIndex.Equals(2))
            {
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();

            }
            else if (comboBox1.SelectedIndex.Equals(3))
            {
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = false;
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();

            }
            else if (comboBox1.SelectedIndex.Equals(4))
            {
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox9.ReadOnly = true;
                textBox4.Clear();
                textBox5.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string plainText = "abc KHALEDSAMEH fghijklmnop1234567";
      
            int blockSize = 8;                                     //n Bits = n/8 characters (Character size=8)
                                                                   //0-7 plain text characters = 12 cipher text characters
                                                                   // 8-15 PT = 24 CT, 16-23 PT = 32 CT
                                                                   //24-31 PT = 44 CT, 32-39 PT = 56 CT
            string initVector = "%aebcxfkbl";
            int CTBlockSize = Program.CT_char_cout(blockSize);
            int shamt = 6;
            int CTRstart = 0, CTRincrement = 1;
            string nonce = "12345678";
            int Tlen = 5;                                         
            int modeOfOperation = 5;
            string ciph = "";
            //-----------------------------------------------------------//
            //-----------------------------------------------------------//

            plainText = textBox1.Text;
             Console.WriteLine("Plain Text:" + plainText);
             blockSize = int.Parse(textBox2.Text);                                   
             modeOfOperation = (comboBox1.SelectedIndex) + 1;
            if (comboBox1.SelectedIndex.Equals(1) || comboBox1.SelectedIndex.Equals(2))
            {
                initVector = textBox4.Text;
            }
            if (comboBox1.SelectedIndex.Equals(2))
            {
                shamt = int.Parse(textBox5.Text);
            }
            if (comboBox1.SelectedIndex.Equals(4))
            {
                CTRstart = int.Parse(textBox7.Text);
                CTRincrement = int.Parse(textBox8.Text);
            }
            if (comboBox1.SelectedIndex.Equals(3))
            {
                nonce = textBox9.Text;
            }
             CTBlockSize = Program.CT_char_cout(blockSize);
             long elapsedMs;
             elapsedMs= Program.block_modes_en(plainText, 5, blockSize, initVector, shamt, CTBlockSize, CTRstart, CTRincrement, nonce, Tlen);
            ciph = Program.getCipher();
            textBox10.Text = ciph;
            Console.WriteLine("Cipher Text:" + ciph);
            if (comboBox1.SelectedIndex.Equals(0))
            {
                Program.SendTimeECB(blockSize.ToString());
                Program.SendTimeECB(elapsedMs.ToString());
                string fileDoc = @"C:\Users\Abdo\source\repos\SecurityProject\ECB.txt";
                StreamReader reader = new StreamReader(fileDoc);
                string inputLine;
                int c = 0;
                string bs = "";
                string t = "";
                chart1.Series["Time"].Points.Clear();

                while ((inputLine = reader.ReadLine()) != null)
                {

                    if (c == 0)
                    {
                        bs = inputLine;
                        c++;
                    }
                    else
                    {
                        t = inputLine;
                        chart1.Series["Time"].Points.AddXY( bs, int.Parse(t));
                        c = 0;
                    }
                }
                reader.Close();
            }
            else if (comboBox1.SelectedIndex.Equals(1))
            {
                Program.SendTimeCBC(blockSize.ToString());
                Program.SendTimeCBC(elapsedMs.ToString());
                string fileDoc = @"C:\Users\Abdo\source\repos\SecurityProject\CBC.txt";
                StreamReader reader = new StreamReader(fileDoc);
                string inputLine;
                int c = 0;
                string bs = "";
                string t = "";
                chart1.Series["Time"].Points.Clear();

                while ((inputLine = reader.ReadLine()) != null)
                {

                    if (c == 0)
                    {
                        bs = inputLine;
                        c++;
                    }
                    else
                    {
                        t = inputLine;
                        chart1.Series["Time"].Points.AddXY( bs, int.Parse(t));
                        c = 0;
                    }
                }
                reader.Close();

            }
            else if (comboBox1.SelectedIndex.Equals(2))
            {
                Program.SendTimeCFB(blockSize.ToString());
                Program.SendTimeCFB(elapsedMs.ToString());
                string fileDoc = @"C:\Users\Abdo\source\repos\SecurityProject\CFB.txt";
                StreamReader reader = new StreamReader(fileDoc);
                string inputLine;
                int c = 0;
                string bs = "";
                string t = "";
                chart1.Series["Time"].Points.Clear();

                while ((inputLine = reader.ReadLine()) != null)
                {

                    if (c == 0)
                    {
                        bs = inputLine;
                        c++;
                    }
                    else
                    {
                        t = inputLine;
                        chart1.Series["Time"].Points.AddXY( bs, int.Parse(t));
                        c = 0;
                    }
                }
                reader.Close();

            }
            else if (comboBox1.SelectedIndex.Equals(3))
            {
                Program.SendTimeOFB(blockSize.ToString());
                Program.SendTimeOFB(elapsedMs.ToString());
                string fileDoc = @"C:\Users\Abdo\source\repos\SecurityProject\OFB.txt";
                StreamReader reader = new StreamReader(fileDoc);
                string inputLine;
                int c = 0;
                string bs = "";
                string t = "";
                chart1.Series["Time"].Points.Clear();

                while ((inputLine = reader.ReadLine()) != null)
                {

                    if (c == 0)
                    {
                        bs = inputLine;
                        c++;
                    }
                    else
                    {
                        t = inputLine;
                        chart1.Series["Time"].Points.AddXY( bs, int.Parse(t));
                        c = 0;
                    }
                }
                reader.Close();

            }
            else if (comboBox1.SelectedIndex.Equals(4))
            {
                Program.SendTimeCTR(blockSize.ToString());
                Program.SendTimeCTR(elapsedMs.ToString());
                 string fileDoc = @"C:\Users\Abdo\source\repos\SecurityProject\CTR.txt";
                            StreamReader reader = new StreamReader(fileDoc);
                string inputLine;
                int c = 0;
                string bs = "";
                string t = "";
                chart1.Series["Time"].Points.Clear();

                while ((inputLine=reader.ReadLine()) != null)
                { 

                  if(c==0)
                    {
                        bs = inputLine;
                        c++;
                    }
                    else
                    {
                        t = inputLine;
                        chart1.Series["Time"].Points.AddXY( bs, int.Parse(t));
                        c = 0;
                    }
                }
                reader.Close();
                
            }
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (!int.TryParse(textBox5.Text, out distance))
            {
                label14.Visible = true;
                textBox5.Text = "";

            }
            else { label14.Visible = false; }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Comparison formC = new Comparison();
            formC.ShowDialog();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (!int.TryParse(textBox2.Text, out distance))
            {
                label8.Visible = true;
                textBox2.Text = "";

            }
            else { label8.Visible = false; }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (!int.TryParse(textBox3.Text, out distance))
            {
                label15.Visible = true;
                textBox3.Text = "";

            }
            else { label15.Visible = false; }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (!int.TryParse(textBox7.Text, out distance))
            {
                label13.Visible = true;
                textBox7.Text = "";

            }
            else { label13.Visible = false; }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            int distance;
            if (!int.TryParse(textBox8.Text, out distance))
            {
                label16.Visible = true;
                textBox8.Text = "";

            }
            else { label16.Visible = false; }
        }
    }
}
