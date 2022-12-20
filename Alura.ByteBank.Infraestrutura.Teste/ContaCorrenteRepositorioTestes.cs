using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repositorio;
        
        public ContaCorrenteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IContaCorrenteRepositorio>();
        }

        [Fact]
        public void TestaObterConstasCorrentes()
        {
            // Arrange
            // Act
            List<ContaCorrente> lista = _repositorio.ObterTodos();

            Assert.NotNull(lista);
            Assert.NotEqual(0, lista.Count);
        }

        [Fact]
        public void TestaObterContaCorretePorId()
        {
            // Arrange
            // Act
            ContaCorrente conta = _repositorio.ObterPorId(1);

            // Assert
            Assert.NotNull(conta);
        }

        [Fact]
        public void TestaObterContaCorretePorGuid()
        {
            // Arrange
            ContaCorrente contaId = _repositorio.ObterPorId(1);

            // Act
            ContaCorrente contaGuid = _repositorio.ObterPorGuid(contaId.Identificador);

            // Assert
            Assert.Equal(contaId.Numero, contaGuid.Numero);

        }

        [Fact]
        public void TestaAtualizaSaldoDeterminadaConta()
        {
            // Arrange
            ContaCorrente conta = _repositorio.ObterPorId(2);
            double saldoNovo = 15;
            conta.Saldo += saldoNovo;

            // Act
            bool atualizado = _repositorio.Atualizar(conta.Id,conta);

            // Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaAdicionarContaCorrente()
        {
            // Arrange
            ContaCorrente conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário"
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Endereco = "Rua das Flores,25",
                    Numero = 147
                }
            };

            // Act
            bool retorno = _repositorio.Adicionar(conta);

            // Assert
            Assert.True(retorno);
        }

        
    }
}
