using System;
using System.Linq;
using CartesianExplosion.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CartesianExplosion
{
    public class MyDataContext : DbContext
    {
        private readonly int itemsInCollection;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var csb = new NpgsqlConnectionStringBuilder()
            {
                Database = "split_benchmark",
                Host = "localhost",
                Port = 5432,
                Username = "",
                Password = "",
                IncludeErrorDetails = true,
                Pooling = true,
                CommandTimeout = 600
            };
            optionsBuilder.UseNpgsql(csb.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefEntity1>().HasKey(x => x.ID);
            modelBuilder.Entity<RefEntity1>()
                .HasOne(x => x.MainEntity)
                .WithMany(x => x.Ref1)
                .HasForeignKey(x => x.MainEntityID);
            
            modelBuilder.Entity<RefEntity2>().HasKey(x => x.ID);
            modelBuilder.Entity<RefEntity2>()
                .HasOne(x => x.MainEntity)
                .WithMany(x => x.Ref2)
                .HasForeignKey(x => x.MainEntityID);
            
            modelBuilder.Entity<RefEntity3>().HasKey(x => x.ID);
            modelBuilder.Entity<RefEntity3>()
                .HasOne(x => x.MainEntity)
                .WithMany(x => x.Ref3)
                .HasForeignKey(x => x.MainEntityID);
            
            modelBuilder.Entity<RefEntity4>().HasKey(x => x.ID);
            modelBuilder.Entity<RefEntity4>()
                .HasOne(x => x.MainEntity)
                .WithMany(x => x.Ref4)
                .HasForeignKey(x => x.MainEntityID);
            
            modelBuilder.Entity<RefEntity5>().HasKey(x => x.ID);
            modelBuilder.Entity<RefEntity5>()
                .HasOne(x => x.MainEntity)
                .WithMany(x => x.Ref5)
                .HasForeignKey(x => x.MainEntityID);
            
            modelBuilder.Entity<BigRefEntity>().HasKey(x => x.ID);
            modelBuilder.Entity<BigRefEntity>()
                .HasOne(x => x.MainEntity)
                .WithMany(x => x.BigRef)
                .HasForeignKey(x => x.MainEntityID);
            
            modelBuilder.Entity<MainEntity>().HasKey(x => x.ID);
            var mainEntities = Enumerable.Range(1, 5).Select(i => new MainEntity{ID = i}).ToArray();
            modelBuilder.Entity<MainEntity>().HasData(mainEntities);

            var arrPow = itemsInCollection * 5;
            SeedRef<RefEntity1>(modelBuilder, arrPow, 10);
            SeedRef<RefEntity2>(modelBuilder, arrPow, 10);
            SeedRef<RefEntity3>(modelBuilder, arrPow, 10);
            SeedRef<RefEntity4>(modelBuilder, arrPow, 10);
            SeedRef<RefEntity5>(modelBuilder, arrPow, 10);
            SeedRef<BigRefEntity>(modelBuilder, arrPow, 1000);
        }
        
        public DbSet<MainEntity> MainEntities { get; set; }
        public DbSet<RefEntity1> Ref1s { get; set; }
        public DbSet<RefEntity2> Ref2s { get; set; }
        public DbSet<RefEntity3> Ref3s { get; set; }
        public DbSet<RefEntity4> Ref4s { get; set; }
        public DbSet<RefEntity5> Ref5s { get; set; }

        private void SeedRef<T>(ModelBuilder modelBuilder, int cnt, int dataLen) where T : RefEntity, new()
        {
            var entities = Enumerable.Range(1, cnt)
                .Select(i => new T
                {
                    ID = i,
                    Payload = RndString(dataLen),
                    MainEntityID = (i-1)/itemsInCollection + 1
                })
                .ToArray();
            modelBuilder.Entity<T>().HasData(entities);
        }

        private static Random rnd = new Random();
        private static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public MyDataContext(int itemsInCollection)
        {
            this.itemsInCollection = itemsInCollection;
        }

        private static string RndString(int len) => 
            new (Enumerable
                    .Range(1, len)
                    .Select(_ => chars[rnd.Next(chars.Length - 1)])
                    .ToArray());
    }
}