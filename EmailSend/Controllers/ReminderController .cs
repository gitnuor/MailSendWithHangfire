using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace EmailSend.Controllers
{
    public class ReminderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ScheduleReminder(string email, string subject, string body)
        {
            BackgroundJob.Schedule(() => SendReminderEmail(email, subject, body), TimeSpan.FromMinutes(1));
            return RedirectToAction("Index");
        }

        public void SendReminderEmail(string email, string subject, string body)
        {
            Console.WriteLine($"Sending reminder email to: {email}, Subject: {subject}, Body: {body}");
        }
    }
}
