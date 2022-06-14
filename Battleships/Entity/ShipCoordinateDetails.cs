using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Entity
{
    public class ShipCoordinateDetails
    {
        public string Coordinate { get; set; } //Format is y:x
        public bool IsCoordinateHit { get; set; }
    }
}
