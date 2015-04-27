using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Postal;

namespace lotdrone.Models
{
    public class NewRegistrationEmail : Email
    {
        public string To { get; set; }
        public string Body { get; set; }
    }
}