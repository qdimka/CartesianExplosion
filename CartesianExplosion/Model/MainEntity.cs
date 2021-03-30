using System.Collections.Generic;

namespace CartesianExplosion.Model
{
    public class MainEntity
    {
        public int ID { get; set; }
        public List<RefEntity1> Ref1 { get; set; }
        public List<RefEntity2> Ref2 { get; set; }
        public List<RefEntity3> Ref3 { get; set; }
        public List<RefEntity4> Ref4 { get; set; }
        public List<RefEntity5> Ref5 { get; set; }
        public List<BigRefEntity> BigRef { get; set; }
    }
}