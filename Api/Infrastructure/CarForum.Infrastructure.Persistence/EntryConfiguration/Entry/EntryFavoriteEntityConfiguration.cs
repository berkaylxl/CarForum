using CarForum.Api.Domain.Models;
using CarForum.Infrastructure.Persistence.Context;
using CarForum.Infrastructure.Persistence.EntryConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarForum.Infrastructure.Persistence.EntryConfiguration.Entry
{
    public class EntryFavoriteEntityConfiguration:BaseEntityConfiguration<EntryFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryfavorite", CarForumContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.Entry)
                   .WithMany(i=>i.EntryFavorites)
                   .HasForeignKey(i => i.EntryId);

            builder.HasOne(i => i.CreatedUser)
                .WithMany(i => i.EntryFavorites)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
          
        }
           
    }
}
