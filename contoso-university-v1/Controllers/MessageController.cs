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

        // GET: Kill
        public void PerformKill()
        {
            Environment.Exit(1);
        }

        // GET: ExpensiveTask
        public ActionResult PerformExpensiveTask()
        {
            var end = DateTime.Now + TimeSpan.FromSeconds(3);
            while (DateTime.Now < end)
            {
                //do nothing
            }
            List<ContosoMessage> messages = new List<ContosoMessage>();
            for(int i=0; i<10; i++)
            {
                ContosoMessage message = new ContosoMessage();
                message.MessageID = (new Random()).Next().ToString();
                message.MessageString = (new Random()).Next().ToString() + (new Random()).Next().ToString();
                message.MessageTimestamp = ""+ (DateTime.Now.Ticks / (decimal)TimeSpan.TicksPerMillisecond);
                messages.Add(message);
            }
            return View(messages);
        }
    }
}