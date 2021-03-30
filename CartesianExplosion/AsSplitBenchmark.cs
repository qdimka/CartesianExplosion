using System;
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

        [Params(false,true)]
        public bool SplitQueries { get; set; }
        
        [Params(0,1,2,3,4,5,6)]
        public int LoadRefs { get; set; }

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
        public List<MainEntity> QueryLoad()
        {
            IQueryable<MainEntity> query = LoadRefs switch
            {
                0 => dbContext.MainEntities,
                1 => dbContext.MainEntities
                    .Include(x => x.Ref1),
                2 => dbContext.MainEntities
                    .Include(x => x.Ref1)
                    .Include(x => x.Ref2),
                3 => dbContext.MainEntities
                    .Include(x => x.Ref1)
                    .Include(x => x.Ref2)
                    .Include(x => x.Ref3),
                4 => dbContext.MainEntities
                    .Include(x => x.Ref1)
                    .Include(x => x.Ref2)
                    .Include(x => x.Ref3)
                    .Include(x => x.Ref4),
                5 => dbContext.MainEntities
                    .Include(x => x.Ref1)
                    .Include(x => x.Ref2)
                    .Include(x => x.Ref3)
                    .Include(x => x.Ref4)
                    .Include(x => x.Ref5),
                6 => dbContext.MainEntities
                    .Include(x => x.Ref1)
                    .Include(x => x.Ref2)
                    .Include(x => x.Ref3)
                    .Include(x => x.Ref4)
                    .Include(x => x.Ref5)
                    .Include(x => x.BigRef),
                _ => throw new ArgumentOutOfRangeException()
            };
            var splitQuery = SplitQueries ? query.AsSplitQuery() : query;
            return splitQuery.Take(2).ToList();
        }

        [GlobalCleanup]
        public void Teardown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}