using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrippleNT.Model
{

    public class resetPasswordResponse
    {
        public string status { get; set; }
        public string msg { get; set; }
    }

    public class ResetPassword
    {
        public resetPasswordResponse resetPassword(long userid, IHostingEnvironment env, IConfiguration _config)
        {
            try
            {
                using (var db = new DBConnect.TrippleNTDBContext())
                {
                    var user = db.CompanyStaff.Find(userid);
                    if(user==null) return new resetPasswordResponse { status = "failed", msg = "user not found" };
                    else
                    {
                        if (user.UserType == "Admin")
                        {
                            var password = Utility.Encryptor.GeneratePassword(6);
                            user.Password = Utility.Encryptor.EncodePasswordMd5(password);
                            user.UseStatus = true;
                            db.CompanyStaff.Attach(user);
                            db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();
                            var comp = db.Company.Find(user.CompanyId);
                            var msg = "Hello, " + comp.Name + ", <br> We are glad to have you on our EduFund crowdfunding Platform. <br>Kindly see your new  credentials below <br><b>User Id: " + comp.PhoneNumber + "</b><br> <b>  Password: " + password + "  </b><br><br>#ChangeTheWorldWithYourSpareChange <br><br>Regards,<br> Admin Edufund";
                            Utility.SendMail.Send("Welcome to EduFund", msg, comp.Email, _config);
                            return new resetPasswordResponse { status = "success", msg = "Password Reset Successful" };
                        }
                        else
                        {

                            var password = Utility.Encryptor.GeneratePassword(6);
                            user.Password = password;
                            user.UseStatus = true;
                            db.CompanyStaff.Attach(user);
                            db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();
                            return new resetPasswordResponse { status = "success", msg = "Password Reset Successful" };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                new Utility.LogWriter(ex.Message + " " + ex.InnerException, env);
                return new resetPasswordResponse { status = "failed", msg = "Something went wrong try again later" };
            }
        }

    }
}
