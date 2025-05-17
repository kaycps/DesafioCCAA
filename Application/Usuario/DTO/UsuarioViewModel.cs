

namespace DesafioCCAA.Application.Usuario.DTO
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }

        public UsuarioViewModel(Domain.Entity.Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Email = usuario.Email;
            DataNascimento = usuario.DataNascimento;

        }
    }
}
