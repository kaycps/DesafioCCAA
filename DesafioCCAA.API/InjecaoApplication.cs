using DesafioCCAA.Application.Livro;
using DesafioCCAA.Application.Login;
using DesafioCCAA.Application.Usuario;
using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Domain.Sevices;

namespace DesafioCCAA.API
{
    public static class InjecaoApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAutenticacaoAppService, AutenticacaoAppService>();
            services.AddScoped<ILivroAppService, LivroAppService>();

            return services;
        }
    }
}
