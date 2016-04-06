using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PlayersDatabase
{
    class CRUD
    {

        //UPDATE method
        public bool Insert(SqlConnection conn, string name, string lname, DateTime age, decimal height, decimal distance, string speed)
        {
            //Try to Update, if so return true to the form so it can clear the inputs and show success message
            try
            {
                //Cast Max Speed Text Box Value to Double
                double maxSpeed = Convert.ToDouble(speed);

                // Open Connection
                conn.Open();

                //SQL Command With Values From Form
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Players (First_Name, Last_Name, Age, Height, RunningDistance, MaxSpeed) 
                                              VALUES(' " + name + " ',' " + lname + " ',' "
                                                  + age + "',' " + height + "',' " + distance + "',' " + maxSpeed + "')");

                //Associate Command With Connection
                cmd.Connection = conn;

                //Execute the Command
                cmd.ExecuteNonQuery();

                //Close the Connection
                conn.Close();
                return true;
            }

            //If there is an error, return false so that an error message can be displayed
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }


        } //End Update Method
    }
}
