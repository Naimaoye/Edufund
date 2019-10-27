using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrippleNT.Model
{

    public class changePasswordPayLoad
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

        public long userId { get; set; }
    }
    public class changePasswordResponse
    {
        public string status { get; set; }
        public string msg { get; set; }
    }

    

    public class ChangePasswordModel
    {

        public changePasswordResponse changePassword(changePasswordPayLoad payload, IHostingEnvironment env, IConfiguration _config)
        {
            try
            {
                using(var db = new DBConnect.TrippleNTDBContext())
                {
                    var user = db.CompanyStaff.Find(payload.userId);
                    if (user == null) return new changePasswordResponse { status = "failed", msg = "user not found" };
                    else
                    {
                        if (user.UserType == "Admin")
                        {
                            if(Utility.Encryptor.EncodePasswordMd5(user.Password)== Utility.Encryptor.EncodePasswordMd5(payload.oldPassword))
                            {
                                user.Password = Utility.Encryptor.EncodePasswordMd5(payload.newPassword);
                                user.UseStatus = false;
                                db.CompanyStaff.Attach(user);
                                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                db.SaveChanges();
                                var comp = db.Company.Find(user.CompanyId);
                                var msg = "Hello, " +comp.Name + ", <br> Tis to notify you that your password change was successful <br> Thanking for being part of this quest of making an impact in the world.<br><br>#ChangeTheWorldWithYourSpareChange <br><br>Regards,<br> Admin Edufund";
                                Utility.SendMail.Send("Welcome to EduFund", msg, comp.Email, _config);
                                return new changePasswordResponse { status = "success", msg = "P Changed Successfully" };

                            }
                            else return new changePasswordResponse { status = "failed", msg = "Invalid Old Password" };
                        }
                        else
                        {
                            if (user.UseStatus)
                            {
                                if (user.Password == payload.oldPassword)
                                {
                                    user.Password = Utility.Encryptor.EncodePasswordMd5(payload.newPassword);
                                    user.UseStatus = false;
                                    db.CompanyStaff.Attach(user);
                                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    db.SaveChanges();
                                    return new changePasswordResponse { status = "success", msg = "Password Changed Successfully" };
                                }
                                else return new changePasswordResponse { status = "failed", msg = "Invalid Old Password" };
                            }
                            else
                            {
                                if (Utility.Encryptor.EncodePasswordMd5(user.Password) == Utility.Encryptor.EncodePasswordMd5(payload.oldPassword))
                                {
                                    user.Password = Utility.Encryptor.EncodePasswordMd5(payload.newPassword);
                                    user.UseStatus = false;
                                    db.CompanyStaff.Attach(user);
                                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    db.SaveChanges();
                                    return new changePasswordResponse { status = "success", msg = "Password Changed Successfully" };
                                }
                                else return new changePasswordResponse { status = "failed", msg = "Invalid Old Password" };
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                new Utility.LogWriter(ex.Message + " " + ex.InnerException, env);
                return new changePasswordResponse { status = "failed", msg = "Something went wrong try again later" };
            }
        }
    


    }
}
