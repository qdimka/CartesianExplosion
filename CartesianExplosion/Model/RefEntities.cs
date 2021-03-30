namespace CartesianExplosion.Model
{
    public abstract class RefEntity
    {
        public int ID { get; set; }
        public string Payload { get; set; } = string.Empty;
        
        public MainEntity MainEntity { get; set; }
        public int? MainEntityID { get; set; }
    }
    
    public class RefEntity1 : RefEntity { }
    public class RefEntity2 : RefEntity { }
    public class RefEntity3 : RefEntity { }
    public class RefEntity4 : RefEntity { }
    public class RefEntity5 : RefEntity { }
    public class BigRefEntity : RefEntity { }

}