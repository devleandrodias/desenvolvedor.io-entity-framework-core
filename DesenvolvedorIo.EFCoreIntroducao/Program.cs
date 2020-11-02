using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DesenvolvedorIo.EFCoreIntroducao
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            var existe = db.Database.GetPendingMigrations().Any();

            if (existe)
            {
                // Executa regra de negocio caso haja migracoes pendentes...
            }

            Console.WriteLine("Hello World!");
        }
    }
}
