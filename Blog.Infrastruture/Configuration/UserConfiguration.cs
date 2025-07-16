using Blog_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastruture.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(300); // Tamanho máximo do hash da senha, dependendo do algoritmo usado
            builder.HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
            builder.HasMany(u => u.Coments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
            builder.HasData(new User
            {
                UserId = 1,
                Username = "admin",
                Email = "teste@teste.com",
                PasswordHash = "AHIjjUiOw1IVwAau3589yIYYrlMf6mjnvu98HDhs36Kx7ZwEqYnCw72xklLO4yZ1gw==" //senha é teste

            });
            builder.HasOne(r => r.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<RefreshToken>(r => r.UserId);

        }
    }
}
