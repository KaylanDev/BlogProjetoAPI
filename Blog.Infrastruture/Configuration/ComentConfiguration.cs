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
            });
        }
    }
}
