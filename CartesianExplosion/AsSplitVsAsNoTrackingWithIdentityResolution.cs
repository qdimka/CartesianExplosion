using System.Linq;
using BenchmarkDotNet.Attributes;
using CartesianExplosion.Model;
using Microsoft.EntityFrameworkCore;

namespace CartesianExplosion
{
    public class AsSplitVsAsNoTrackingWithIdentityResolution
    {
        private MyDataContext dbContext;
        
        [Params(2, 5)]
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
                .AsSplitQuery()
                .Single();
            
            return entry;
        }
        
        [Benchmark]
        public MainEntity AsQuery()
        {
            var mainEntry = dbContext.MainEntities
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Include(x => x.BigRef)
                .Single(x => x.ID == 2);
            
            return mainEntry;
        }

        [Benchmark]
        public MainEntity AsNoTrackingWithIdentityResolution()
        {
            var mainEntry = dbContext.MainEntities
                .AsNoTrackingWithIdentityResolution()
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Include(x => x.BigRef)
                .Single(x => x.ID == 2);
            
            return mainEntry;
        }
        
        [Benchmark]
        public MainEntity AsNoTracking()
        {
            var mainEntry = dbContext.MainEntities
                .AsNoTracking()
                .Include(x => x.Ref1)
                .Include(x => x.Ref2)
                .Include(x => x.Ref3)
                .Include(x => x.Ref4)
                .Include(x => x.Ref5)
                .Include(x => x.BigRef)
                .Single(x => x.ID == 2);
            
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