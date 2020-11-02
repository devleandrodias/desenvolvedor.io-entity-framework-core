using System;
using System.Linq;
using DesenvolvedorIo.EFCoreIntroducao.Domain;
using DesenvolvedorIo.EFCoreIntroducao.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DesenvolvedorIo.EFCoreIntroducao
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var db = new Data.ApplicationContext();

            //var existe = db.Database.GetPendingMigrations().Any();

            //if (existe)
            //{
                // Executa regra de negocio caso haja migracoes pendentes...
            //}

            InserirDados();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "MacBook Air 2020",
                CodigoBarras = "3247623487926",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Valor = 1000m,
                Ativo = true,
            };

            using Data.ApplicationContext db = new Data.ApplicationContext();

            //db.Produtos.Add(produto);

            db.Set<Produto>().Add(produto);

            //db.Entry(produto).State = EntityState.Added;

            //db.Add(produto);

            int registros = db.SaveChanges();

            Console.WriteLine($"Total de registro(s): {registros}");
        }
    }
}
