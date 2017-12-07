using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class RealPlayer : IPlayer
    {
        public string symbol1 { get; set; }
        public string symbol2 { get; set; }
        public string symbol3 { get; set; }

        public int Ship1CoordinateI { get; set; }
        public int Ship1CoordinateJ { get; set; }

        public int Ship2CoordinateI1 { get; set; }
        public int Ship2CoordinateJ1 { get; set; }
        public int Ship2CoordinateI2 { get; set; }
        public int Ship2CoordinateJ2 { get; set; }

        public int HitCoordinateI { get; set; }
        public int HitCoordinateJ { get; set; }

        public string OverWrittenSymmbolShip1 { get; set; }
        public string OverWrittenSymmbolShip2S1 { get; set; }
        public string OverWrittenSymmbolShip2S2 { get; set; }

        public int Ship1Settled { get; set; }
        public int Ship2_1_Settled { get; set; }
        public int Ship2_2_Settled { get; set; }

        public void PutTheShipOnTheBoard(int numberOfGrids, int number, string[,] board)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Player {1}, please enter a vertical coordinate from 0 to {0} to set your first ship.", numberOfGrids - 1, number);
                    Ship1CoordinateI = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a horizontal coordinate from 0 to {0} to set first your ship.", numberOfGrids - 1, number);
                    Ship1CoordinateJ = Int32.Parse(Console.ReadLine());

                    if (Board.CheckIfOutOfRange(Ship1CoordinateI, Ship1CoordinateJ, board) == true)
                    {
                        Console.WriteLine("The coordinates must be from 0 to {0}", numberOfGrids - 1);
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
                                      "to set the first grid of your second ship.", numberOfGrids - 1, number);
                    Ship2CoordinateI1 = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a horizontal coordinate from 0 to {0} " +
                                      "to set the first grid of your second ship.", numberOfGrids - 1, number);
                    Ship2CoordinateJ1 = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a vertical coordinate from 0 to {0} " +
                                      "to set the second grid of your second ship.", numberOfGrids - 1, number);
                    Ship2CoordinateI2 = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, please enter a horizontal coordinate from 0 to {0} " +
                                      "to set the second grid of your second ship.", numberOfGrids - 1, number);
                    Ship2CoordinateJ2 = Int32.Parse(Console.ReadLine());

                    if (Board.CheckIfOutOfRange(Ship2CoordinateI1, Ship2CoordinateJ1, Ship2CoordinateI2, Ship2CoordinateJ2, board) == true)
                    {
                        Console.WriteLine("The coordinates must be from 0 to {0}", numberOfGrids - 1);
                        Console.WriteLine();
                        continue;
                    }

                    if (Ship.CheckTheSecondGridOfTheShip(Ship2CoordinateI1, Ship2CoordinateJ1, Ship2CoordinateI2, Ship2CoordinateJ2) == false)
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

        public void HitTheBoard(int numberOfGrids, int number, string[,] board)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Player {1}, pleast enter a vertical coordinate from 0 to {0} to hit the board.", numberOfGrids - 1, number);
                    HitCoordinateI = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Player {1}, pleast enter a horizontal coordinate from 0 to {0} to hit the board.", numberOfGrids - 1, number);
                    HitCoordinateJ = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("This is not a valid input.");
                    Console.WriteLine();
                    continue;
                }

                if (Board.CheckIfOutOfRange(HitCoordinateI, HitCoordinateJ, board) == false) break;

                else
                {
                    Console.WriteLine("The coordinates must be from 0 to {0}", numberOfGrids - 1);
                    Console.WriteLine();
                    continue;
                }
            }
        }
    }
}
