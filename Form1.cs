using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using System.Diagnostics;

namespace TimeMuka
{
 
    public partial class Form1 : Form
    {

        public static DateTime dt = DateTime.Now;
        public static DateTime nextYear = new DateTime(DateTime.Now.Year + 1, 1, 1);

        public static TimeSpan ts = nextYear - DateTime.Now;
        public static int firstDayOfYear = (int)new DateTime(dt.Year, 1, 1).DayOfWeek;
        public static int weekNumber = (dt.DayOfYear + firstDayOfYear) / 7 - 1;

        public static DateTime date = DateTime.Today;
        public static int daysInYear = DateTime.IsLeapYear(date.Year) ? 366 : 365;

        public static string path = Directory.GetCurrentDirectory();
        public static string SFname = "/Settings.ini";
        public static string curFile = path + SFname;

        public static IniFile ini = new IniFile(curFile);
        public static int locx = 0;
        public static int locy = 0;

        public Form1(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0) { Console.WriteLine("test");  } // agrumenti
 

            if (!File.Exists(curFile))
            {
                
                ini.IniWriteValue("FormMuka", "Visible", "1");
                ini.IniWriteValue("Koordinat", "X", "1");
                ini.IniWriteValue("Koordinat", "Y", "1");

                for (int i = 0; i < 5; i++)
                {
                    ini.IniWriteValue("Muka", "Tip" + i, "tip");
                    ini.IniWriteValue("Muka", "srok" + i, "123456 12.11.2023 12.5");
                }
                
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            notifyIcon1.Text = Convert.ToString(dt.DayOfYear) + "/" + Convert.ToString(daysInYear) + "(" + Convert.ToString(weekNumber + 1) + ")";

            bool formv = ini.IniReadValue("FormMuka", "Visible") == "1" ? true : false;
            мукаToolStripMenuItem.Checked = formv;
            //bool _status = status == "1" ? true : false;
            // formv = Convert.ToBoolean(ini.IniReadValue("FormMuka", "Visible"));

            if (formv != true)
            {
                this.WindowState = FormWindowState.Minimized;
                this.TopMost = false;
                this.Hide();
            }


            label1.Text = ini.IniReadValue("Muka", "Tip0");
            label2.Text = ini.IniReadValue("Muka", "Tip1");
            label3.Text = ini.IniReadValue("Muka", "Tip2");
            label4.Text = ini.IniReadValue("Muka", "Tip3");
            label5.Text = ini.IniReadValue("Muka", "Tip4");

            textBox1.Text = ini.IniReadValue("Muka", "srok0");
            textBox2.Text = ini.IniReadValue("Muka", "srok1");
            textBox3.Text = ini.IniReadValue("Muka", "srok2");
            textBox4.Text = ini.IniReadValue("Muka", "srok3");
            textBox5.Text = ini.IniReadValue("Muka", "srok4");

            locx = Convert.ToInt32(ini.IniReadValue("Koordinat", "X"));
            locy = Convert.ToInt32(ini.IniReadValue("Koordinat", "Y"));
            this.Location = new Point(locx, locy);

 

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void мукаToolStripMenuItem_Click(object sender, EventArgs e)
        {

            мукаToolStripMenuItem.Checked = !мукаToolStripMenuItem.Checked;

            if (мукаToolStripMenuItem.Checked == true)
            {
                ini.IniWriteValue("FormMuka", "Visible", "1");
                this.Show();
            }
            else
            { 
                ini.IniWriteValue("FormMuka", "Visible", "0");
                this.Hide();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ini.IniWriteValue("Muka", "srok0", textBox1.Text);
            ini.IniWriteValue("Muka", "srok1", textBox2.Text);
            ini.IniWriteValue("Muka", "srok2", textBox3.Text);
            ini.IniWriteValue("Muka", "srok3", textBox4.Text);
            ini.IniWriteValue("Muka", "srok4", textBox5.Text);
            MessageBox.Show("Ok");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ini.IniWriteValue("Koordinat", "X", Convert.ToString(this.Left));
            ini.IniWriteValue("Koordinat", "Y", Convert.ToString(this.Top));
            
        }
    }
}
