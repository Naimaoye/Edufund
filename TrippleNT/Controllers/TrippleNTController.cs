using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TrippleNT.Model;
using static TrippleNT.ResponseModels.Payloads;
using static TrippleNT.ResponseModels.Responses;

namespace TrippleNT.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    public class TrippleNTController : ControllerBase
    {
        private IConfiguration configuration;
        private IHostingEnvironment _hosting;
        public TrippleNTController(IConfiguration config, IHostingEnvironment hosting)
        {
            configuration = config;
            _hosting = hosting;
        }



        [HttpPost]
        [Route("api/TrippleNT/AddCompany/")]
        // [MessageHandlers.JWTAuthHandler]
        public AddCompanyResponse AddCompany(AddCompanyPayload company)
        {
            return new AddCompanyToDb().addCompModel(company, configuration,_hosting);
        }
        [HttpPost]
        [Route("api/TrippleNT/AddCompanyInd/")]
        // [MessageHandlers.JWTAuthHandler]
        public AddCompanyResponse AddCompanyInd(AddCompanyIndPayload company)
        {
            return new AddCompanyToDb().addCompIndvidualModel(company, configuration, _hosting);
        }
        [HttpGet]
        [Route("api/TrippleNT/LoginUser/")]
        // [MessageHandlers.JWTAuthHandler]
        public loginResponse LoginUser(loginPayload user)
        {
            return    LoginModel.Login(user, _hosting);
        }
        [HttpPost]
        [Route("api/TrippleNT/ChangePassword/")]
        // [MessageHandlers.JWTAuthHandler]
        public changePasswordResponse ChangePassword(changePasswordPayLoad user)
        {
            return new ChangePasswordModel().changePassword(user, _hosting,configuration);
        }
        [HttpGet]
        [Route("api/TrippleNT/ResetPassword/")]
        // [MessageHandlers.JWTAuthHandler]
        public resetPasswordResponse ResetPassword(string userId)
        {
            return new ResetPassword().resetPassword(Convert.ToInt64(userId), _hosting, configuration);
        }
        [HttpGet]
        [Route("api/TrippleNT/GetDonationsForCompany/")]
        // [MessageHandlers.JWTAuthHandler]
        public List<getDonationsResponse> GetDonationsForCompany(string companyId,string startdate,string enddate)
        {
            return new GetDonationsByCompany().getDonationsForCompany(Convert.ToInt64(companyId), Convert.ToDateTime(startdate), Convert.ToDateTime(startdate));
        }
        [HttpGet]
        [Route("api/TrippleNT/GetNotReconciledDonationsForCompany/")]
        // [MessageHandlers.JWTAuthHandler]
        public List<getDonationsResponse> GetNotReconciledDonationsForCompany(string companyId)
        {
            return new GetNotReconciledDonations().getNotReconciledForCompany(Convert.ToInt64(companyId));
        }
        [HttpGet]
        [Route("api/TrippleNT/NewDonatation/")]
        // [MessageHandlers.JWTAuthHandler]
        public donateResponse NewDonatation(donatePayload donation)
        {
            return new Donate().donate(donation, configuration,_hosting);
        }
        [HttpGet]
        [Route("api/TrippleNT/MakePayment/")]
        // [MessageHandlers.JWTAuthHandler]
        public PaymentResponse MakePayment(PaymentPayload payment)
        {
            return new Pay().NewRecord(payment);
        }
    }
}