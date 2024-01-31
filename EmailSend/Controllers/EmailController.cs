using EmailSend.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmailSend.Controllers
{
    public class EmailController : Controller
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IConfiguration _configuration;

        public IActionResult Index()
        {
            return View();
        }
        public EmailController(IBackgroundJobClient backgroundJobClient, IConfiguration configuration)
        {
            _backgroundJobClient = backgroundJobClient;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult ScheduleEmail()
        {
            var emailService = new EmailService(_configuration); // Inject IConfiguration to access settings
            var to = "noor05cse@gmail.com"; // Your Gmail account
            var subject = "Test Email.";
            var body = "Please Upload Tax Recipt.";

            _backgroundJobClient.Enqueue(() => emailService.SendEmail(to, subject, body));

            return Ok("Email scheduled for sending!");
        }
    }
}
