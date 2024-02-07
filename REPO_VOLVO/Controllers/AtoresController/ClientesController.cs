using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas aos clientes.
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        // Método para cadastrar um novo cliente.
        [HttpPost("Cadastrar")]
        public IActionResult PostCliente([FromForm] Cliente cliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Verifica se o documento identificador do cliente é único no contexto.
                ValidationHelper.CheckUniqueDoc(_context, cliente.DocIdentificadorCliente);
                // Valida os formatos dos dados do cliente.
                ValidationHelper.ValidateNameFormat(cliente.NomeCliente, "Nome inválido.");
                ValidationHelper.ValidateNumericFormat(cliente.DocIdentificadorCliente, "Formato do Documento Identificador inválido.");
                ValidationHelper.ValidateEmailFormat(cliente.EmailCliente, "Email inválido.");
                ValidationHelper.ValidateNumericFormat(cliente.NumeroContatoCliente, "Formato de telefone inválido.");
                try
                {
                    // Define o código do cliente como 0 para que seja gerado automaticamente.
                    cliente.CodCliente = 0;
                    // Define o cliente como ativo.
                    cliente.ClienteAtivo = true;
                    // Adiciona o cliente ao contexto e salva as mudanças no banco de dados.
                    _context.Clientes.Add(cliente);
                    _context.SaveChanges();
                    return Ok("Cliente cadastrado com sucesso.");
                }
                catch (Exception e)
                {
                    // Verifica se ocorreu uma exceção de chave única duplicada e lança uma exceção personalizada.
                    if (e.InnerException.Message == "Cannot insert duplicate key row in object 'dbo.Clientes' with unique index 'IX_Clientes_DocIdentificadorCliente'. The duplicate key value is (123123).")
                    {
                        throw new DuplicateUniqueValueException($"O Documento '{cliente.DocIdentificadorCliente}' já está cadastrado. Tente Novamente.");
                    }
                    throw;
                }
            }
        }

        // Método para listar todos os clientes.
        [HttpGet("Listar")]
        public List<Cliente> GetTodosClientes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Clientes.ToList();
            }
        }

        // Método para buscar um cliente pelo documento identificador.
        [HttpGet("Buscar/{Documento}")]
        public IActionResult GetCliente(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                return new ObjectResult(item);
            }
        }

        // Método para atualizar os dados de um cliente pelo documento identificador.
        [HttpPut("Atualizar/{Documento}")]
        public IActionResult PutCliente(string Documento, [FromForm] Cliente cliente)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                // Valida os formatos dos dados do cliente.
                ValidationHelper.ValidateNameFormat(cliente.NomeCliente, "Nome inválido.");
                ValidationHelper.ValidateEmailFormat(cliente.EmailCliente, "Email inválido.");
                ValidationHelper.ValidateNumericFormat(cliente.NumeroContatoCliente, "Formato de telefone inválido.");
                try
                {
                    // Atualiza os dados do cliente no contexto e salva as mudanças no banco de dados.
                    item.NomeCliente = cliente.NomeCliente;
                    item.EmailCliente = cliente.EmailCliente;
                    item.NumeroContatoCliente = cliente.NumeroContatoCliente;
                    _context.SaveChanges();
                    return Ok("Os dados do cliente foram atualizados.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para desativar um cliente pelo documento identificador.
        [HttpPut("Desativar/{Documento}")]
        public IActionResult PutDeleteCliente(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                // Desativa os caminhões associados ao cliente.
                var caminhaoCliente = _context.Caminhoes.Where(f => f.FkClientesCodCliente == item.CodCliente);
                foreach (var caminhao in caminhaoCliente)
                {
                    caminhao.CaminhaoAtivo = false;
                }
                // Define o cliente como inativo e salva as mudanças no banco de dados.
                item.ClienteAtivo = false;
                _context.SaveChanges();
                return Ok("O cliente foi desativado.");
            }
        }

        // Método para deletar um cliente pelo documento identificador.
        [HttpDelete("Deletar/{Documento}")]
        public IActionResult DeleteCliente(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Clientes.FirstOrDefault(t => t.DocIdentificadorCliente == Documento);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cliente com esse documento foi encontrado.");
                }
                // Registra a deleção do cliente.
                ManipulacaoDadosHelper.RegistrarDelete("Clientes", "Cliente", item);
                // Remove o cliente do contexto e salva as mudanças no banco de dados.
                _context.Clientes.Remove(item);
                _context.SaveChanges();
                return Ok("O cliente foi deletado.");
            }
        }
    }
}
