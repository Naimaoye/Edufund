using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrippleNT.DBConnect;

namespace TrippleNT.Services
{
    public class BGService
    {
        internal class AmAvailable : IHostedService, IDisposable
        {
            private readonly ILogger _logger;


            private readonly IConfiguration _configuration;
            private Timer _timer;
            public static DateTime haverun2 = DateTime.Now.AddDays(-1);

            public AmAvailable(ILogger<AmAvailable> logger, IConfiguration config)
            {
                _logger = logger;
                _configuration = config;
              
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Timed Background Service is starting.");
                //Thread t1 = new Thread(new ThreadStart(MainWorker));
                //t1.Start();
                _timer = new Timer(DoWork, null, TimeSpan.Zero,
                 TimeSpan.FromSeconds(60*60*1000));

                return Task.CompletedTask;
            }

            private void MainWorker()
            {
                _logger.LogInformation("Background Service is working.");
                while (true)
                {
                    DoWork(null);
                    Thread.Sleep(300000);
                }

            }
            private void DoWork(object state)
            {
                _logger.LogInformation("Background Service is working. Am available");
                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (haverun2 != DateTime.Now) {
                        using (var db = new TrippleNTDBContext())
                        {
                            var reco = new Reconciliation();
                            var enddate = DateTime.Now.AddDays(-1).Date;
                            var comp = db.Company.Where(o => o.Status == "Acvtive").ToList();
                            foreach (var item in comp)
                            {
                                reco.Amount = db.Donations.Where(o => o.ComapanyId == item.CompanyId && o.Status == "NT" && o.DateDonated.Date <= enddate).Sum(o => o.Amount);
                                reco.StartPeriod = DateTime.Now.AddDays(-7);
                                reco.EndPeriod = DateTime.Now.AddDays(-1);
                                reco.CompanyId = item.CompanyId;
                                reco.Status = "Pending";
                                var msg = "Hello, " + item.Name + ", <br> Your donation reconciliation of NGN" + string.Format("{0:n}", reco.Amount) + " for this week is now ready. Kindly log in to make payment. <br> Thank you for your continuous co-operation and for your contribution to changing the world.<br><br>#ChangeTheWorldWithYourSpareChange <br><br>Regards,<br> Admin Edufund";
                                Utility.SendMail.Send("Welcome to EduFund", msg, item.Email, _configuration);
                            }
                            haverun2 = DateTime.Now;

                        }

                    }
                }
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _logger.LogInformation("Timed Background Service is stopping.");

                _timer?.Change(Timeout.Infinite, 0);

                return Task.CompletedTask;
            }

            public void Dispose()
            {
                _timer?.Dispose();
            }
        }
    }

}
