using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Krooze.EntranceTest.WriteHere.Structure.Model
{
    /// <summary>
    /// Cruise Transfer Object
    /// </summary>
    /// <summary>
    /// Cruise Transfer Object
    /// </summary>    
    public class CruiseDTO
    {
        public string CruiseCode { get; set; }

        public decimal TotalValue { get; set; }

        public decimal CabinValue { get; set; }

        [XmlElement("PortChargesAmt")]
        public decimal PortCharge { get; set; }

        public string ShipName { get; set; }

        public List<PassengerCruiseDTO> PassengerCruise { get; set; }

        public bool IsThereDiscount() => LastCabinValue() < this.FirstCabinValue();
    
        private decimal FirstCabinValue() => PassengerCruise.First().Cruise.CabinValue;
       
        private decimal LastCabinValue()  => PassengerCruise.Last().Cruise.CabinValue;
        
        
    }
}
