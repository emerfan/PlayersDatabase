using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PlayersDatabase
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=lugh4.it.nuigalway.ie;Persist Security Info=True;User ID=msdb2367;Password=msdb2367EM");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double maxSpeed = Convert.ToDouble(textBox3.Text);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Players (First_Name, Last_Name, Age, Height, RunningDistance, MaxSpeed) VALUES(' " + textBox1.Text + " ',' " + textBox2.Text + " ',' " + dateTimePicker1.Value + "',' " + numericUpDown2.Value + "',' " + numericUpDown3.Value + "',' " + maxSpeed + "')");
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
