using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SecuritySender
{
    public partial class Comparison : Form
    {
        public Comparison()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            //----------------------------------------------------------------------------------------------//
            //---------------------------------------------------------------------------------------------//

            string fileDoc1 = @"C:\Users\Abdo\source\repos\SecurityProject\CBC.txt";
            StreamReader reader1 = new StreamReader(fileDoc1);
            string inputLine1;
            int c1 = 0;
            string bs1 = "";
            string t1 = "";
            chart2.Series["Time"].Points.Clear();
            
       
            while ((inputLine1 = reader1.ReadLine()) != null)
            {

                if (c1 == 0)
                {
                    bs1 = inputLine1;
                    c1++;
                }
                else
                {
                    t1 = inputLine1;
                    chart2.Series["Time"].Points.AddXY( bs1, int.Parse(t1));
                    c1 = 0;
                }
            }
            reader1.Close();

            //-----------------------------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------//

            string fileDoc2 = @"C:\Users\Abdo\source\repos\SecurityProject\CFB.txt";
            StreamReader reader2 = new StreamReader(fileDoc2);
            string inputLine2;
            int c2 = 0;
            string bs2 = "";
            string t2 = "";
            chart3.Series["Time"].Points.Clear();

            while ((inputLine2 = reader2.ReadLine()) != null)
            {

                if (c2 == 0)
                {
                    bs2 = inputLine2;
                    c2++;
                }
                else
                {
                    t2 = inputLine2;
                    chart3.Series["Time"].Points.AddXY( bs2, int.Parse(t2));
                    c2 = 0;
                }
            }
            reader2.Close();

            //-------------------------------------------------------------------------------------------------------//
            //------------------------------------------------------------------------------------------------------//

            string fileDoc3 = @"C:\Users\Abdo\source\repos\SecurityProject\OFB.txt";
            StreamReader reader3 = new StreamReader(fileDoc3);
            string inputLine3;
            int c3 = 0;
            string bs3 = "";
            string t3 = "";
            chart4.Series["Time"].Points.Clear();

            while ((inputLine3 = reader3.ReadLine()) != null)
            {

                if (c3 == 0)
                {
                    bs3 = inputLine3;
                    c3++;
                }
                else
                {
                    t3 = inputLine3;
                    chart4.Series["Time"].Points.AddXY( bs3, int.Parse(t3));
                    c3 = 0;
                }
            }
            reader3.Close();

            //--------------------------------------------------------------------------------------------------------//
            //--------------------------------------------------------------------------------------------------------//

            string fileDoc4 = @"C:\Users\Abdo\source\repos\SecurityProject\CTR.txt";
            StreamReader reader4 = new StreamReader(fileDoc4);
            string inputLine4;
            int c4 = 0;
            string bs4 = "";
            string t4 = "";
            chart5.Series["Time"].Points.Clear();

            while ((inputLine4 = reader4.ReadLine()) != null)
            {

                if (c4 == 0)
                {
                    bs4 = inputLine4;
                    c4++;
                }
                else
                {
                    t4 = inputLine4;
                    chart5.Series["Time"].Points.AddXY( bs4, int.Parse(t4));
                    c4 = 0;
                }
            }
            reader4.Close();
        }
    }
}
