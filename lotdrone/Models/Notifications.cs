using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace lotdrone.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
        public string LicensePlate { get; set; }
        public string Status { get; set; }

    }
}