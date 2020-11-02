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

            //InserirDados();

            InserirDadosEmMassa();
        }

        private static void InserirDadosEmMassa()
        {
            Produto produto = new Produto()
            {
                Ativo = true,
                CodigoBarras = "274863983746",
                Descricao = "MacBook Pro 2018",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Valor = 1999.99m
            };

            //Cliente cliente = new Cliente
            //{
            //    Nome = "Leandro Dias",
            //    CEP = "14305092",
            //    Cidade = "São Paulo",
            //    Estado = "SP",
            //    Telefone = "46897654386"
            //};

            var listaCientes = new[] {
                    new Cliente
                {
                    Nome = "Leandro Dias 2",
                    CEP = "14305092",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "46897654386"
                },
                    new Cliente
                {
                    Nome = "Leandro Dias 3",
                    CEP = "14305092",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "46897654386"
                }
            };

            using Data.ApplicationContext db = new Data.ApplicationContext();

            //db.AddRange(produto, cliente);

            db.AddRange(listaCientes);

            int registros = db.SaveChanges();

            Console.WriteLine($"Total de registro(s): {registros}");
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
