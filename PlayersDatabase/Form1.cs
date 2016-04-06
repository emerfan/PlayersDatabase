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

        //Instantiate a new DataSet();
        DataSet ds;

        //CRUD class to perform methods
        CRUD crud = new CRUD();

        //Create a new connection to the database
        SqlConnection conn = new SqlConnection("Data Source=lugh4.it.nuigalway.ie;Persist Security Info=True;User ID=msdb2367;Password=msdb2367EM");
        public Form1()
        {
            InitializeComponent();
            //Load the datagrid
            loadData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //boolean to get return value from insert method
            bool isUpdated;

            // Call CRUD update method, assign true or false depending on whether update is successful
            isUpdated = crud.Insert(conn, textBox1.Text, textBox2.Text, dateTimePicker1.Value, numericUpDown2.Value, numericUpDown1.Value, textBox3.Text);

            //if insert is successful
            if (isUpdated)
            {
                //Reset the Form Values
                Utilities.ResetAllControls(this);
                //Alert Success
                MessageBox.Show("Player Successfully Inserted.");
                //Load datagrid with new data
                loadData();
            }
            //If insert is not successful
            else
            {
                MessageBox.Show("Could Not Update Player.");
            }
            
        }



        //Method to load the data into the datagridview
        private void loadData()
        {
            //Select everything in the players db
            string select = "SELECT First_Name as 'First Name', Last_Name as 'Last Name', Age, Height, RunningDistance as 'Running Distance', MaxSpeed as 'Max Speed' FROM Players";

            //Create a new SqlDataAdapter with the select string and the connection
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, conn); //c.con is the connection string


            //SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            //Instantiate a new DataSet();
            ds = new DataSet();
            //Use the dataAdapter to fill the dataset with the Players table
            dataAdapter.Fill(ds, "Players");
            dataGridView1.ReadOnly = true;
            //Associate the view with the datasource
            dataGridView1.DataSource = ds.Tables[0];

            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SqlCommandBuilder sca = new SqlCommandBuilder();
            //sca.GetUpdateCommand(ds);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
