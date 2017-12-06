using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameEngine
    {
        public static void PlayingTheGame(Player player1, Player player2, string[,] board, int numberOfGrids)
        {
            int number = 0;

            Console.WriteLine("You may put 2 ships on the board. The first one takes 1 grid, the second one 2. " +
                              "\nThe grids of the second ship must form a vertical or horizontal line.");
            Console.WriteLine();

            while (number < 3)
            {
                //The players set their ships on the board
                number = Player.CurrentPlayer == player1 ? 1 : 2;

                if (StartNewGame.GameMode.Equals(GameScenario.RealVsReal))
                {
                    Player.CurrentPlayer.PutTheShipOnTheBoardReal(numberOfGrids, number, board);
                }
                        
                else
                {
                    if (number == 1) Player.CurrentPlayer.PutTheShipOnTheBoardReal(numberOfGrids, number, board);

                    else Player.CurrentPlayer.PutTheShipOnTheBoardAI(numberOfGrids, board);
                }

                if (Board.CheckIfTheGridTaken(Player.CurrentPlayer.Ship1CoordinateI, Player.CurrentPlayer.Ship1CoordinateJ, board) == true ||
                    Board.CheckIfTheGridTaken(Player.CurrentPlayer.Ship2CoordinateI1, Player.CurrentPlayer.Ship2CoordinateJ1, 
                                              Player.CurrentPlayer.Ship2CoordinateI2, Player.CurrentPlayer.Ship2CoordinateJ2, board) == true)
                {
                    Console.WriteLine("The ships are on the same grid. Please enter the coordinates again.");
                    Console.WriteLine();
                    StartNewGame.NewGame(player1, player2, board);
                    continue;
                }

                board[Player.CurrentPlayer.Ship1CoordinateI, Player.CurrentPlayer.Ship1CoordinateJ] = Player.CurrentPlayer.symbol1;
                board[Player.CurrentPlayer.Ship2CoordinateI1, Player.CurrentPlayer.Ship2CoordinateJ1] = Player.CurrentPlayer.symbol2;
                board[Player.CurrentPlayer.Ship2CoordinateI2, Player.CurrentPlayer.Ship2CoordinateJ2] = Player.CurrentPlayer.symbol3;

                Player.FlipThePlayers();

                number++;
            }

            Console.WriteLine();

            while (Board.Winner == null)
            {
                number = Player.CurrentPlayer == player1 ? 1 : 2;

                //The beginning of the game

                if (StartNewGame.GameMode == GameScenario.RealVsReal)
                {    
                    Board.InitializeOrPrintBoard(board);
                    Player.CurrentPlayer.HitTheBoardReal(numberOfGrids, number, board);
                }
                else
                {
                    if (number == 1)
                    {
                        Board.InitializeOrPrintBoard(board);
                        Player.CurrentPlayer.HitTheBoardReal(numberOfGrids, number, board);
                    }

                    else Player.CurrentPlayer.HitTheBoardAI(numberOfGrids, board);
                }
                
                HitStatus result = Board.CheckIfHitTheShip(board);

                if (result == HitStatus.Own)
                {
                    if (Player.CurrentPlayer.GetType() == player1.GetType())
                    {
                        Console.WriteLine("This is your ship. Please choose different coordinates.");
                        Console.WriteLine();
                    }
                    continue;
                }

                else if (result == HitStatus.Hit)
                {
                    if (Player.CurrentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've settled the ship!!!");

                    else Console.WriteLine("Your ship's been settled.");
                }
                else if (result == HitStatus.HalfHit)
                {
                    if (Player.CurrentPlayer.GetType() == player1.GetType()) Console.WriteLine("Congratulations! You've hit the ship!!!");

                    else Console.WriteLine("Your ship's been hit.");

                    if (Player.NextPlayer.Ship2_1_Settled == 1 && Player.NextPlayer.Ship2_2_Settled == 1)
                    {
                        if (Player.CurrentPlayer.GetType() == player1.GetType()) Console.WriteLine("Congratulations! You've settled the ship!!!");

                        else Console.WriteLine("Your ship's been settled.");
                    }
                }

                // An option to play without moving the ships.
                //else if (result == HitStatus.Repeat)
                //{
                //    if (Player.CurrentPlayer.GetType() == player1.GetType()) Console.WriteLine("This grid has already been hit. Please enter different coordinates.");
                //    continue;
                //}
                else
                {
                    if (Player.CurrentPlayer.GetType() == player1.GetType()) Console.WriteLine("You missed.");

                    else Console.WriteLine("Your opponent missed.");
                }

                if (StartNewGame.CheckTheWinner() == true)
                {
                    Board.Winner = Player.CurrentPlayer == player1 ? "Player 1" : "Player 2";
                }

                Board.MoveTheShip(numberOfGrids, board);

                Player.FlipThePlayers();
            }
        }
    }
}
