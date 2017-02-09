using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PWWServ_Client.Models;
using PWWServ_Client.ViewModels;

namespace PWWServ_Client.Controllers
{
    public class TransferController : Controller
    {

        [HttpPost]
        public ActionResult AddTransfer(TransferVM inTVM)
        {
            PWServiceClient pwc = new PWServiceClient();
            pwc.CreateTransfer(inTVM.Transfer);
            return RedirectToAction("Index");
        }

    }
}
