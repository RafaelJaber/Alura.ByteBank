using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Teste
{
    public class ClienteRepositorioTestes
    {
        [Fact]
        public void TestaObterTodosClientes()
        {
            // Arrange
            var _repositorio = new ClienteRepositorio();

            // Act
            List<Cliente> lista = _repositorio.ObterTodos();

            // Assert
            Assert.NotNull(lista);
            Assert.NotEqual(0,lista.Count);
        }
    }
}
