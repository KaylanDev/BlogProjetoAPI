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
        }
    }
}
