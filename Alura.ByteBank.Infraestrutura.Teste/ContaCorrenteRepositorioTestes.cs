using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Teste.Servico;
using Alura.ByteBank.Infraestrutura.Teste.Servico.DTO;
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
    public class ContaCorrenteRepositorioTestes
    {
        public ITestOutputHelper SaidaConsoleTeste;
        private readonly IContaCorrenteRepositorio _repositorio;

        public ContaCorrenteRepositorioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");

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
            bool atualizado = _repositorio.Atualizar(conta.Id, conta);

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

        // Testes com Mock
        [Fact]
        public void TestaObterContasMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarContasCorrentes();

            //Assert - Verificando o comportamento
            bytebankRepositorioMock.Verify(b => b.BuscarContasCorrentes());
        }

        [Fact]
        public void TestaConsultaPixPorChaveMock()
        {
            //Arange
            var pixRepositorioMock = new Mock<IPixRepositorio>();
            var mock = pixRepositorioMock.Object;

            //Act
            var lista = mock.consultaPix(new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a"));

            //Assert - Verificando o comportamento
            pixRepositorioMock.Verify(b => b.consultaPix(new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a")));
        }

        [Fact]
        public void TestaConsultaPix()
        {
            // Arrange
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO()
            {
                Chave = guid,
                Saldo = 10
            };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;

            // Act
            var saldo = mock.consultaPix(guid).Saldo;

            // Assert
            Assert.Equal(10, saldo);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Destrutor invocado.");
        }
    }
}
