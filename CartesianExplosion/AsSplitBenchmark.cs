using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CartesianExplosion.Model;
using Microsoft.EntityFrameworkCore;

namespace CartesianExplosion
{
    public class AsSplitBenchmark
    {
        private MyDataContext dbContext;
        
        
        [Params(2,5,10)]
        public int ItemsInCollection { get; set; }
        
        [Params(1,3,5)]
        public int Limit { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            dbContext = new MyDataContext(ItemsInCollection);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        [IterationSetup]
        public void IterCleanup()
        {
            dbContext.ChangeTracker.Clear();
        }
        
        [Benchmark]
        public List<MainEntity> Refs0_Single()
        {
            var entries = dbContext.MainEntities.Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs0_Splitted()
        {
            var entries = dbContext.MainEntities.Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs1_Single()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs1_Splitted()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs2_Single()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs2_Splitted()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        [Benchmark]
        public List<MainEntity> Refs3_Single()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs3_Splitted()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        [Benchmark]
        public List<MainEntity> Refs4_Single()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs4_Splitted()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        [Benchmark]
        public List<MainEntity> Refs5_Single()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs5_Splitted()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        [Benchmark]
        public List<MainEntity> Refs6WithBig_Single()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Include(x => x.BigRef)
                .Take(Limit).ToList();
            return entries;
        }
        
        [Benchmark]
        public List<MainEntity> Refs6WithBig_Splitted()
        {
            var entries = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Include(x => x.BigRef)
                .Take(Limit).AsSplitQuery().ToList();
            return entries;
        }
        [GlobalCleanup]
        public void Teardown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}