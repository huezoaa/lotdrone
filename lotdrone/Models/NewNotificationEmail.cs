using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Postal;

namespace lotdrone.Models
{
    public class NewNotificationEmail : Email
    {
        public string To { get; set; }
        public string LicensePlate { get; set; }
        public string Notification { get; set; }
    }
}