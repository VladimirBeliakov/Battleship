using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class RealPlayer : Player
    {
        public int HitI { get; set; }
        public int HitJ { get; set; }

        public int Ship1Settled { get; set; }
        public int Ship2Settled { get; set; }
        public int Ship3Settled { get; set; }

        public List<Ship> ships { get; set; }

        public void InitializeTheShips()
        {
            ships = new List<Ship>();
            ships.Add(new Ship());
            ships.Add(new Ship());
            ships.Add(new Ship());
        }

        public void PutTheShipOnTheBoard(int number, string[,] board)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Player {1}, please enter a vertical coordinate from 0 to {0} to set your first ship.", board.GetLength(0) - 1, number);
                    ships[0].I = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a horizontal coordinate from 0 to {0} to set first your ship.", board.GetLength(0) - 1, number);
                    ships[0].J = Int32.Parse(Console.ReadLine());

                    if (Board.CheckIfOutOfRange(ships[0].I, ships[0].J, board))
                    {
                        Console.WriteLine("The coordinates must be from 0 to {0}", board.GetLength(0) - 1);
                        Console.WriteLine();
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("This is not a valid input.");
                    Console.WriteLine();
                    continue;
                }
                break;
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Player {1}, please enter a vertical coordinate from 0 to {0} " +
                                      "to set the first grid of your second ship.", board.GetLength(0) - 1, number);
                    ships[1].I = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a horizontal coordinate from 0 to {0} " +
                                      "to set the first grid of your second ship.", board.GetLength(0) - 1, number);
                    ships[1].J = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a vertical coordinate from 0 to {0} " +
                                      "to set the second grid of your second ship.", board.GetLength(0) - 1, number);
                    ships[2].I = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a horizontal coordinate from 0 to {0} " +
                                      "to set the second grid of your second ship.", board.GetLength(0) - 1, number);
                    ships[2].J = Int32.Parse(Console.ReadLine());

                    if (Board.CheckIfOutOfRange(ships[1].I, ships[1].J, board) ||
                        Board.CheckIfOutOfRange(ships[2].I, ships[2].J, board))
                    {
                        Console.WriteLine("The coordinates must be from 0 to {0}", board.GetLength(0) - 1);
                        Console.WriteLine();
                        continue;
                    }

                    if (Ship.CheckTheSecondGridOfTheShip(ships[1].I, ships[1].J, ships[2].I, ships[2].J) == false)
                    {
                        Console.WriteLine("The grids of the second ship do not form a vertical or horizontal line. Please enter different coordinats.");
                        Console.WriteLine();
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("This is not a valid input.");
                    Console.WriteLine();
                    continue;
                }
                break;
            }
        }

        public void HitTheBoard(int number, string[,] board)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Player {1}, pleast enter a vertical coordinate from 0 to {0} to hit the board.", board.GetLength(0) - 1, number);
                    HitI = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, pleast enter a horizontal coordinate from 0 to {0} to hit the board.", board.GetLength(0) - 1, number);
                    HitJ = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("This is not a valid input.");
                    Console.WriteLine();
                    continue;
                }

                if (Board.CheckIfOutOfRange(HitI, HitJ, board))
                {
                    Console.WriteLine("The coordinates must be from 0 to {0}", board.GetLength(0) - 1);
                    Console.WriteLine();
                    continue;
                }
                break;
            }
        }
    }
}
