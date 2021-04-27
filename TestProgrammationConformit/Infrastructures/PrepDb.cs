using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProgrammationConformit.Infrastructures
{
    public class PrepDb
    {
        public static void PrepEnvironment(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                AutoMigrate(serviceScope.ServiceProvider.GetService<ConformitContext>());
            }
        }

        public static void AutoMigrate(ConformitContext context)
        {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();
            System.Console.WriteLine("Finished applying migrations...");

            context.SaveChanges();
        }
    }
}