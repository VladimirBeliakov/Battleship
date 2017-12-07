using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class AIPlayer : IPlayer
    {
        Random random = new Random();

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
            Ship1CoordinateI = random.Next(numberOfGrids);
            Ship1CoordinateJ = random.Next(numberOfGrids);

            while (true)
            {
                Ship2CoordinateI1 = random.Next(numberOfGrids);
                Ship2CoordinateJ1 = random.Next(numberOfGrids);
                Ship2CoordinateI2 = random.Next(Ship2CoordinateI1 - 1, Ship2CoordinateI1 + 2);
                Ship2CoordinateJ2 = random.Next(Ship2CoordinateJ1 - 1, Ship2CoordinateJ1 + 2);

                if (Board.CheckIfOutOfRange(Ship2CoordinateI1, Ship2CoordinateJ1, Ship2CoordinateI2, Ship2CoordinateJ2, board) == true) continue;

                if (Ship.CheckTheSecondGridOfTheShip(Ship2CoordinateI1, Ship2CoordinateJ1, Ship2CoordinateI2, Ship2CoordinateJ2) == true) break;
                else continue;
            }
        }

        public void HitTheBoard(int numberOfGrids, int number, string[,] board)
        {
            while (true)
            {
                HitCoordinateI = random.Next(numberOfGrids);
                HitCoordinateJ = random.Next(numberOfGrids);

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
