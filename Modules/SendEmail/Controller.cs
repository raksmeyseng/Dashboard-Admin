using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace ArchtistStudio.Modules.SendEmail;

public class ApiSendEmailController : MyAdminController
{
    [HttpPost]
    public IActionResult Insert([FromBody] SendEmail sendEmail)
    {
        MailMessage mailMessage = new MailMessage
        {
            Subject = "New Inquiry from " + sendEmail.Name,
            Body = $@"
        <html>
        <body style='font-family: Arial, sans-serif; color: #333;'>
            <h2 style='color: #0066cc;'>New Inquiry Received</h2>
            <p><strong>Name:</strong> {sendEmail.Name}</p>
            <p><strong>Email:</strong> {sendEmail.Email}</p>
            <p><strong>Company:</strong> {sendEmail.Company}</p>
            <p><strong>Country:</strong> {sendEmail.Country}</p>
            <p><strong>Phone:</strong> {sendEmail.Phone}</p>
            <p><strong>Expertise:</strong> {sendEmail.Expertise}</p>
            
            <h4 style='color: #0066cc;'>Message</h4>
            <p style='background-color: #f9f9f9; padding: 15px; border: 1px solid #ddd;'>
                {sendEmail.Message}
            </p>
            
            <hr style='border: 1px solid #ddd;' />
            <p><small>Sent on: {DateTime.Now.ToString("F")}</small></p>
        </body>
        </html>",

            IsBodyHtml = true,
            From = new MailAddress("Sengvichet2525@gmail.com", "fromSone")
        };
        mailMessage.To.Add(new MailAddress("Sengvichet2525@gmail.com", "Nerd someone"));

        SmtpClient smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(MyAccount.OwnerGmail, MyAccount.OwnerPassword),
            Port = 587
        };

        smtpClient.Send(mailMessage);

        return Ok();
    }

}
