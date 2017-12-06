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
        public static string CurrentPlayerOverWrittenSymmbolShip1 { get; set; }
        public static string CurrentPlayerOverWrittenSymmbolShip2S1 { get; set; }
        public static string CurrentPlayerOverWrittenSymmbolShip2S2 { get; set; }

        public static string NextPlayerOverWrittenSymmbolShip1 { get; set; }
        public static string NextPlayerOverWrittenSymmbolShip2S1 { get; set; }
        public static string NextPlayerOverWrittenSymmbolShip2S2 { get; set; }

        public static string Winner { get; set; }

        public static void InitializeOrPrintBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == null)
                    {
                        board[i, j] = GridProperties.M1.ToString();
                        Console.Write(board[i, j]);
                    }

                    else
                    {
                        if (board[i, j] == Player.NextPlayer.symbol1 || 
                            board[i, j] == Player.NextPlayer.symbol2 || 
                            board[i, j] == Player.NextPlayer.symbol3)
                        {
                            if (board[i, j] == Player.NextPlayer.symbol1) Console.Write(NextPlayerOverWrittenSymmbolShip1);
                            if (board[i, j] == Player.NextPlayer.symbol2) Console.Write(NextPlayerOverWrittenSymmbolShip2S1);
                            if (board[i, j] == Player.NextPlayer.symbol3) Console.Write(NextPlayerOverWrittenSymmbolShip2S2);
                        }
                        else if (board[i, j] == Player.CurrentPlayer.symbol1) Console.Write(Player.CurrentPlayer.symbol1);
        
                        else if (board[i, j] == Player.CurrentPlayer.symbol2) Console.Write(Player.CurrentPlayer.symbol2);

                        else if (board[i, j] == Player.CurrentPlayer.symbol3) Console.Write(Player.CurrentPlayer.symbol3);

                        else Console.Write(board[i, j]);
                    }
                    if (j == board.GetLength(0) - 1) Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

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

        public static HitStatus CheckIfHitTheShip(string[,] board)
        {
            if (board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == Player.CurrentPlayer.symbol1 ||
                board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == Player.CurrentPlayer.symbol2 ||
                board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == Player.CurrentPlayer.symbol3) return HitStatus.Own;

            else if (board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == Player.NextPlayer.symbol1)
            {
                board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] = GridProperties.X1.ToString();
                Player.NextPlayer.Ship1Settled += 1;
                return HitStatus.Hit;
            }

            else if (board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == Player.NextPlayer.symbol2)
            {
                board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] = GridProperties.X1.ToString();
                Player.NextPlayer.Ship2_1_Settled += 1;
                return HitStatus.HalfHit;
            }

            else if (board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == Player.NextPlayer.symbol3)
            {
                board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] = GridProperties.X1.ToString();
                Player.NextPlayer.Ship2_2_Settled += 1;
                return HitStatus.HalfHit;
            }

            // An option to play without moving the ships.
            //else if (board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == GridProperties.O1.ToString() ||
            //         board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] == GridProperties.X1.ToString()) return HitStatus.Repeat;

            else
            {
                board[Player.CurrentPlayer.HitCoordinateI, Player.CurrentPlayer.HitCoordinateJ] = GridProperties.O1.ToString();
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


        public static void MoveTheShip(int numberOfGrids, string[,] board)
        {
            Random random = new Random();

            if (Player.CurrentPlayer.Ship1Settled == 0 &&
                CheckIfMovingPossible(Player.CurrentPlayer.Ship1CoordinateI, Player.CurrentPlayer.Ship1CoordinateJ, board) == true)
            {
                int[] arrayShip1 = new int[2];

                arrayShip1 = MoveOneSectionOfTheShip(Player.CurrentPlayer.Ship1CoordinateI, 
                                                     Player.CurrentPlayer.Ship1CoordinateJ, board, random);

                board[Player.CurrentPlayer.Ship1CoordinateI, Player.CurrentPlayer.Ship1CoordinateJ] = CurrentPlayerOverWrittenSymmbolShip1;

                CurrentPlayerOverWrittenSymmbolShip1 = board[arrayShip1[0], arrayShip1[1]];

                board[arrayShip1[0], arrayShip1[1]] = Player.CurrentPlayer.symbol1;

                Player.CurrentPlayer.Ship1CoordinateI = arrayShip1[0];
                Player.CurrentPlayer.Ship1CoordinateJ = arrayShip1[1];
            }

            // The case when neither section of the second ship has been hit.
            if (Player.CurrentPlayer.Ship2_1_Settled == 0 && Player.CurrentPlayer.Ship2_2_Settled == 0 &&
               (CheckIfMovingPossible(Player.CurrentPlayer.Ship2CoordinateI1, Player.CurrentPlayer.Ship2CoordinateJ1, board) == true ||
                CheckIfMovingPossible(Player.CurrentPlayer.Ship2CoordinateI2, Player.CurrentPlayer.Ship2CoordinateJ2, board) == true))
            {
                int oldShip2CoordinateI1 = Player.CurrentPlayer.Ship2CoordinateI1;
                int oldShip2CoordinateJ1 = Player.CurrentPlayer.Ship2CoordinateJ1;

                int[] arrayShip2_1_Case1 = new int[2];

                arrayShip2_1_Case1 = MoveOneSectionOfTheShip(Player.CurrentPlayer.Ship2CoordinateI1,
                                                             Player.CurrentPlayer.Ship2CoordinateJ1,
                                                             Player.CurrentPlayer.Ship2CoordinateI2,
                                                             Player.CurrentPlayer.Ship2CoordinateJ2, board, random);

                board[Player.CurrentPlayer.Ship2CoordinateI1, Player.CurrentPlayer.Ship2CoordinateJ1] = CurrentPlayerOverWrittenSymmbolShip2S1;
                board[Player.CurrentPlayer.Ship2CoordinateI2, Player.CurrentPlayer.Ship2CoordinateJ2] = CurrentPlayerOverWrittenSymmbolShip2S2;

                CurrentPlayerOverWrittenSymmbolShip2S1 = board[arrayShip2_1_Case1[0], arrayShip2_1_Case1[1]];

                board[arrayShip2_1_Case1[0], arrayShip2_1_Case1[1]] = Player.CurrentPlayer.symbol2;

                Player.CurrentPlayer.Ship2CoordinateI1 = arrayShip2_1_Case1[0];
                Player.CurrentPlayer.Ship2CoordinateJ1 = arrayShip2_1_Case1[1];

                // Checking of the first section of the second ship is done.

                if (Player.CurrentPlayer.Ship2CoordinateI1 != Player.CurrentPlayer.Ship2CoordinateI2 ||
                    Player.CurrentPlayer.Ship2CoordinateJ1 != Player.CurrentPlayer.Ship2CoordinateJ2)
                {
                    CurrentPlayerOverWrittenSymmbolShip2S2 = board[oldShip2CoordinateI1, oldShip2CoordinateJ1];

                    board[oldShip2CoordinateI1, oldShip2CoordinateJ1] = Player.CurrentPlayer.symbol3;

                    Player.CurrentPlayer.Ship2CoordinateI2 = oldShip2CoordinateI1;
                    Player.CurrentPlayer.Ship2CoordinateJ2 = oldShip2CoordinateJ1;
                }

                
                else
                {
                    int[] arrayShip2_2_Case1 = new int[2];

                    arrayShip2_2_Case1 = MoveOneSectionOfTheShip(Player.CurrentPlayer.Ship2CoordinateI2,
                                                                 Player.CurrentPlayer.Ship2CoordinateJ2,
                                                                 oldShip2CoordinateI1,
                                                                 oldShip2CoordinateJ1, board, random);

                    CurrentPlayerOverWrittenSymmbolShip2S2 = board[arrayShip2_2_Case1[0], arrayShip2_2_Case1[1]];

                    board[arrayShip2_2_Case1[0], arrayShip2_2_Case1[1]] = Player.CurrentPlayer.symbol3;

                    Player.CurrentPlayer.Ship2CoordinateI2 = arrayShip2_2_Case1[0];
                    Player.CurrentPlayer.Ship2CoordinateJ2 = arrayShip2_2_Case1[1];
                }
            }

            // The case when one of the sections of the second ship has been hit.
            else
            {
                if (Player.CurrentPlayer.Ship2_1_Settled == 0 &&
                    CheckIfMovingPossible(Player.CurrentPlayer.Ship2CoordinateI1, 
                                          Player.CurrentPlayer.Ship2CoordinateJ1, board) == true)
                {
                    int[] arrayShip2_1_Case2 = new int[2];

                    arrayShip2_1_Case2 = MoveOneSectionOfTheShip(Player.CurrentPlayer.Ship2CoordinateI1, 
                                                                 Player.CurrentPlayer.Ship2CoordinateJ1, board, random);

                    board[Player.CurrentPlayer.Ship2CoordinateI1, Player.CurrentPlayer.Ship2CoordinateJ1] = CurrentPlayerOverWrittenSymmbolShip2S1;

                    CurrentPlayerOverWrittenSymmbolShip2S1 = board[arrayShip2_1_Case2[0], arrayShip2_1_Case2[1]];

                    board[arrayShip2_1_Case2[0], arrayShip2_1_Case2[1]] = Player.CurrentPlayer.symbol2;

                    Player.CurrentPlayer.Ship2CoordinateI1 = arrayShip2_1_Case2[0];
                    Player.CurrentPlayer.Ship2CoordinateJ1 = arrayShip2_1_Case2[1];
                }

                else if (Player.CurrentPlayer.Ship2_2_Settled == 0 &&
                         CheckIfMovingPossible(Player.CurrentPlayer.Ship2CoordinateI2, 
                                               Player.CurrentPlayer.Ship2CoordinateJ2, board) == true)
                {
                    int[] arrayShip2_2_Case2 = new int[2];

                    arrayShip2_2_Case2 = MoveOneSectionOfTheShip(Player.CurrentPlayer.Ship2CoordinateI2, 
                                                                 Player.CurrentPlayer.Ship2CoordinateJ2, board, random);

                    board[Player.CurrentPlayer.Ship2CoordinateI2, Player.CurrentPlayer.Ship2CoordinateJ2] = CurrentPlayerOverWrittenSymmbolShip2S2;

                    CurrentPlayerOverWrittenSymmbolShip2S2 = board[arrayShip2_2_Case2[0], arrayShip2_2_Case2[1]];

                    board[arrayShip2_2_Case2[0], arrayShip2_2_Case2[1]] = Player.CurrentPlayer.symbol3;

                    Player.CurrentPlayer.Ship2CoordinateI2 = arrayShip2_2_Case2[0];
                    Player.CurrentPlayer.Ship2CoordinateJ2 = arrayShip2_2_Case2[1];
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

        
        public static void FlipOverWrittenSymmbol()
        {
            string temporaryVariable;

            temporaryVariable = CurrentPlayerOverWrittenSymmbolShip1;
            CurrentPlayerOverWrittenSymmbolShip1 = NextPlayerOverWrittenSymmbolShip1;
            NextPlayerOverWrittenSymmbolShip1 = temporaryVariable;

            temporaryVariable = CurrentPlayerOverWrittenSymmbolShip2S1;
            CurrentPlayerOverWrittenSymmbolShip2S1 = NextPlayerOverWrittenSymmbolShip2S1;
            NextPlayerOverWrittenSymmbolShip2S1 = temporaryVariable;

            temporaryVariable = CurrentPlayerOverWrittenSymmbolShip2S2;
            CurrentPlayerOverWrittenSymmbolShip2S2 = NextPlayerOverWrittenSymmbolShip2S2;
            NextPlayerOverWrittenSymmbolShip2S2 = temporaryVariable;
        }
    }
}