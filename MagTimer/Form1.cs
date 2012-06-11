using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MagTimer
{
    public partial class Form1 : Form
    {
        DateTime from;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Enabled = false;
            st_stp_btn.Enabled = false;
            from = new DateTime(1,1,1);
            label1.Text = string.Format("{0:D2}:{1:D2}", from.Minute, from.Second);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.MinValue < from)
            {
                from = from.AddMilliseconds(-timer1.Interval);
                double minsec = from.Minute + from.Second / 60.0;
                label1.Text = string.Format("{0:D2}:{1:D2}", from.Minute, from.Second);
                double current = minsec / 0.4;
                progressBar1.Value = (int)current;
                textBox2.Text = current.ToString("###.#");
            }
            else
            {
                timer1.Enabled = false;
                st_stp_btn.Text = "Start";
                st_stp_btn.Enabled = false;
            }
        }

        private void st_stp_btn_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                // run -> stop
                timer1.Enabled = false;
                st_stp_btn.Text = "Start";
            }
            else
            {
                // stop -> run
                timer1.Enabled = true;
                st_stp_btn.Text = "Stop";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // only number, but character(like aaa or 123e) to negative number -1
            int per;
            if (!int.TryParse(textBox1.Text, out per)) per = -1;

            // %(0 ~ 100) -> time (~ 40:00)
            if (per >= 0 && per <= 100)
            {
                progressBar1.Value = per;
                textBox2.Text = per.ToString("###.#"); // XXX
                double m = 0.4 * per; // 100(%):40(min) = 1:0.4 => 0.4(min/%)
                from = new DateTime(1, 1, 1, 0, (int)m, (int)((m - (int)m) * 60), 0);
                label1.Text = string.Format("{0:D2}:{1:D2}", from.Minute, from.Second);
                st_stp_btn.Enabled = true;
            }
        }
    }
}
