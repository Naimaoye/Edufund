using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TrippleNT.Utility
{
    public class SendSMS
    {
        public static  bool Send(String telephone, String message,IConfiguration _config )
        {
            WebClient webclient = new WebClient();
            if (telephone.ToString().Length == 11 && telephone.ToString().Substring(0, 1) == "0")
            {
                telephone = "+234" + telephone.ToString().Substring(1);
            }
            var username = _config.GetSection("smsusername").ToString();
            var password = _config.GetSection("smspassword").ToString();
            string a = "https://smsexperience.com/api/sms/dnd-fallback?username=" + username + "&password=" + password + "&sender=EduFund&recipient=" + telephone + "&message=" + message + "";
            var content = webclient.DownloadString(a);

            if (content.Contains("TG00"))
            {
                return true;
            }
            else return false;
        }
    }
}
