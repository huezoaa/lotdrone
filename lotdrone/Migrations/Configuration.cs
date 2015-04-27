namespace lotdrone.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using lotdrone.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;


    internal sealed class Configuration : DbMigrationsConfiguration<lotdrone.Models.ApplicationDbContext>
    {
        bool AddUserAndRole(lotdrone.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "ahuezo",
            };
            ir = um.Create(user, "P@ssword1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "lotdrone.Models.ApplicationDbContext";
        }

        protected override void Seed(lotdrone.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
            context.Employees.AddOrUpdate( e => e.LastName,
              new Employee 
              { 
                  LastName = "Huezo",
                  FirstName = "Angel",
                  Email = "angelohuezo@gmail.com",
                  Extension = 1000,
              },
              new Employee
              {
                  LastName = "Lopez",
                  FirstName = "Tai",
                  Email = "tailopez@gmail.com",
                  Extension = 1001,
              },
              new Employee
              {
                  LastName = "Rose",
                  FirstName = "Brian",
                  Email = "brianrose@gmail.com",
                  Extension = 1002,
              }
              );

            context.Cars.AddOrUpdate(c => c.Make,
                new Car
                {
                    Make = "Honda",
                    Model = "Accord",
                    Year = 2015,
                    Color = "Green",
                    LicensePlate = "123ABC",
                    EmployeeID = 3,
                },
                new Car
                {
                    Make = "Chevy",
                    Model = "Impala",
                    Year = 2015,
                    Color = "Blue",
                    LicensePlate = "XYZ456",
                    EmployeeID = 1,
                },
                new Car
                {
                    Make = "Ford",
                    Model = "F150",
                    Year = 2015,
                    Color = "White",
                    LicensePlate = "305MIA",
                    EmployeeID = 2,
                }
                );

            context.Notifications.AddOrUpdate(m => m.TimeStamp,
                new Notification
                {
                    TimeStamp = Convert.ToDateTime("4/19/2015 9:35 am"),
                    Description = "White Ford truck, your headlights are still on. License plate 305MIA",
                    LicensePlate = "305MIA",
                    Status = "sent",
                },
                new Notification
                {
                    TimeStamp = Convert.ToDateTime("4/19/2015 11:30am"),
                    Description = "Alarm is going off. Green Honda Accord 123ABC.",
                    LicensePlate = "123ABC",
                    Status = "sent",
                }
                );               
           
        }
    }
}
