﻿using CarForum.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Infrastructure.Persistence.Context
{
     public  class CarForumContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
        public CarForumContext()
        {

        }
        public CarForumContext( DbContextOptions opt):base(opt)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public DbSet<EntryComment> EntryComment { get; set; }
        public DbSet<EntryFavorite> EntryFavorites { get; set; }
        public DbSet<EntryVote> EntryVotes { get; set; }

        public DbSet<EntryCommentFavorite>EntryCommentFavorites{ get; set; }
        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "Server=LAPTOP-BEKO;initial Catalog=CarForum;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(conStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public override int SaveChanges()
        {

            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {

            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }
         
        private void OnBeforeSave()
        {
            var addedEntities=ChangeTracker.Entries()
                .Where(i=>i.State==EntityState.Added)
                .Select(i=>(BaseEntity)i.Entity);

            PrepareAddedEntities(addedEntities);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity>entities)
        {
            foreach (var entity in entities)
            {
                if(entity.CreateDate==DateTime.MinValue)
                     entity.CreateDate = DateTime.Now;
            }
        }








    }
}
