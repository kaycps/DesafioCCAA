using DesafioCCAA.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Infrastructure
{
    public class DesafioCCAAContext : DbContext
    {
        public DesafioCCAAContext(DbContextOptions<DesafioCCAAContext> options)
        : base(options) { }

        public DbSet<Livro> Livros => Set<Livro>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Editora> Editora => Set<Editora>();
        public DbSet<ResetSenhaToken> ResetSenhaTokens => Set<ResetSenhaToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(builder =>
            {
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
                builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
                builder.Property(u => u.HashSenha).IsRequired();
                builder.HasIndex(u => u.Email).IsUnique();
                builder.HasMany(u => u.Livros);
            });

            modelBuilder.Entity<Livro>(builder =>
            {
                builder.HasKey(b => b.Id);
                builder.Property(b => b.Titulo).IsRequired().HasMaxLength(200);
                builder.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
                builder.Property(b => b.IdGenero).IsRequired();
                builder.Property(b => b.Autor).IsRequired().HasMaxLength(150);
                builder.Property(b => b.Sinopse).HasMaxLength(5000);
                builder.Property(b => b.FotoPath).HasMaxLength(300);
                builder.HasOne(b => b.Editora)
                       .WithMany()
                       .HasForeignKey(b => b.IdEditora);
                builder.HasOne(b=>b.Usuario)
                       .WithMany(b=>b.Livros)
                       .HasForeignKey(b => b.IdUsuario)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Editora>(builder =>
            {
                builder.HasKey(b => b.Id);
                builder.Property(b => b.Nome).IsRequired().HasMaxLength(200);

            });

            modelBuilder.Entity<ResetSenhaToken>(builder =>
            {
                builder.HasKey(b => b.Id);
                builder.HasOne(b => b.Usuario)
                    .WithMany()
                    .HasForeignKey(b => b.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
