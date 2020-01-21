using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace niccrud.Models
{
    public class customerhandler
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["niccrudEntities"].ToString();
       
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW STUDENT *********************
        public bool AddStudent(customer smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@country", smodel.country);
            cmd.Parameters.AddWithValue("@sex", smodel.sex);
            cmd.Parameters.AddWithValue("@photo", smodel.photo);
           cmd.Parameters.AddWithValue("@EmailID", smodel.EmailID);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW STUDENT DETAILS ********************
        public List<customer> GetStudent()
        {
            connection();
            List<customer> studentlist = new List<customer>();

            SqlCommand cmd = new SqlCommand("GetStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new customer
                    {
                        customerID = Convert.ToInt32(dr["customerID"]),
                        Name = Convert.ToString(dr["Name"]),
                        country = Convert.ToInt32(dr["country"]),
                        sex = Convert.ToString(dr["sex"]),
                        photo = Convert.ToString(dr["photo"]),
                        EmailID = Convert.ToString(dr["EmailID"]),

                    });
            }
            return studentlist;
        }

        // ***************** UPDATE STUDENT DETAILS *********************
        public bool UpdateDetails(customer smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdatecustomerDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customerID", smodel.customerID);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@country", smodel.country);
            cmd.Parameters.AddWithValue("@sex", smodel.sex);
            cmd.Parameters.AddWithValue("@photo", smodel.photo);
            cmd.Parameters.AddWithValue("@EmailID", smodel.EmailID);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE STUDENT DETAILS *******************
        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Deletecustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customerID", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
       
        public  List<country> Populatecountry()
        {
            connection();
            country model1 = new country();
            List<country> items = new List<country>();
            string constr = ConfigurationManager.ConnectionStrings["niccrudEntities"].ConnectionString;
           
                SqlCommand cmd = new SqlCommand("Select CountryID,Country From country", con);
                con.Open();


            SqlDataReader idr = cmd.ExecuteReader();

            List<country> customers = new List<country>();

            if (idr.HasRows)

            {

                while (idr.Read())

                {

                    customers.Add(new country

                    {

                        CountryID = Convert.ToInt32(idr["CountryID"]),

                        Country = Convert.ToString(idr["Country"]),

                    });

                }

            }



            con.Close();



           



            return customers;
        }
    }
}