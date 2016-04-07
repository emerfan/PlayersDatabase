﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

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


        public bool Update(SqlConnection conn, int id, string name, string lname, DateTime age, decimal height, decimal distance, string speed)
        {
            //Try Block To Insert Into Database
            try
            {
                //Cast Max Speed Text Box Value to Double
                double maxSpeed = Convert.ToDouble(speed);

                //create a new Insert String 
                conn.Open();


                string InsertString = @"Update Players 
                                        SET First_Name = @fname, Last_Name = @lname, 
                                        Age = @age, Height = @height,
                                        RunningDistance = @distance, MaxSpeed = @speed 
                                        WHERE Id = @id";


                //Open Connection
                //Associate the Reader with the SQL Command
                SqlCommand cmd = new SqlCommand(InsertString, conn);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@distance", distance);
                cmd.Parameters.AddWithValue("@speed", speed);

                //Execute Query
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

        } // End Update Method
    }
}
