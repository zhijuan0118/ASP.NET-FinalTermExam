using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASPFinalExam.Dao
{
    public class CustomerDao
    {
        /// <summary>
        /// DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public int InsertCustomer(Models.Customer customer)
        {
            string sql = @"INSERT INTO [Sales].[Customers]
                               ([CompanyName]
                               ,[ContactName]
                               ,[ContactTitle]
                               ,[CreationDate]
                               ,[Address]
                               ,[City]
                               ,[Region]
                               ,[PostalCode]
                               ,[Country]
                               ,[Phone]
                               ,[Fax])
                         VALUES
                               @CompanyName,
                               @ContactName
                               @ContactTitle,
                               @CreationDate,
                               @Address,
                               @City,
                               @Region,
                               @PostalCode,
                               @Country,
                               @Phone,
                               @Fax,
						Select SCOPE_IDENTITY()
						";
            int orderId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CompanyName", customer.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", customer.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", customer.ContactTitle));
                cmd.Parameters.Add(new SqlParameter("@CreationDate", customer.CreationDate));
                cmd.Parameters.Add(new SqlParameter("@Address", customer.Address));
                cmd.Parameters.Add(new SqlParameter("@City", customer.City));
                cmd.Parameters.Add(new SqlParameter("@Region", customer.Region == null ? string.Empty : customer.Region));
                cmd.Parameters.Add(new SqlParameter("@PostalCode", customer.PostalCode.ToString() == null ? 0 : customer.PostalCode));
                cmd.Parameters.Add(new SqlParameter("@Country", customer.Country));
                cmd.Parameters.Add(new SqlParameter("@Phone", customer.Phone));
                cmd.Parameters.Add(new SqlParameter("@Fax", customer.Fax == null ? string.Empty : customer.Fax));

                orderId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return orderId;
        }


        public DataTable GetCustomer()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                                A.CustomerID,
                                A.CompanyName,
                                A.ContactName,
                                 B.CodeId+'-'+B.CodeVal AS ContactTitle  
                            FROM 
                                Sales.Customers AS A INNER JOIN 
                                CodeTable AS B ON A.ContactTitle=B.CodeId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }


        public DataTable GetCustomerByCondition(Models.CustomerSearchArg arg)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                                A.CustomerID,
                                A.CompanyName,
                                A.ContactName,
                                B.CodeId+'-'+B.CodeVal AS ContactTitle   
                            FROM 
                                Sales.Customers AS A INNER JOIN 
                                CodeTable AS B ON A.ContactTitle=B.CodeId
                            WHERE 
                                (A.Companyname Like @CompanyName OR @CompanyName = '') AND
                                (A.CustomerID Like @CustomerID OR @CustomerID = '') AND
                                (A.ContactName = @ContactName OR @ContactName = '') AND
                                ( (B.CodeId+'-'+B.CodeVal) = @ContactTitle OR @ContactTitle = '')";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CompanyName", arg.CompanyName == null ? string.Empty : '%' + arg.CompanyName + '%'));
                cmd.Parameters.Add(new SqlParameter("@CustomerID", arg.CustomerId == null ? string.Empty : '%' + arg.CustomerId + '%'));
                cmd.Parameters.Add(new SqlParameter("@ContactName", arg.ContactName == null ? string.Empty : arg.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", arg.ContactTitle == null ? string.Empty : arg.ContactTitle));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public int DeleteCustomerById(string customerId)
        {
            try
            {
                int result;
                string sql = "DELETE FROM Sales.Customers WHERE CustomerId = @CustomerId";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", customerId));
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}