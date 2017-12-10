using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class AIPlayer : Player
    {
        public int HitI { get; set; }
        public int HitJ { get; set; }

        public List<Ship> ships { get; set; } = new List<Ship>();

        public void InitializeTheShips()
        {
            ships.Add(new Ship());
            ships.Add(new Ship());
            ships.Add(new Ship());
        }

        public void PutTheShipOnTheBoard(int number, string[,] board)
        {
            Random random = new Random();

            ships[0].I = random.Next(board.GetLength(0));
            ships[0].J = random.Next(board.GetLength(0));

            while (true)
            {
                ships[1].I = random.Next(board.GetLength(0));
                ships[1].J = random.Next(board.GetLength(0));
                ships[2].I = random.Next(ships[1].I - 1, ships[1].I + 2);
                ships[2].J = random.Next(ships[1].J - 1, ships[1].J + 2);

                if (ships[0].I == ships[1].I && ships[0].J == ships[1].J ||
                    ships[0].I == ships[2].I && ships[0].J == ships[2].J) continue;

                if (Board.CheckIfOutOfRange(ships[1].I, ships[1].J, board) ||
                    Board.CheckIfOutOfRange(ships[2].I, ships[2].J, board)) continue;

                if (Ship.CheckTheSecondGridOfTheShip(ships[1].I, ships[1].J, ships[2].I, ships[2].J)) break;

                else continue;
            }
        }

        public void HitTheBoard(int number, string[,] board)
        {
            Random random = new Random();

            while (true)
            {
                HitI = random.Next(board.GetLength(0));
                HitJ = random.Next(board.GetLength(0));

                if (Board.CheckIfOutOfRange(HitI, HitJ, board) == false) break;

                else
                {
                    Console.WriteLine("The coordinates must be from 0 to {0}", board.GetLength(0) - 1);
                    Console.WriteLine();
                    continue;
                }
            }
        }
    }
}
