using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class MessageController : Controller
    {
        private ServiceBusContext sbc = ServiceBusContext.Instance;

        // GET: Message
        public ActionResult Index()
        {
            List<ContosoMessage> messages = sbc.ReceiveMessages();
            return View(messages);
        }
    }
}