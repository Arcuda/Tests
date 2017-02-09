using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PWWServ_Client.Models;
using PWWServ_Client.ViewModels;

namespace PWWServ_Client.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            PWServiceClient pwc = new PWServiceClient();
            ViewBag.ListCustomers = pwc.All();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(CustomerVM inCVM)
        {
            PWServiceClient pwc = new PWServiceClient();
            pwc.CreateCustomer(inCVM.Customer);
            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(string inEmail)
        {
            PWServiceClient pwc = new PWServiceClient();
            pwc.Delete(pwc.Find(inEmail));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(CustomerVM inCVM)
        {
            PWServiceClient pwc = new PWServiceClient();
            Customer _cust = pwc.Login(inCVM.Customer.Email, inCVM.Customer.PassHesh);
            if (_cust == null)
                return View("ErrorLogin");
            else
            {
                List<Transfer> _rts = pwc.FindCustTransfers(_cust.Email);
                ViewBag.Customer = _cust;
                ViewBag.Transfers = _rts;
                return View("Profile");
            }
        }


        [HttpGet]
        public ActionResult AddTransfer(CustomerVM inCVM)
        {
            ViewBag.Customer = inCVM.Customer;
            return View("CreateTransfer");
        }

        [HttpPost]
        public ActionResult CreatTransfer(TransferVM inTVM)
        {
            PWServiceClient pwc = new PWServiceClient();
            
            inTVM.Transfer.WhenDT = DateTime.Now;
            pwc.CreateTransfer(inTVM.Transfer);

            Customer _cust = pwc.Find(inTVM.Transfer.Client);
            List<Transfer> _rts = pwc.FindCustTransfers(_cust.Email);
            ViewBag.Customer = _cust;
            ViewBag.Transfers = _rts;
            return View("Profile");

        }

    }
}
