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
    class ComentConfiguration : IEntityTypeConfiguration<Coment>
    {
        public void Configure(EntityTypeBuilder<Coment> builder)
        {
            builder.HasKey(c => c.ComentId);
            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);
            builder.HasOne(c => c.User)
                .WithMany(p => p.Coments)
                .HasForeignKey(c => c.UserId);
                

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Coments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new Coment
            {
                ComentId = 1,
                Content = "Este é o primeiro comentário.",
                CreatedAt = new DateTime(2025, 06, 25),
                PostId = 1, // Assumindo que o post com PostId 1 já existe
                UserId = 1 // Assumindo que o usuário com UserId 1 já existe
            },
          
    new Coment { ComentId = 2, Content = "Comentário 2", CreatedAt = new DateTime(2025, 7, 2), PostId = 2, UserId = 1 },
    new Coment { ComentId = 3, Content = "Comentário 3", CreatedAt = new DateTime(2025, 7, 3), PostId = 3, UserId = 1 },
    new Coment { ComentId = 4, Content = "Comentário 4", CreatedAt = new DateTime(2025, 7, 4), PostId = 4, UserId = 1 },
    new Coment { ComentId = 5, Content = "Comentário 5", CreatedAt = new DateTime(2025, 7, 5), PostId = 5, UserId = 1 },
    new Coment { ComentId = 6, Content = "Comentário 6", CreatedAt = new DateTime(2025, 7, 6), PostId = 6, UserId = 1 },
    new Coment { ComentId = 7, Content = "Comentário 7", CreatedAt = new DateTime(2025, 7, 7), PostId = 7, UserId = 1 },
    new Coment { ComentId = 8, Content = "Comentário 8", CreatedAt = new DateTime(2025, 7, 8), PostId = 8, UserId = 1 },
    new Coment { ComentId = 9, Content = "Comentário 9", CreatedAt = new DateTime(2025, 7, 9), PostId = 9, UserId = 1 },
    new Coment { ComentId = 10, Content = "Comentário 10", CreatedAt = new DateTime(2025, 7, 10), PostId = 10, UserId = 1 },
    new Coment { ComentId = 11, Content = "Comentário 11", CreatedAt = new DateTime(2025, 7, 11), PostId = 11, UserId = 1 },
    new Coment { ComentId = 12, Content = "Comentário 12", CreatedAt = new DateTime(2025, 7, 12), PostId = 12, UserId = 1 },
    new Coment { ComentId = 13, Content = "Comentário 13", CreatedAt = new DateTime(2025, 7, 13), PostId = 13, UserId = 1 },
    new Coment { ComentId = 14, Content = "Comentário 14", CreatedAt = new DateTime(2025, 7, 14), PostId = 14, UserId = 1 },
    new Coment { ComentId = 15, Content = "Comentário 15", CreatedAt = new DateTime(2025, 7, 15), PostId = 15, UserId = 1 }



            );
        }
    }
}
