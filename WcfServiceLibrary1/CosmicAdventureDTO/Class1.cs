using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CosmicAdventureDTO
{
    [DataContract]
    public class SpaceSystem
    {
        [DataMember]
        public string Name { set; get; }
        public int MinShipPower { set; get; }
        [DataMember]
        public int BaseDistance { get; set; }
        public int Gold { set; get; }
    }
    [DataContract]
    public class Starship
    {
        [DataMember]
        public List<Person> Crew { get; set; }
        [DataMember]
        public int Gold { get; set; }
        [DataMember]
        public int ShipPower { get; set; }
    }
    public class Person
    {
        public string Name { get; set; }
        public string Nick { get; set; }
        public float Age { get; set; }
    }
}
