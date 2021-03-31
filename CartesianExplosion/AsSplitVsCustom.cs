using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using CartesianExplosion.Model;
using Microsoft.EntityFrameworkCore;

namespace CartesianExplosion
{
    public class AsSplitVsCustom
    {
        private MyDataContext dbContext;
        
        
        [Params(2,5,10,100)]
        public int ItemsInCollection { get; set; }

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
        public MainEntity AsSplitQuery()
        {
            var entry = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Where(x => x.ID == 2)
                .AsSplitQuery().Single();
            return entry;
        }

        [Benchmark]
        public MainEntity CustomQueries()
        {
            var mainEntry = dbContext.MainEntities.Single(x => x.ID == 2);
            mainEntry.Ref1 = dbContext.Ref1s.Where(x => x.MainEntityID == 2).ToList();
            mainEntry.Ref2 = dbContext.Ref2s.Where(x => x.MainEntityID == 2).ToList();
            mainEntry.Ref3 = dbContext.Ref3s.Where(x => x.MainEntityID == 2).ToList();
            mainEntry.Ref4 = dbContext.Ref4s.Where(x => x.MainEntityID == 2).ToList();
            mainEntry.Ref5 = dbContext.Ref5s.Where(x => x.MainEntityID == 2).ToList();
            return mainEntry;
        }
        
        [GlobalCleanup]
        public void Teardown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}