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
    class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.PostId);
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(2000);
            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); //se um usuario for deletado, seus posts também serão deletados

            builder.HasMany(p => p.Coments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId);

            builder.HasData(new Post
            {
                PostId = 1,
                Title = "Primeiro Post",
                Content = "Este é o conteúdo do primeiro post.",
                CreatedAt = new DateTime(2025, 06, 25),
                UpdatedAt = new DateTime(2025,06,25),
                UserId = 1 // Assumindo que o usuário com UserId 1 já existe
            });

        }
    }
}
