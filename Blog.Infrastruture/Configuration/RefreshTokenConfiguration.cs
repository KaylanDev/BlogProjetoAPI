using Blog_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Configuration
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(500); // Tamanho máximo do token, ajuste conforme necessário
            builder.Property(x => x.Expiration)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();
            builder.Property(x => x.IsRevoked)
                .IsRequired()
                .HasDefaultValue(false); // Define o valor padrão como false
            builder.HasOne(x => x.User)
                .WithOne(u => u.RefreshToken) // Assumindo que a propriedade de navegação é RefreshTokens
                .HasForeignKey<User>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Se um usuário for deletado, seus tokens também serão deletados
        }
    }
}
