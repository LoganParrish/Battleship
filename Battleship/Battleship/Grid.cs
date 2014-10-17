using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum PlaceShipDirection { Horizontal, Vertical };

    class Grid
    {
        
        public Point[,] Ocean { get; set; }
        List<Ship> ListOfShips { get; set; }
        bool AllShipsDestroyed 
        {
            get
            {
                return ListOfShips.All(x => x.IsDestroyed);
            }
        }
        int CombatRound { get; set; }

        //define constructor
        public Grid()
        {
            this.ListOfShips = new List<Ship>();
            //initialize the ocean
            this.Ocean = new Point[10, 10];
            //this.Ocean[1, 1] = new Point(1, 1, Point.PointStatus.Empty);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    this.Ocean[i, j] = new Point(i, j, PointStatus.Empty);
                }
            }

            
        }

        public void PlaceShip(Ship shipToPlace, PlaceShipDirection direction, int startX, int startY)
        {
            for (int i = 0; i < shipToPlace.ShipLength; i++)
			{
                Ocean[startX, startY].Status = PointStatus.Ship;
                shipToPlace.OccupiedPoints.Add(Ocean[startX, startY]);
                if (direction == PlaceShipDirection.Horizontal)
                {
                    startX++;
                }
                else
                {
                    startY++;
                }
			}
            this.ListOfShips.Add(shipToPlace);
        }
        //method to display the 'ocean' grid
        public void DisplayOcean()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("     0  1  2  3  4  5  6  7  8  9  X");
            Console.WriteLine("===================================");
            Console.ResetColor();

            //loop for writing out the coordinates on separate
            for (int y = 0; y < 10; y++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(y + " ||");
                Console.ResetColor();

                for (int x = 0; x < 10; x++)
                {
                    if (Ocean[x, y].Status == PointStatus.Empty || Ocean[x, y].Status == PointStatus.Ship)
                    {
                        Console.Write("[ ]");
                    }
                    else if (Ocean[x, y].Status == PointStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[X]");
                        Console.ResetColor();
                    }
                    else if (Ocean[x, y].Status == PointStatus.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[O]");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Y ||");
            Console.ResetColor();
        }
        public void Target(int x, int y)
        {
            if (Ocean[x, y].Status == PointStatus.Ship)
            {
                Ocean[x, y].Status = PointStatus.Hit;
            }
            else if (Ocean [x, y].Status == PointStatus.Empty)
            {
                Ocean[x, y].Status = PointStatus.Miss;
            }
            
        }
        public void PlayGame()
        {
            while (!AllShipsDestroyed)
            {
                DisplayOcean();

                string xException = "", yException = "";

                while (xException == "")
                {
                    Console.WriteLine("Enter your x coordinate.");
                    xException = Console.ReadLine();
                }
                
                int x = int.Parse(xException);

                while (yException == "")
                {
                    Console.WriteLine("Enter your y coordinate.");
                    yException = Console.ReadLine();
                }

                int y = int.Parse(yException);

                if (x <= 9 && y <= 9)
                {
                    Target(x, y);
                    CombatRound++;
                }
                Console.Clear();
            }
            DisplayOcean();
            Console.WriteLine("Congratulations, you won!");
            Console.WriteLine("It took " + CombatRound + " turns for you to FINALLY finish...");
            AddHighScore(CombatRound);
            DisplayHighScore();
        }
        static void AddHighScore(int playerScore)
        {
            Console.WriteLine("Add your name to the highscores: ");
            string playerName = Console.ReadLine();

            LoganEntities1 db = new LoganEntities1();

            HighScore newHighScore = new HighScore();
            newHighScore.DateCreated = DateTime.Now;
            newHighScore.Name = playerName;
            newHighScore.Game = "Battleship";
            newHighScore.Score = playerScore;

            db.HighScores.Add(newHighScore);

            db.SaveChanges();
        }
        static void DisplayHighScore()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================== HIGH SCORES ====================");
            Console.WriteLine("=====================================================");
            Console.WriteLine("\n");
            Console.ResetColor();

            LoganEntities1 db = new LoganEntities1();
            List<HighScore> highScoreList = db.HighScores.Where(x => x.Game == "Battleship").OrderBy(x => x.Score).Take(10).ToList();

            foreach (HighScore highScore in highScoreList)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0}. {1} - Took {2} turns to sink all ships  - {3}", highScoreList.IndexOf(highScore) + 1, highScore.Name, highScore.Score, highScore.DateCreated.Value.ToShortDateString());
                Console.ResetColor();
            }
        }
    }
}