using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace PlayersDatabase
{
    public class CRUD
    {

        //1. Insert method
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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        } //End Insert Method

        //2. DELETE method
        public bool Delete(SqlConnection conn, int id)
        {
            try
            {
                conn.Open();

                string deleteString = "DELETE FROM Players WHERE Id = @id";

                //Open Connection
                //Associate the Reader with the SQL Command
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                //Scalar
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = id;

                cmd.ExecuteNonQuery();
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

      // 3. Calculate Mean Distance Method
        public double CalcMeanDistance(SqlConnection conn)
        {
            // Data Reader
            SqlDataReader rdr = null;

            //Try Block To Read from Database
            try
            {
                //Open Connection
                conn.Open();

                //Create a SQL Command
                SqlCommand command = new SqlCommand("SELECT RunningDistance FROM Players", conn);

                //Associate the Reader with the SQL Command
                rdr = command.ExecuteReader();

                //Int to Hold Result
                int result = 0;
                int playerAmount = 0;
                //While There is Data to be read
                while (rdr.Read())
                {
                    result += (int)rdr[0];
                    playerAmount++;
                }

                double finalResult = (result / playerAmount);
                conn.Close();
                return finalResult;

            }

            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();

                }
            }

        }// end Calculate Mean Distance Method


        // 4. Calculate Min Distance Method
        public int CalcMinDistance(SqlConnection conn)

        {

            //Try Block To Read from Database
            try
            {
                //Open Connection
                conn.Open();

                //Create a DataTable to perform the Min function
                var table = new DataTable();
                using (var adapt = new SqlDataAdapter("SELECT * FROM Players", conn))
                {
                    //Fill the Datatable with the Players DB
                    adapt.Fill(table);
                }

                int minDistance = Convert.ToInt32(table.Compute("min(RunningDistance)", string.Empty));

                return minDistance;

            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();

                }
            }

        }// end Calculate Min Distance Method

        // 5. Calculate  Max Distance Method
        public int CalcMaxDistance(SqlConnection conn)

        {

            //Try Block To Read from Database
            try
            {
                //Open Connection
                conn.Open();

                //Create a DataTable to perform the Min function
                var table = new DataTable();
                using (var adapt = new SqlDataAdapter("SELECT * FROM Players", conn))
                {
                    //Fill the Datatable with the Players DB
                    adapt.Fill(table);
                }

                int maxDistance = Convert.ToInt32(table.Compute("max(RunningDistance)", string.Empty));

                return maxDistance;

            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();

                }
            }

        }// end Calculate Max Distance Method

        // 6. Calculate Mean Speed Method
      public double CalcMeanSpeed(SqlConnection conn)
        {
            // Data Reader
            SqlDataReader rdr = null;
            //Try Block To Read from Database
            try
            {
                //Open Connection
                conn.Open();

                //Create a SQL Command
                SqlCommand command = new SqlCommand("SELECT MaxSpeed FROM Players", conn);

                //Associate the Reader with the SQL Command
                rdr = command.ExecuteReader();

                //Int to Hold Result
                double result = 0;
                double playerAmount = 0;
                //While There is Data to be read
                while (rdr.Read())
                {
                    result += (double)rdr[0];
                    playerAmount++;
                }

                double finalResult = (result / playerAmount);

                return finalResult;

            }

            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();

                }
            }

        }// end Calculate Mean Speed Method

        // 7. Calculate Min Speed Method
        public  double CalcMinSpeed(SqlConnection conn)
        {

            //Try Block To Read from Database
            try
            {
                //Open Connection
                conn.Open();

                //Create a DataTable to perform the Min function
                var table = new DataTable();
                using (var adapt = new SqlDataAdapter("SELECT * FROM Players", conn))
                {
                    //Fill the Datatable with the Players DB
                    adapt.Fill(table);
                }
                //Calculate Min Speed
                double minSpeed = Convert.ToDouble(table.Compute("min(MaxSpeed)", string.Empty));

                return minSpeed;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();

                }
            }

        }// end Calculate Min Speed Method

        // 8. Calculate Max Speed Method
        public double CalcMaxSpeed(SqlConnection conn)

        {
            //Try Block To Read from Database
            try
            {
                //Open Connection
                conn.Open();

                //Create a DataTable to perform the Min function
                var table = new DataTable();
                using (var adapt = new SqlDataAdapter("SELECT * FROM Players", conn))
                {
                    //Fill the Datatable with the Players DB
                    adapt.Fill(table);
                }
                //Calculate Max Spepd
                double maxSpeed = Convert.ToDouble(table.Compute("max(MaxSpeed)", string.Empty));
                return maxSpeed;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();

                }
            }

        }// end Calculate Max Speed Method 
    } 
}