using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ASPFinalExam.Models;

namespace ASPFinalExam.Service
{
    public class CustomerService
    {
        Dao.CustomerDao customerDao = new Dao.CustomerDao();

        
        public int InsertCustomer(Models.Customer customer)
        {
            int customerId = customerDao.InsertCustomer(customer);
            return customerId;
        }


        public List<Models.Customer> GetCustomer()
        {
            DataTable dt = customerDao.GetCustomer();
            return this.MapCustomerDataToList(dt);
        }

        public List<Models.Customer> GetCustomerByCondition(Models.CustomerSearchArg arg)
        {
            DataTable dt = customerDao.GetCustomerByCondition(arg);
            return this.MapCustomerDataToList(dt);
        }

        /// <summary>
        /// 刪除客戶
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int DeleteCustomerById(string customerId)
        {
            return customerDao.DeleteCustomerById(customerId);
        }



        private List<Customer> MapCustomerDataToList(DataTable dt)
        {
            List<Customer> result = new List<Customer>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Customer()
                {
                    CustomerId = (int)row["CustomerId"],
                    CompanyName = row["CompanyName"].ToString(),
                    ContactName = row["ContactName"].ToString(),
                    ContactTitle = row["ContactTitle"].ToString()
                });
            }
            return result;
        }

    }
}