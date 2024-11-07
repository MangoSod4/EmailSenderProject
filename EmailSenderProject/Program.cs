using System;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit;

namespace EmailSenderProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a new message
                using var message = new MimeMessage();
                message.From.Add(new MailboxAddress("your_name", "your_email"));
                message.To.Add(new MailboxAddress("receiver_name", "receiver_email"));
                message.Subject = "Notification";

                // Configure the email body 
                message.Body = new TextPart()
                {
                    Text = @"Hey receiver_name, 

                    How are you? Did you watch the game yesterday?
                    I was thinking of making something up tonight.

                    Are you interested?

                    -- your_name
                    "
                };

                // Connecting and sending via SMTP 
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.example.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // Authentication (if necessary)
                    client.Authenticate("your_email", "your_app_password");

                    // Send the email
                    client.Send(message);

                    // Disconnects and closes the connection
                    client.Disconnect(true);
                    Console.WriteLine("Done!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
