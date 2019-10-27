using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;

namespace TrippleNT.Model
{


  public class   loginPayload{
        public string username { get; set; }
        public string password { get; set; }
    }
    public class loginResponse
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string id { get; set; }
        public string role { get; set; }
      
        public string companyId { get; set; }

        public string companyName { get; set; }
        public bool firsttime { get; set; }


    }
    public class LoginModel
    {


        public static loginResponse Login(loginPayload payload,IHostingEnvironment env)
        {
            try
            {
                using(var db =new TrippleNTDBContext())
                {
                    if (payload.username.Length <=7)
                    {
                        var userid = Convert.ToInt64(payload.username);
                        var user = db.CompanyStaff.Find(userid);
                        if(user.Status=="Disabled") return new loginResponse { status = "failed", msg = "You are not Authirized on this system" };
                        if (user == null) return new loginResponse { status="failed", msg = "Invalid Username or Password" };
                       
                        else return user.Password== Utility.Encryptor.EncodePasswordMd5(payload.password)?  new loginResponse { status = "success", msg = "Login Successful", role=user.UserType ,id=user.UserId.ToString(),companyId =user.CompanyId.ToString(),companyName=db.Company.Find(user.CompanyId).Name ,firsttime=user.UseStatus}: new loginResponse { status = "failed", msg = "Invalid Username or Password" ,role="",id="", companyId ="" ,companyName="", firsttime = false };
                        
                    }
                    else
                    {
                        var company = db.Company.Where(o => o.PhoneNumber == payload.username).FirstOrDefault();
                        if(company==null) return new loginResponse { status = "failed", msg = "Invalid Username or Password" };
                        else{
                            var password = Utility.Encryptor.EncodePasswordMd5(payload.password);
                            var user = db.CompanyStaff.Where(o => o.CompanyId== company.CompanyId && o.UserType=="Admin" && o.Password==password).FirstOrDefault();
                            return user.Password == Utility.Encryptor.EncodePasswordMd5(payload.password) ? new loginResponse { status = "success", msg = "Login Successful", role = user.UserType, id = user.UserId.ToString(), companyId = user.CompanyId.ToString(), companyName = db.Company.Find(user.CompanyId).Name, firsttime = user.UseStatus } : new loginResponse { status = "failed", msg = "Invalid Username or Password", role = "", id = "", companyId = "", companyName = "" , firsttime = false };
                        }
                       
                    }
                    
                }
            }
            catch(Exception ex)

            {
                new Utility.LogWriter(ex.Message + " " + ex.InnerException, env);
                return new loginResponse { status = "failed", msg = "Something went wrong try again later" };
            }
        }

    }
}
