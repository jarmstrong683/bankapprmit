using System;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using A3_MiBank_App.Data;
using A3_MiBank_App.Controllers;
using Microsoft.EntityFrameworkCore;
using A3_MiBank_App.Models;


// RESEARCH COMPONENT : THE FOLLOWING REFERENCES WERE USED TO HELP WRITE THE FOLLOWING CODE:

// DISCUSSION FORUM : THANKS VERY MUCH MATTHEW FOR POINTING US IN THE RIGHT DIRECTION
// WITH THIS FORUM RESPONSE (AND THANKS TO STUDENT PHUNG DO FOR ASKING THE QUESTION!
//https://rmit.instructure.com/groups/228800/discussion_topics/835273 :

// WE HAVE COMBINED  THE BACKGROUND TASK APPROACH WITH A BACKGROUND TIMER ( : BAGKROUND SERVICE, IHOSTEDSERVICE )
//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2&tabs=visual-studio#timed-background-tasks
// HAD SOME THREDAING ISSUES WHICH WERE SORTED OUT HERE
// https://docs.microsoft.com/en-gb/ef/core/miscellaneous/configuring-dbcontext#avoiding-dbcontext-threading-issues


namespace A3_MiBank_App.Services
{
    public class CheckScheduledBillPayService : BackgroundService, IHostedService, IDisposable
    {

        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        private CancellationToken _cancellationToken;

        public CheckScheduledBillPayService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            //_logger.LogInformation("Timed Background Service is starting.");
            _timer = new Timer(Dowork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private void Dowork(object state)
        {
            Do(state);
        }
        private async Task Do(object state)
        {
            await ExecuteAsync(_cancellationToken);
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MiBankContext>();


                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine("HELLO Background Service!");
                }


                // ONLY run if there are billpays present
                if (context.BillPay.Any())
                {

                    BillPayController controller = new BillPayController(context);
                    //BillPay billPayModel = new BillPay();

                    var billPays = await context.BillPay.ToListAsync();


                    if (billPays != null && billPays.Count > 0)
                    {
                        try
                        {
                            var billPaysDue = billPays.Where(b => b.ScheduleDate <= DateTime.Now);
                            foreach (var bill in billPaysDue)
                            {
                                if (!bill.Paid)
                                {
                                    await controller.PayBillImmediately(bill.BillPayID);


                                    if (bill.PaymentPeriod == Models.PaymentPeriod.Minutely)
                                    {
                                        controller.AddAnotherMinuteBillPay(bill);
                                    }

                                    if (bill.PaymentPeriod == Models.PaymentPeriod.Quarterly)
                                    {
                                        controller.AddAnotherQuarterBillPay(bill);
                                    }

                                    if (bill.PaymentPeriod == Models.PaymentPeriod.Quarterly)
                                    {
                                        controller.AddAnotherYearBillPay(bill);
                                    }


                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Bill Pays are up to date.");
                        }
                    }
                }
            }
        }


        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
