using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Teste.Servico;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;
        public ITestOutputHelper SaidaConsoleTeste { get; set; }

        public AgenciaRepositorioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor executado com sucesso!");

            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaObterTodasAgencias()
        {
            // Arrange
            // Act
            List<Agencia> lista = _repositorio.ObterTodos();

            // Assert
            Assert.NotNull(lista);
            Assert.NotEqual(0, lista.Count);

        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            // Arrange
            // Act
            Agencia agencia = _repositorio.ObterPorId(1);

            // Assert
            Assert.NotNull(agencia);
        }

        [Fact]
        public void TestaExcecaoConsultaAgenciaPorId()
        {
            // Arrage
            // Act
            // Assert
            Assert.Throws<FormatException>(
                () => _repositorio.ObterPorId(50)
            );
        }

        [Fact]
        public void TestaAdicionarAgenciaMock()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agencia Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            // Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            // Assert
            Assert.True(adicionado);
        }

        [Fact]
        public void TestaObterAgenciasMock()
        {
            // Arrange
            var byteBankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = byteBankRepositorioMock.Object;

            // Act
            var lista = mock.BuscarAgencias();

            // Assert
            byteBankRepositorioMock.Verify(b => b.BuscarAgencias());

        }

        private void Dispose()
        {
            _repositorio.Dispose();
            SaidaConsoleTeste.WriteLine("Teste de Agência finalizados");
        }
    }
}
