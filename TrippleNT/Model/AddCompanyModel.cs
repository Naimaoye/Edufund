using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrippleNT.DBConnect;
using TrippleNT.ResponseModels;
using static TrippleNT.ResponseModels.Payloads;
using static TrippleNT.ResponseModels.Responses;

namespace TrippleNT.Model
{
    public class AddCompanyToDb
    {

    public AddCompanyResponse addCompModel(AddCompanyPayload comp, IConfiguration _config,IHostingEnvironment env)
        {
            try
            {
                using(var db = new TrippleNTDBContext())
                {
                    var checkcompany = db.Company.Where(o => o.Name.ToLower() == comp.companyName.ToLower() || o.PhoneNumber == comp.phoneNumber || o.Email == comp.email ).FirstOrDefault();
                    if (checkcompany != null)
                    {
                        return checkcompany.Name.ToLower() == comp.companyName.ToLower() ? new AddCompanyResponse { status = "failed", msg = "Company Name Already Exists" } : checkcompany.PhoneNumber == comp.phoneNumber ? new AddCompanyResponse { status = "failed", msg = "Phone Number  Already Exists" } : new AddCompanyResponse { status = "failed", msg = "Email Already Exists" };
                    }
                    var newcomp = new Company();
                    newcomp.Name = comp.companyName;
                    newcomp.Email = comp.email;
                    newcomp.PhoneNumber = comp.phoneNumber;
                    newcomp.Type = 1;
                    newcomp.RegDate = DateTime.Now;
                    newcomp.Location = comp.location;
                    newcomp.State = comp.state;
                    db.Company.Add(newcomp);
                    db.SaveChanges();
                    var password = Utility.Encryptor.GeneratePassword(6);
                    var staffadmin = new CompanyStaff();
                    staffadmin.CompanyId = newcomp.CompanyId;
                    staffadmin.DateCreated = DateTime.Now;
                    staffadmin.UseStatus = false;
                    staffadmin.Password = Utility.Encryptor.EncodePasswordMd5(password);
                    staffadmin.UserType = "Admin";
                    staffadmin.UseStatus = true;
                    staffadmin.Status = "Active";
                    db.CompanyStaff.Add(staffadmin);
                    for (var i = 0; i < comp.noOfStaff+5;i++) {
                        var staff = new CompanyStaff();
                        staff.CompanyId = newcomp.CompanyId;
                        staff.DateCreated = DateTime.Now;
                        staff.UseStatus = false;
                        staff.Password = Utility.Encryptor.GeneratePassword(6);
                        staff.UserType = "User";
                        staff.UseStatus = true;
                        staff.Status = "Active";
                        db.CompanyStaff.Add(staff);
                    }
                    db.SaveChanges();
                    var msg = "Hello, " + comp.companyName + ", <br> We are glad to have you on our EduFund crowdfunding Platform. <br> Kindly See your credentials below <br><b>User Id: " + newcomp.PhoneNumber + "</b><br> <b>  Password: " + password + "  </b><br><br>#ChangeTheWorldWithYourSpareChange <br><br>Regards,<br> Admin Edufund";
                    Utility.SendMail.Send("Welcome to EduFund", msg, newcomp.Email, _config);

                    return new AddCompanyResponse { status="success",msg="Company Created Successfully" };
                }

            }
            catch(Exception ex)
            {
                new Utility.LogWriter(ex.Message + " " + ex.InnerException, env);
                return new AddCompanyResponse { status = "failed", msg = "Something Went Wrong Try again Later" };
            }

        }
        public AddCompanyResponse addCompIndvidualModel(AddCompanyIndPayload comp,IConfiguration _config , IHostingEnvironment env)
        {
            try
            {
                using (var db = new TrippleNTDBContext())
                {
                    var checkcompany = db.Company.Where(o =>  o.PhoneNumber== comp.phoneNumber|| o.Email ==comp.email || o.Rcno ==comp.rcNo).FirstOrDefault();
                    if (checkcompany != null)
                    {
                        return  checkcompany.PhoneNumber == comp.phoneNumber? new AddCompanyResponse { status = "failed", msg = "Phone Number  Already Exists" } :comp.rcNo==checkcompany.Rcno ? new AddCompanyResponse { status = "failed", msg = "Invalid Company Reg Number" }: new AddCompanyResponse { status = "failed", msg = "Email Already Exists" };
                    }
                    var newcomp = new Company();
                    newcomp.Name = comp.companyName;
                    newcomp.Email = comp.email;
                    newcomp.PhoneNumber = comp.phoneNumber;
                    newcomp.Type = 1;
                    newcomp.RegDate = DateTime.Now;
                    newcomp.Location = comp.location;
                    newcomp.State = comp.state;
                    db.Company.Add(newcomp);
                    db.SaveChanges();
                    var password =comp.password;
                    var staff = new CompanyStaff();
                    staff.CompanyId = newcomp.CompanyId;
                    staff.DateCreated = DateTime.Now;
                    staff.UseStatus = false;
                    staff.Password = Utility.Encryptor.EncodePasswordMd5(password);
                    staff.UserType = "Admin";
                    db.CompanyStaff.Add(staff);
                    var staff1 = new CompanyStaff();
                    staff.CompanyId = newcomp.CompanyId;
                    staff1.DateCreated = DateTime.Now;
                    staff1.UseStatus = false;
                    staff1.Password = Utility.Encryptor.GeneratePassword(6); 
                    staff1.UserType = "User";
                    db.CompanyStaff.Add(staff);
                    db.SaveChanges();
                    var msg = "Hello, " + comp.companyName + ", <br> We are glad to have you on our EduFund crowdfunding Platform. <br> Kindly See your credentials below <br><b>User Id: " + newcomp.PhoneNumber + "</b><br> <b>  Password: "+password+"  </b><br><br>#ChangeTheWorldWithYourSpareChange <br><br>Regards,<br> Admin Edufund";
                    Utility.SendMail.Send("Welcome to EduFund", msg,newcomp.Email, _config);


                    return new AddCompanyResponse { status = "success", msg = "Company Created Successfully" };
                }

            }
            catch (Exception ex)
            {
                new Utility.LogWriter(ex.Message + " " + ex.InnerException, env);
                return new AddCompanyResponse { status = "failed", msg = "Something Went Wrong Try again Later" };
            }

        }



    }
}
