using Hangfire;
using Microsoft.Extensions.Configuration;

namespace EmailSend.Models
{
    public class HangfireJobStarter:IHostedService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IConfiguration _configuration;

        public HangfireJobStarter(IBackgroundJobClient backgroundJobClient, IConfiguration configuration)
        {
            _backgroundJobClient = backgroundJobClient;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var emailService = new EmailService(_configuration);
            // Schedule a Hangfire job when the application starts
            var to = "noor.alam@bracits.com"; // Your Gmail account
            var subject = "Test Email.";
            var body = "Please Upload Tax Recipt.";
  
           // _backgroundJobClient.Schedule(() => emailService.SendEmail(to, subject, body), TimeSpan.FromMinutes(5));

            RecurringJob.AddOrUpdate<EmailService>("send-reminder",
            x => x.SendEmail(to, subject, body), Cron.MinuteInterval(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Cleanup logic, if needed
            return Task.CompletedTask;
        }

        //public void SendEmailJob()
        //{
        //    // Logic to send email using Hangfire job
        //    Console.WriteLine("Email job triggered!");
        //    // Add your email sending logic here
        //}
    }
}
