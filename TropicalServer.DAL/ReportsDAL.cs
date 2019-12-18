using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace TropicalServer.DAL
{
    public class ReportsDAL
    {
        string connString = Convert.ToString(ConfigurationManager.AppSettings["TropicalServerConnectionString"]);

        /*
         * Insert item description to get the #, description, 
         * pre-price and size of the item           
         */
        public bool GetUserAuthentication(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select count(*) From tblUserLogin where UserID = @username AND Password = @password", connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);

                if (count == 1) return true;
                else return false;

            }
        }

        public bool ValidateEmail(string email)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@Email", SqlDbType.VarChar);
            parameters[0].Value = email;

            try
            {
                bool value = false;
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spValidateEmail", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    int count = reader.GetInt32(0);

                    if (count == 1) value = true;
                    else value =  false;

                    connection.Close();
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
            }
        }

        public DataSet GetProductByProductCategory_DAL(string newItemDescription)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            DataSet ds = new DataSet();

            parameters[0] = new SqlParameter("@itemDescription", SqlDbType.NVarChar);

            if (newItemDescription.Trim() != string.Empty)
                parameters[0].Value = newItemDescription.Trim();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetProductByProductCategory", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Product Categories - " + ex.Message.ToString());
            }
        }//End of GetProductByProductCategory_DAL method...

        public string GetOrderDetails(string orderid)
        {
            string message = "";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select OrderItemQty, ItemUnitPrice, ItemTotalPrice from tblOrder Where OrderID = @OrderID", connection);
                command.Parameters.AddWithValue("@OrderID", int.Parse(orderid));

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    message += "Quantity: " + reader.GetInt32(0) + "&nbsp" + "Unit Price: " + reader.GetDecimal(1) + "&nbsp" + "Total Price: " + reader.GetDecimal(2);
                }

            }
            return message;
        }

        public void DeleteOrder(string orderid)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Delete from tblOrder Where OrderID = @OrderID", connection);
                command.Parameters.AddWithValue("@OrderID", int.Parse(orderid));

                command.ExecuteNonQuery();

            }
        }

        public void UpdateOrder(string text1, string text2, string text3, string text4, string text5, string text6, string v)
        {
            SqlParameter[] parameters = new SqlParameter[7];
            
            parameters[0] = new SqlParameter("@OrderTrackingNumber", SqlDbType.VarChar);
            parameters[0].Value = text1;

            parameters[1] = new SqlParameter("@OrderDate", SqlDbType.VarChar);
            parameters[1].Value = text2;

            parameters[2] = new SqlParameter("@OrderCustomerNumber", SqlDbType.Int);
            parameters[2].Value = int.Parse(text3);

            parameters[3] = new SqlParameter("@CustName", SqlDbType.VarChar);
            parameters[3].Value = text4;

            parameters[4] = new SqlParameter("@CustOfficeAddress1", SqlDbType.VarChar);
            parameters[4].Value = text5;

            parameters[5] = new SqlParameter("@OrderRouteNumber", SqlDbType.Int);
            parameters[5].Value = int.Parse(text6);

            parameters[6] = new SqlParameter("@OrderID", SqlDbType.Int);
            parameters[6].Value = int.Parse(v);

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spUpdateOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Update Info - " + ex.Message.ToString());
            }
        }

        /*
         *Enter a number to populate 
         * the CustSalesRepNumber
         */
        public DataSet GetCustSalesRepNumber_DAL(int newCustSaleRepNum)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            DataSet ds = new DataSet();

            parameters[0] = new SqlParameter("@custSaleRepNum", SqlDbType.Int);

            if (newCustSaleRepNum != 0)
                parameters[0].Value = newCustSaleRepNum;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetCustSalesRepNumber", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Sales Representative Number - " + ex.Message.ToString());
            }
        }// End of GetCustSalesRepNumber_DAL method...

        /*
         * Select custSalesRepNum on the 
         * side bar to get the route info.
         */
        public DataSet GetRouteInfo_DAL(int newRouteID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            DataSet ds = new DataSet();

            parameters[0] = new SqlParameter("@roleID", SqlDbType.Int);

            parameters[0].Value = newRouteID;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetRouteInfo", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
            }

        }// End of GetRouteInfo_DAL method...

        /*
         * Get the Name,LoginID, password, Role Description
         * of the User who are active(true).
         */
        public DataSet GetUsersSetting_DAL()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetUsersSetting", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
            }
        }// End of GetRouteInfo_DAL method...

        /*
         * Get the Customers for Setting page.
         */
        public DataSet GetCustomersSetting_DAL()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetCustomersSetting", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
            }
        }// End of GetCustomersSetting_DAL method...

        /*
         * Get the Price Group Info for Setting page.
         */
        public DataSet GetPriceGroupSetting_DAL()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetPriceGroupSetting", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
            }
        }// End of GetPriceGroup_DAL method...

        public DataSet GetOrdersByTimePeriod(string OrderDate)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            DataSet ds = new DataSet();

            parameters[0] = new SqlParameter("@OrderDate", SqlDbType.VarChar);

            parameters[0].Value = OrderDate;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetOrdersByTimePeriod", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
            }
        }
        public DataSet GetOrdersByTimePeriodAndCustID(string OrderDate, int customerNumber)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            DataSet ds = new DataSet();

            parameters[0] = new SqlParameter("@OrderDate", SqlDbType.VarChar);
            parameters[0].Value = OrderDate;

            parameters[1] = new SqlParameter("@CustID", SqlDbType.Int);
            parameters[1].Value = customerNumber;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetOrdersByTimePeriodAndCustID", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Order Info - " + ex.Message.ToString());
            }
        }
        public DataSet GetOrdersByTimePeriodAndCustName(string OrderDate, string customerName)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            DataSet ds = new DataSet();

            parameters[0] = new SqlParameter("@OrderDate", SqlDbType.VarChar);
            parameters[0].Value = OrderDate;

            parameters[1] = new SqlParameter("@CustName", SqlDbType.VarChar);
            parameters[1].Value = customerName;

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetOrdersByTimePeriodAndCustName", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        SqlParameter p = null;
                        foreach (SqlParameter sqlP in parameters)
                        {
                            p = sqlP;
                            if (p != null)
                            {
                                if (p.Direction == ParameterDirection.InputOutput ||
                                   p.Direction == ParameterDirection.Input && p.Value == null)
                                {
                                    p.Value = DBNull.Value;
                                }
                                command.Parameters.Add(p);
                            }
                        }
                    }
                    command.CommandTimeout = 6000;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while retrieving Order Info - " + ex.Message.ToString());
            }
        }
    }    
}

