using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Board
    {
        public static bool CheckIfOutOfRange(int ship_I, int ship_J, string[,] board)
        {
            return ship_I > board.GetLength(0) - 1 ||
                   ship_J > board.GetLength(0) - 1 ||
                   ship_I < 0 ||
                   ship_J < 0 ? true : false;
        }

        public static Ship CheckIfHitTheShip(string[,] board, Player currentPlayer, Player nextPlayer)
        {
            // I may change the condition to Contains().
            foreach (Ship ship in currentPlayer.ships)
            {
                if (board[currentPlayer.HitI, currentPlayer.HitJ] == ship.Symbol) return ship;
            }

            foreach (Ship ship in nextPlayer.ships)
            {
                if (board[currentPlayer.HitI, currentPlayer.HitJ] == ship.Symbol)
                {
                    board[currentPlayer.HitI, currentPlayer.HitJ] = GridProperties.X1.ToString();
                    ship.Settled = 1;
                    return ship;
                }
            }

            board[currentPlayer.HitI, currentPlayer.HitJ] = GridProperties.O1.ToString();
            return null;
        }

        public static bool CheckIfTheGridTaken(List<Ship> ships, string[,] board)
        {
            foreach (Ship ship in ships)
            {
                if (board[ship.I, ship.J].Contains(GridProperties.S.ToString()))
                return true;
            }
            return false;
        }

        public static bool CheckIfTheGridTaken(int ship_I, int ship_J, string[,] board)
        {
            if (board[ship_I, ship_J].Contains(GridProperties.S.ToString())) return true;
            return false;
        }

        public static void MoveTheShip(string[,] board, Random random, Player currentPlayer)
        {
            if (currentPlayer.ships[0].Settled == 0 &&
                CheckIfMovingPossible(currentPlayer.ships[0], board))
            {
                board[currentPlayer.ships[0].I, currentPlayer.ships[0].J] = currentPlayer.ships[0].OverWrittenSymbol;

                MoveManagement(currentPlayer.ships[0], board, random);
            }

            // The case when neither section of the second ship has been hit.
            if (currentPlayer.ships[1].Settled == 0 && 
                currentPlayer.ships[2].Settled == 0 &&
               (CheckIfMovingPossible(currentPlayer.ships[1], board) ||
                CheckIfMovingPossible(currentPlayer.ships[2], board)))
            {
                int oldShip2_I = currentPlayer.ships[1].I;
                int oldShip2_J = currentPlayer.ships[1].J;

                board[currentPlayer.ships[1].I, currentPlayer.ships[1].J] = currentPlayer.ships[1].OverWrittenSymbol;
                board[currentPlayer.ships[2].I, currentPlayer.ships[2].J] = currentPlayer.ships[2].OverWrittenSymbol;

                MoveManagement(currentPlayer.ships[1], board, random);

                // Checking of the first section of the second ship is done.

                if (!currentPlayer.ships[1].I.Equals(currentPlayer.ships[2].I) &&
                    !currentPlayer.ships[1].J.Equals(currentPlayer.ships[2].J))
                {
                    currentPlayer.ships[2].OverWrittenSymbol = board[oldShip2_I, oldShip2_J];

                    board[oldShip2_I, oldShip2_J] = currentPlayer.ships[2].Symbol;

                    currentPlayer.ships[2].I = oldShip2_I;
                    currentPlayer.ships[2].J = oldShip2_J;
                }

                else
                {
                    MoveManagement(currentPlayer.ships[2], oldShip2_I, oldShip2_J, board, random);
                }
            }

            // The case when one of the sections of the second ship has been hit.
            else
            {
                if (currentPlayer.ships[1].Settled == 0 &&
                    CheckIfMovingPossible(currentPlayer.ships[1], board))
                {
                    board[currentPlayer.ships[1].I, currentPlayer.ships[1].J] = currentPlayer.ships[1].OverWrittenSymbol;

                    MoveManagement(currentPlayer.ships[1], board, random);
                }

                else if (currentPlayer.ships[2].Settled == 0 &&
                         CheckIfMovingPossible(currentPlayer.ships[2], board))
                {
                    board[currentPlayer.ships[2].I, currentPlayer.ships[2].J] = currentPlayer.ships[2].OverWrittenSymbol;

                    MoveManagement(currentPlayer.ships[2], board, random);
                }
            }
        }

        private static void MoveManagement(Ship ship, string[,] board, Random random)
        {
            int[] arrayShip = new int[2];

            arrayShip = MoveOneSectionOfTheShip(ship, board, random);

            ship.OverWrittenSymbol = board[arrayShip[0], arrayShip[1]];

            board[arrayShip[0], arrayShip[1]] = ship.Symbol;

            ship.I = arrayShip[0];
            ship.J = arrayShip[1];
        }

        private static int[] MoveOneSectionOfTheShip(Ship ship, string[,] board, Random random)
        {
            int oldShip_I = ship.I;
            int oldShip_J = ship.J;

            while (true)
            {
                ship.I = random.Next(oldShip_I - 1, oldShip_I + 2);
                ship.J = random.Next(oldShip_J - 1, oldShip_J + 2);

                if (CheckIfOutOfRange(ship.I, ship.J, board)) continue;

                if (CheckIfTheGridTaken(ship.I, ship.J, board)) continue;

                if (Ship.CheckTheSecondGridOfTheShip(oldShip_I, oldShip_J, ship.I, ship.J) == false) continue;
                break;
            }

            int[] array = new int[] { ship.I, ship.J };

            return array;
        }

        private static void MoveManagement(Ship ship, int ship2_I, int ship2_J, string[,] board, Random random)
        {
            int[] arrayShip = new int[2];

            arrayShip = MoveOneSectionOfTheShip(ship, ship2_I, ship2_J, board, random);

            ship.OverWrittenSymbol = board[arrayShip[0], arrayShip[1]];

            board[arrayShip[0], arrayShip[1]] = ship.Symbol;

            ship.I = arrayShip[0];
            ship.J = arrayShip[1];
        }

        private static int[] MoveOneSectionOfTheShip(Ship ship, int ship2_I, int ship2_J, string[,] board, Random random)
        {
            int oldShip_I = ship.I;
            int oldShip_J = ship.J;

            while (true)
            {
                ship.I = random.Next(oldShip_I - 1, oldShip_I + 2);
                ship.J = random.Next(oldShip_J - 1, oldShip_J + 2);

                if (CheckIfOutOfRange(ship.I, ship.J, board)) continue;

                if (ship.I == ship2_I && ship.J == ship2_J) continue;

                if (CheckIfTheGridTaken(ship.I, ship.J, board)) continue;

                if (Ship.CheckTheSecondGridOfTheShip(oldShip_I, oldShip_J, ship.I, ship.J) == false) continue;
                break;
            }

            int[] array = new int[] { ship.I, ship.J };

            return array;
        }

        private static bool CheckIfMovingPossible(Ship ship, string[,] board)
        {
            int I1 = ship.I - 1;
            int I2 = ship.I;
            int I3 = ship.I + 1;
            int J1 = ship.J - 1;
            int J2 = ship.J;
            int J3 = ship.J + 1;

            if (CheckIfOutOfRange(I1, J2, board) == false && CheckIfTheGridTaken(I1, J2, board) == false) return true;
            if (CheckIfOutOfRange(I2, J3, board) == false && CheckIfTheGridTaken(I2, J3, board) == false) return true;
            if (CheckIfOutOfRange(I3, J2, board) == false && CheckIfTheGridTaken(I3, J2, board) == false) return true;
            if (CheckIfOutOfRange(I2, J1, board) == false && CheckIfTheGridTaken(I2, J1, board) == false) return true;
            else return false;
        }
    }
}