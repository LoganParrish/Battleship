using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            //declare 5 ships

            Ship submarine = new Ship(ShipType.Submarine);
            Ship minesweeper = new Ship(ShipType.Minesweeper);
            Ship cruiser = new Ship(ShipType.Cruiser);
            Ship battleship = new Ship(ShipType.Battleship);
            Ship carrier = new Ship(ShipType.Carrier);

            //Place ship in the ocean
            grid.PlaceShip(submarine, PlaceShipDirection.Vertical, 7, 3);
            grid.PlaceShip(minesweeper, PlaceShipDirection.Horizontal, 0, 4);
            grid.PlaceShip(cruiser, PlaceShipDirection.Horizontal, 6, 7);
            grid.PlaceShip(battleship, PlaceShipDirection.Vertical, 4, 2);
            grid.PlaceShip(carrier, PlaceShipDirection.Horizontal, 2, 1);

            grid.PlayGame();
            
            Console.ReadKey();
        }
        
    }
}
