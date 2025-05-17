using DesafioCCAA.Domain.Interfaces;
using DesafioCCAA.Domain.Sevices;
using DesafioCCAA.Infrastructure.Repository;
using DesafioCCAA.Infrastructure.Repository.Livro;
using DesafioCCAA.Infrastructure.Repository.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DesafioCCAAContext>(options =>
                options.UseSqlServer(connectionString));

            
            //repositorios
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnityOfWork, UnityOfWorkRepository>();
            services.AddScoped<IjwtConfigProvider, JwtConfigProvider>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILivroSevice, LivroService>();
            services.AddScoped<IPdfService, PdfService>();

            return services;
        }
    }
}
