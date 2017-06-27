using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPFinalExam.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult InsertCustomer()
        {
            return View(new Models.Customer());
        }


        [HttpPost()]
        public JsonResult DoInsertCustomer(Models.Customer customer)
        {
            Service.CustomerService customerService = new Service.CustomerService();
            int orderid = customerService.InsertCustomer(customer);
            ModelState.Clear();
            JsonResult result = this.Json(orderid, JsonRequestBehavior.AllowGet);
            return result;
        }

        /// <summary>
        /// 首頁回傳
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult SelectOrder(Models.CustomerSearchArg arg)
        {
            try
            {
                Service.CustomerService orderService = new Service.CustomerService();
                JsonResult result = this.Json(orderService.GetCustomerByCondition(arg), JsonRequestBehavior.AllowGet);
                return result;

            }
            catch (Exception)
            {

                return this.Json(false);
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteCustomer(Models.DeleteJson customer)
        {
            try
            {
                string customerId = customer.CustomerId;
                Service.CustomerService orderService = new Service.CustomerService();
                int result = orderService.DeleteCustomerById(customerId);
                if (result >= 1)
                {
                    return this.Json(true);
                }
                else
                {
                    return this.Json(false);
                }


            }
            catch (Exception)
            {

                return this.Json(false);
            }

        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public JsonResult Read(Models.CustomerSearchArg arg)
        {
            try
            {
                Service.CustomerService customerService = new Service.CustomerService();
                JsonResult result = this.Json(customerService.GetCustomerByCondition(arg), JsonRequestBehavior.AllowGet);
                return result;

            }
            catch (Exception)
            {

                return this.Json(false);
            }

        }




        /// <summary>
        /// 取得員工資料
        /// </summary>
        public JsonResult GetContactTitleList()
        {
            try
            {
                Service.CodeService codeService = new Service.CodeService();
                JsonResult result = this.Json(codeService.GetTitle(), JsonRequestBehavior.AllowGet);
                return result;

            }
            catch (Exception)
            {

                return this.Json(false);
            }
        }
    }
}