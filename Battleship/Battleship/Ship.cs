using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum ShipType { Carrier = 5, Battleship = 4, Cruiser = 3, Submarine = 3, Minesweeper = 2 }

    class Ship
    {

        ShipType Type { get; set; }
        public List<Point> OccupiedPoints { get; set; }
        public int ShipLength { get; set; }
        public bool IsDestroyed
        {
            get
            {
                return OccupiedPoints.All(x => x.Status == PointStatus.Hit);
            }
        }
        public Ship(ShipType typeOfShip)
        {
            this.OccupiedPoints = new List<Point>();
            this.Type = typeOfShip;
            this.ShipLength = (int)Type;
        }
    }
    
}
