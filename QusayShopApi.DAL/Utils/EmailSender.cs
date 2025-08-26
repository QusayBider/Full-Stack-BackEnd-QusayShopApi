﻿using Azure.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QusayShopApi.DAL.Utils
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("QusayBider@gmail.com", "hkqc giwb zegy okpp")
            };

            return client.SendMailAsync(
                new MailMessage(from: "QusayBider@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {IsBodyHtml=true}
                );
        }
    }
}
