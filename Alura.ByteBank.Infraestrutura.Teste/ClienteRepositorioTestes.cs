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
using Xunit.Abstractions;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repositorio;
        public ITestOutputHelper SaidaConsoleTeste { get; set; }

        public ClienteRepositorioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor executado com sucesso!");

            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TestaObterTodosClientes()
        {
            // Arrange
            // Act
            List<Cliente> lista = _repositorio.ObterTodos();

            // Assert
            Assert.NotNull(lista);
            Assert.NotEqual(0, lista.Count);
        }

        [Fact]
        public void TestaObterClientePorId()
        {
            // Arrange
            // Act
            Cliente cliente = _repositorio.ObterPorId(1);

            // Assert
            Assert.NotNull(cliente);
        }

        [Fact]
        public void TestaObertClientePorGuid()
        {
            // Arrange
            Cliente clienteId = _repositorio.ObterPorId(1);

            // Act
            Cliente clienteGuid = _repositorio.ObterPorGuid(clienteId.Identificador);

            // Assert
            Assert.Equal(clienteId.CPF, clienteGuid.CPF);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterClientesPorVariosIds(int id)
        {
            // Arrange
            // Act
            var cliente = _repositorio.ObterPorId(id);

            // Assert
            Assert.NotNull(cliente);
        }

        private void Dispose()
        {
            _repositorio.Dispose();
            SaidaConsoleTeste.WriteLine("Teste de Agência finalizados");
        }
    }
}
