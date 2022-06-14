using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Entity
{
    public class ShipDetails
    {
        public List<ShipCoordinateDetails> ShipCoordinateList { get; set; }
        public EnumShipAlignment ShipAlignment { get; set; }
    }
}
