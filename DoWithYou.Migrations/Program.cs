using System;
using DoWithYou.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DoWithYou.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Run Migration? (Y/N): ");
            char check = Console.ReadKey(false).KeyChar.ToString().ToLower()[0];

            if (check != 'y')
                return;

            Console.WriteLine();
            Console.WriteLine("Running...");
            try
            {
                using (DoWithYouContext context = new Data.Factories.DoWithYouContextFactory().CreateDbContext(args))
                {
                    context.Database.Migrate();
                    context.SaveChanges();
                }

                Console.WriteLine("Complete!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAILED!");
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
