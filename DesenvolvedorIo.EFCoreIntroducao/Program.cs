using System;
using System.Collections.Generic;
using System.Linq;
using DesenvolvedorIo.EFCoreIntroducao.Data;
using DesenvolvedorIo.EFCoreIntroducao.Domain;
using DesenvolvedorIo.EFCoreIntroducao.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DesenvolvedorIo.EFCoreIntroducao
{
    public class Program
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

            //InserirDadosEmMassa();

            //ConsultarDados();

            //InserirDadosRelacionados();

            //ConsultarPedidosCarregamentoAdiantado();

            //AtualizarDados();

            RemoverDados();
        }

        private static void ConsultarDados()
        {
            ApplicationContext db = new ApplicationContext();

            //List<Cliente> consultaPorSintaxa = (from c in db.Clientes where c.Id > 0 select c).ToList();

            /**
             * O Entity Framework Core por padrao ao realizar uma consulta faz o Tracking dos dados obtidos p/ memoria
             * Quando for realizar uma consulta ele fara primeiro na memoria caso nao encontre, entao fara consulta
             * diretamente na sua base de dados. (Find eh o unico que faz consulta na memoria)
             * Utilizando o AsNoTracking ele nao trackeia os registros e toda consulta sera feita diretamente no 
             * banco de dados ao inves da memoria.
             */

            List<Cliente> consultaPorMetodo = db.Clientes
                .Where(x => x.Id > 0)
                .OrderBy(x => x.Id)
                .ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(x => x.Id == cliente.Id);
            }
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

        private static void InserirDadosRelacionados()
        {
            ApplicationContext db = new ApplicationContext();

            Cliente cliente = db.Clientes.FirstOrDefault();
            Produto produto = db.Produtos.FirstOrDefault();

            Pedido pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido de Teste",
                Status = StatusPedido.Finalizado,
                TipoFrete = TipoFrete.FOB,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quanitdade =1,
                        Valor = 10
                    }
                }
            };

            db.Set<Pedido>().Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarPedidosCarregamentoAdiantado()
        {
            ApplicationContext db = new ApplicationContext();

            // Carregamento Adiantado (Include)
            //List<Pedido> pedidos = db.Pedidos.Include("Itens").ToList();

            List<Pedido> pedidos = db.Pedidos
                .Include(x => x.Itens)
                .ThenInclude(y => y.Produto)
                .ToList();

            Console.WriteLine(pedidos);
        }

        private static void AtualizarDados()
        {
            ApplicationContext db = new ApplicationContext();

            Cliente cliente = db.Clientes.FirstOrDefault(x => x.Id == 2);

            cliente.Nome = "Leandro Dias Atalizado 2";

            var clienteDesconctado = new
            {
                Nome = "Cliente Desconctado",
                Telefone = "37593759357"
            };

            // Sobrescreve todas as informacoes para atualizar msm as que nao sofreram atualizacao
            //db.Clientes.Update(cliente);

            db.Entry(cliente).CurrentValues.SetValues(clienteDesconctado);

            db.SaveChanges();
        }

        private static void RemoverDados()
        {
            ApplicationContext db = new ApplicationContext();

            Cliente clienteDesconectado = new Cliente { Id = 3};

            //Cliente cliente = db.Clientes.Find(4);

            db.Clientes.Remove(clienteDesconectado);

            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            //db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }
    }
}
