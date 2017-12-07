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
        public static bool CheckIfOutOfRange(int shipCoordinateI, int shipCoordinateJ, string[,] board)
        {
            return shipCoordinateI > board.GetLength(0) - 1 ||
                   shipCoordinateJ > board.GetLength(0) - 1 ||
                   shipCoordinateI < 0 ||
                   shipCoordinateJ < 0 ? true : false;
        }

        public static bool CheckIfOutOfRange(int shipCoordinateI1, int shipCoordinateJ1, int shipCoordinateI2, int shipCoordinateJ2, string[,] board)
        {
            return shipCoordinateI1 > board.GetLength(0) - 1 || shipCoordinateI1 < 0 ||
                   shipCoordinateJ1 > board.GetLength(0) - 1 || shipCoordinateJ1 < 0 ||
                   shipCoordinateI2 > board.GetLength(0) - 1 || shipCoordinateI2 < 0 ||
                   shipCoordinateJ2 > board.GetLength(0) - 1 || shipCoordinateJ2 < 0 ? true : false;
        }

        public static HitStatus CheckIfHitTheShip(string[,] board, IPlayer currentPlayer, IPlayer nextPlayer)
        {
            if (board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == currentPlayer.symbol1 ||
                board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == currentPlayer.symbol2 ||
                board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == currentPlayer.symbol3) return HitStatus.Own;

            else if (board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == nextPlayer.symbol1)
            {
                board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] = GridProperties.X1.ToString();
                nextPlayer.Ship1Settled += 1;
                return HitStatus.Hit;
            }

            else if (board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == nextPlayer.symbol2)
            {
                board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] = GridProperties.X1.ToString();
                nextPlayer.Ship2_1_Settled += 1;
                return HitStatus.HalfHit;
            }

            else if (board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == nextPlayer.symbol3)
            {
                board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] = GridProperties.X1.ToString();
                nextPlayer.Ship2_2_Settled += 1;
                return HitStatus.HalfHit;
            }

            // An option to play without moving the ships.
            else if (board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == GridProperties.O1.ToString() ||
                     board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] == GridProperties.X1.ToString()) return HitStatus.Repeat;

            else
            {
                board[currentPlayer.HitCoordinateI, currentPlayer.HitCoordinateJ] = GridProperties.O1.ToString();
                return HitStatus.Missed;
            }       
        }
        
        public static bool CheckIfTheGridTaken(int shipCoordinateI, int shipCoordinateJ, string[,] board)
        {
            return board[shipCoordinateI, shipCoordinateJ] != GridProperties.M1.ToString() &&
                   board[shipCoordinateI, shipCoordinateJ] != GridProperties.O1.ToString() &&
                   board[shipCoordinateI, shipCoordinateJ] != GridProperties.X1.ToString() ? true : false;
        }

        public static bool CheckIfTheGridTaken(int shipCoordinateI1, int shipCoordinateJ1, int shipCoordinateI2, int shipCoordinateJ2, string[,] board)
        {
            return board[shipCoordinateI1, shipCoordinateJ1] != GridProperties.M1.ToString() ||
                   board[shipCoordinateI2, shipCoordinateJ2] != GridProperties.M1.ToString() ? true : false;
        }


        public static void MoveTheShip(int numberOfGrids, string[,] board, Random random, IPlayer currentPlayer)
        {
            if (currentPlayer.Ship1Settled == 0 &&
                CheckIfMovingPossible(currentPlayer.Ship1CoordinateI, currentPlayer.Ship1CoordinateJ, board) == true)
            {
                int[] arrayShip1 = new int[2];

                arrayShip1 = MoveOneSectionOfTheShip(currentPlayer.Ship1CoordinateI, 
                                                     currentPlayer.Ship1CoordinateJ, board, random);

                board[currentPlayer.Ship1CoordinateI, currentPlayer.Ship1CoordinateJ] = currentPlayer.OverWrittenSymmbolShip1;

                currentPlayer.OverWrittenSymmbolShip1 = board[arrayShip1[0], arrayShip1[1]];

                board[arrayShip1[0], arrayShip1[1]] = currentPlayer.symbol1;

                currentPlayer.Ship1CoordinateI = arrayShip1[0];
                currentPlayer.Ship1CoordinateJ = arrayShip1[1];
            }

            // The case when neither section of the second ship has been hit.
            if (currentPlayer.Ship2_1_Settled == 0 && currentPlayer.Ship2_2_Settled == 0 &&
               (CheckIfMovingPossible(currentPlayer.Ship2CoordinateI1, currentPlayer.Ship2CoordinateJ1, board) == true ||
                CheckIfMovingPossible(currentPlayer.Ship2CoordinateI2, currentPlayer.Ship2CoordinateJ2, board) == true))
            {
                int oldShip2CoordinateI1 = currentPlayer.Ship2CoordinateI1;
                int oldShip2CoordinateJ1 = currentPlayer.Ship2CoordinateJ1;

                int[] arrayShip2_1_Case1 = new int[2];

                arrayShip2_1_Case1 = MoveOneSectionOfTheShip(currentPlayer.Ship2CoordinateI1,
                                                             currentPlayer.Ship2CoordinateJ1,
                                                             currentPlayer.Ship2CoordinateI2,
                                                             currentPlayer.Ship2CoordinateJ2, board, random);

                board[currentPlayer.Ship2CoordinateI1, currentPlayer.Ship2CoordinateJ1] = currentPlayer.OverWrittenSymmbolShip2S1;
                board[currentPlayer.Ship2CoordinateI2, currentPlayer.Ship2CoordinateJ2] = currentPlayer.OverWrittenSymmbolShip2S2;

                currentPlayer.OverWrittenSymmbolShip2S1 = board[arrayShip2_1_Case1[0], arrayShip2_1_Case1[1]];

                board[arrayShip2_1_Case1[0], arrayShip2_1_Case1[1]] = currentPlayer.symbol2;

                currentPlayer.Ship2CoordinateI1 = arrayShip2_1_Case1[0];
                currentPlayer.Ship2CoordinateJ1 = arrayShip2_1_Case1[1];

                // Checking of the first section of the second ship is done.

                if (currentPlayer.Ship2CoordinateI1 != currentPlayer.Ship2CoordinateI2 ||
                    currentPlayer.Ship2CoordinateJ1 != currentPlayer.Ship2CoordinateJ2)
                {
                    currentPlayer.OverWrittenSymmbolShip2S2 = board[oldShip2CoordinateI1, oldShip2CoordinateJ1];

                    board[oldShip2CoordinateI1, oldShip2CoordinateJ1] = currentPlayer.symbol3;

                    currentPlayer.Ship2CoordinateI2 = oldShip2CoordinateI1;
                    currentPlayer.Ship2CoordinateJ2 = oldShip2CoordinateJ1;
                }
                
                else
                {
                    int[] arrayShip2_2_Case1 = new int[2];

                    arrayShip2_2_Case1 = MoveOneSectionOfTheShip(currentPlayer.Ship2CoordinateI2,
                                                                 currentPlayer.Ship2CoordinateJ2,
                                                                 oldShip2CoordinateI1,
                                                                 oldShip2CoordinateJ1, board, random);

                    currentPlayer.OverWrittenSymmbolShip2S2 = board[arrayShip2_2_Case1[0], arrayShip2_2_Case1[1]];

                    board[arrayShip2_2_Case1[0], arrayShip2_2_Case1[1]] = currentPlayer.symbol3;

                    currentPlayer.Ship2CoordinateI2 = arrayShip2_2_Case1[0];
                    currentPlayer.Ship2CoordinateJ2 = arrayShip2_2_Case1[1];
                }
            }

            // The case when one of the sections of the second ship has been hit.
            else
            {
                if (currentPlayer.Ship2_1_Settled == 0 &&
                    CheckIfMovingPossible(currentPlayer.Ship2CoordinateI1, 
                                          currentPlayer.Ship2CoordinateJ1, board) == true)
                {
                    int[] arrayShip2_1_Case2 = new int[2];

                    arrayShip2_1_Case2 = MoveOneSectionOfTheShip(currentPlayer.Ship2CoordinateI1, 
                                                                 currentPlayer.Ship2CoordinateJ1, board, random);

                    board[currentPlayer.Ship2CoordinateI1, currentPlayer.Ship2CoordinateJ1] = currentPlayer.OverWrittenSymmbolShip2S1;

                    currentPlayer.OverWrittenSymmbolShip2S1 = board[arrayShip2_1_Case2[0], arrayShip2_1_Case2[1]];

                    board[arrayShip2_1_Case2[0], arrayShip2_1_Case2[1]] = currentPlayer.symbol2;

                    currentPlayer.Ship2CoordinateI1 = arrayShip2_1_Case2[0];
                    currentPlayer.Ship2CoordinateJ1 = arrayShip2_1_Case2[1];
                }

                else if (currentPlayer.Ship2_2_Settled == 0 &&
                         CheckIfMovingPossible(currentPlayer.Ship2CoordinateI2, 
                                               currentPlayer.Ship2CoordinateJ2, board) == true)
                {
                    int[] arrayShip2_2_Case2 = new int[2];

                    arrayShip2_2_Case2 = MoveOneSectionOfTheShip(currentPlayer.Ship2CoordinateI2, 
                                                                 currentPlayer.Ship2CoordinateJ2, board, random);

                    board[currentPlayer.Ship2CoordinateI2, currentPlayer.Ship2CoordinateJ2] = currentPlayer.OverWrittenSymmbolShip2S2;

                    currentPlayer.OverWrittenSymmbolShip2S2 = board[arrayShip2_2_Case2[0], arrayShip2_2_Case2[1]];

                    board[arrayShip2_2_Case2[0], arrayShip2_2_Case2[1]] = currentPlayer.symbol3;

                    currentPlayer.Ship2CoordinateI2 = arrayShip2_2_Case2[0];
                    currentPlayer.Ship2CoordinateJ2 = arrayShip2_2_Case2[1];
                }
            }
        }
        
        private static int[] MoveOneSectionOfTheShip(int shipCoordinateI, 
                                                     int shipCoordinateJ, string[,] board, Random random)
        {
            int oldShipCoordinateI = shipCoordinateI;
            int oldShipCoordinateJ = shipCoordinateJ;

            while (true)
            {
                shipCoordinateI = random.Next(oldShipCoordinateI - 1, oldShipCoordinateI + 2);
                shipCoordinateJ = random.Next(oldShipCoordinateJ - 1, oldShipCoordinateJ + 2);

                if (CheckIfOutOfRange(shipCoordinateI, shipCoordinateJ, board) == true) continue;

                if (CheckIfTheGridTaken(shipCoordinateI, shipCoordinateJ, board) == true) continue;

                if (Ship.CheckTheSecondGridOfTheShip(oldShipCoordinateI,
                                                     oldShipCoordinateJ,
                                                     shipCoordinateI,
                                                     shipCoordinateJ) == false) continue;
                break;
            }

            int[] array = new int[] { shipCoordinateI, shipCoordinateJ };

            return array;
        }

        private static int[] MoveOneSectionOfTheShip(int shipCoordinateI, int shipCoordinateJ, 
                                                     int ship2CoordinateI, int ship2CoordinateJ,  
                                                     string[,] board, Random random)
        {
            int oldShipCoordinateI = shipCoordinateI;
            int oldShipCoordinateJ = shipCoordinateJ;

            while (true)
            {
                shipCoordinateI = random.Next(oldShipCoordinateI - 1, oldShipCoordinateI + 2);
                shipCoordinateJ = random.Next(oldShipCoordinateJ - 1, oldShipCoordinateJ + 2);

                if (CheckIfOutOfRange(shipCoordinateI, shipCoordinateJ, board) == true) continue;

                if (shipCoordinateI == ship2CoordinateI && shipCoordinateJ == ship2CoordinateJ) continue;

                if (CheckIfTheGridTaken(shipCoordinateI, shipCoordinateJ, board) == true) continue;

                if (Ship.CheckTheSecondGridOfTheShip(oldShipCoordinateI,
                                                     oldShipCoordinateJ,
                                                     shipCoordinateI,
                                                     shipCoordinateJ) == false) continue;
                break;
            }

            int[] array = new int[] { shipCoordinateI, shipCoordinateJ };

            return array;
        }

        
        private static bool CheckIfMovingPossible(int ShipCoordinateI, 
                                                  int ShipCoordinateJ, string[,] board)
        {
            int I1 = ShipCoordinateI - 1;
            int I2 = ShipCoordinateI;
            int I3 = ShipCoordinateI + 1;
            int J1 = ShipCoordinateJ - 1;
            int J2 = ShipCoordinateJ;
            int J3 = ShipCoordinateJ + 1;

            if (CheckIfOutOfRange(I1, J2, board) == false && CheckIfTheGridTaken(I1, J2, board) == false) return true;
            if (CheckIfOutOfRange(I2, J3, board) == false && CheckIfTheGridTaken(I2, J3, board) == false) return true;
            if (CheckIfOutOfRange(I3, J2, board) == false && CheckIfTheGridTaken(I3, J2, board) == false) return true;
            if (CheckIfOutOfRange(I2, J1, board) == false && CheckIfTheGridTaken(I2, J1, board) == false) return true;
            else return false;
        }
    }
}