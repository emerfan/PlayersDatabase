using System;
using System.Data;
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

        //SqlDataAdapter
        SqlDataAdapter dataAdapter;

        //SqlCommandBuilder
        SqlCommandBuilder com;

        //Create a new connection to the database
        SqlConnection conn = new SqlConnection("Data Source=lugh4.it.nuigalway.ie;Persist Security Info=True;User ID=msdb2367;Password=msdb2367EM");
        public Form1()
        {
            InitializeComponent();
            //Load the datagrid
            loadData();

        }

        private void insertPlayer_Click(object sender, EventArgs e)
        {
            //boolean to get return value from insert method
            bool isUpdated;

            // Call CRUD update method, assign true or false depending on whether update is successful
            isUpdated = crud.Insert(conn, textBox1.Text, textBox2.Text, dateTimePicker1.Value, numericUpDown2.Value, numericUpDown1.Value, textBox3.Text);

            //if insert is successful
            if (isUpdated)
            {
                //Alert Success
                MessageBox.Show("Player Successfully Inserted.");
                //Load datagrid with new data
                loadData();
                //Reset the Form Values
                Utilities.ResetAllControls(this);
            }
            //If insert is not successful
            else
            {
                MessageBox.Show("Could Not Insert Player.");
                //Reset the Form Values
                Utilities.ResetAllControls(this);
            }
            
        }

        //Update Method Using SqlDataAdapter, allows user to directly edit the table and update
        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Update Command
                com.GetUpdateCommand();
                //Update DataSet, Players Table
                dataAdapter.Update(ds, "Players");
                //Confirm Update
                MessageBox.Show("Records Successfully Updated");
            }

            catch(Exception err)
            {

                MessageBox.Show("Could not update record:", err.ToString());
            }
        }



        //Delete Records Method Calls the CRUD delete method
        private void delete_Click(object sender, EventArgs e)
        {
            //Prompt user to confirm that they want to delete the records
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete? This cannot be undone.", "Permanent Delete Warning", MessageBoxButtons.YesNo);
            //If Yes
            if (dialogResult == DialogResult.Yes)
            {
                //Delete the Records
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    //Get The Selected Players ID
                    int getID = Convert.ToInt32(row.Cells[0].Value.ToString());
                    //Pass to the CRUD delete method with conn and ID
                    crud.Delete(conn, getID);

                    //Reload the datatable with updated records
                    loadData();
                    
                }
            }

        } // End delete_Click






        //Method to load the data into the datagridview
         private void loadData()
         {
             //Select everything in the players db
             string select = @"SELECT Id as 'Player ID' , First_Name as 'First Name', 
                                Last_Name as 'Last Name', Age as 'D.O.B.', Height, 
                                RunningDistance as 'Running Distance', MaxSpeed as 'Max Speed' FROM Players";

             //Create a new SqlDataAdapter with the select string and the connection
             dataAdapter = new SqlDataAdapter(select, conn);

             //Instantiate Command Builder
             com = new SqlCommandBuilder(dataAdapter); 

             //Instantiate a new DataSet();
             ds = new DataSet();

             //Use the dataAdapter to fill the dataset with the Players table
             dataAdapter.Fill(ds, "Players");

             //Close Connection
             conn.Close();

             //Associate the view with the datasource
             dataGridView1.DataSource = ds;

             //Properties for dataGridView
             dataGridView1.ReadOnly = false;
             dataGridView1.MultiSelect = false;
             dataGridView1.AllowUserToAddRows = false;

             //Table to show
             dataGridView1.DataMember = "Players";

             //Can't change age
             dataGridView1.Columns["D.O.B."].ReadOnly = true;

             //hide ID column
             dataGridView1.Columns[0].Visible = false;

            //Call statistics method if datagrid has data. Prevents errors being thrown from CRUD class
            if(dataGridView1.Rows.Count > 0)
            {
                //Call the method to load statistics
                getStatistics();
            }
         } //End Load Data
       







         //Method to getStatistics from the CRUD class
       private void getStatistics()
         {
             //Calculate Distance Statistics
             double meandistance = crud.CalcMeanDistance(conn);
             int mindistance = crud.CalcMinDistance(conn);
             int maxdistance = crud.CalcMaxDistance(conn);

             //Return to View
             avDistanceAmount.Text = Convert.ToString(meandistance) + " metres";
             maxDistanceAmount.Text = Convert.ToString(maxdistance) + " metres";
             minDistanceAmount.Text = Convert.ToString(mindistance) + " metres";

             //Calculate Speed Statistics
             double meanspeed = crud.CalcMeanSpeed(conn);
             double minspeed = crud.CalcMinSpeed(conn);
             double maxspeed = crud.CalcMaxSpeed(conn);

             //Return to View
             avSpeedAmount.Text = Convert.ToString(meanspeed) + " km/hr";
             maxSpeedAmount.Text = Convert.ToString(maxspeed) + " km/hr";
             minSpeedAmount.Text = Convert.ToString(minspeed) + " km/hr";

         } 

    }
 }
