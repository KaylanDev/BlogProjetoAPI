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
            },
            

    new Post { PostId = 2, Title = "Post 2", Content = "Conteúdo do post 2", CreatedAt = new DateTime(2025, 6, 26), UpdatedAt = new DateTime(2025, 6, 26), UserId = 1 },
    new Post { PostId = 3, Title = "Post 3", Content = "Conteúdo do post 3", CreatedAt = new DateTime(2025, 6, 27), UpdatedAt = new DateTime(2025, 6, 27), UserId = 1 },
    new Post { PostId = 4, Title = "Post 4", Content = "Conteúdo do post 4", CreatedAt = new DateTime(2025, 6, 28), UpdatedAt = new DateTime(2025, 6, 28), UserId = 1 },
    new Post { PostId = 5, Title = "Post 5", Content = "Conteúdo do post 5", CreatedAt = new DateTime(2025, 6, 29), UpdatedAt = new DateTime(2025, 6, 29), UserId = 1 },
    new Post { PostId = 6, Title = "Post 6", Content = "Conteúdo do post 6", CreatedAt = new DateTime(2025, 6, 30), UpdatedAt = new DateTime(2025, 6, 30), UserId = 1 },
    new Post { PostId = 7, Title = "Post 7", Content = "Conteúdo do post 7", CreatedAt = new DateTime(2025, 7, 1), UpdatedAt = new DateTime(2025, 7, 1), UserId = 1 },
    new Post { PostId = 8, Title = "Post 8", Content = "Conteúdo do post 8", CreatedAt = new DateTime(2025, 7, 2), UpdatedAt = new DateTime(2025, 7, 2), UserId = 1 },
    new Post { PostId = 9, Title = "Post 9", Content = "Conteúdo do post 9", CreatedAt = new DateTime(2025, 7, 3), UpdatedAt = new DateTime(2025, 7, 3), UserId = 1 },
    new Post { PostId = 10, Title = "Post 10", Content = "Conteúdo do post 10", CreatedAt = new DateTime(2025, 7, 4), UpdatedAt = new DateTime(2025, 7, 4), UserId = 1 },
    new Post { PostId = 11, Title = "Post 11", Content = "Conteúdo do post 11", CreatedAt = new DateTime(2025, 7, 5), UpdatedAt = new DateTime(2025, 7, 5), UserId = 1 },
    new Post { PostId = 12, Title = "Post 12", Content = "Conteúdo do post 12", CreatedAt = new DateTime(2025, 7, 6), UpdatedAt = new DateTime(2025, 7, 6), UserId = 1 },
    new Post { PostId = 13, Title = "Post 13", Content = "Conteúdo do post 13", CreatedAt = new DateTime(2025, 7, 7), UpdatedAt = new DateTime(2025, 7, 7), UserId = 1 },
    new Post { PostId = 14, Title = "Post 14", Content = "Conteúdo do post 14", CreatedAt = new DateTime(2025, 7, 8), UpdatedAt = new DateTime(2025, 7, 8), UserId = 1 },
    new Post { PostId = 15, Title = "Post 15", Content = "Conteúdo do post 15", CreatedAt = new DateTime(2025, 7, 9), UpdatedAt = new DateTime(2025, 7, 9), UserId = 1 }
);


        }
    }
}
