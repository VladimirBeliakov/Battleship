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
            int numberOfGrids = 0;

            while (true)
            {
                try
                {
                    Console.WriteLine("To start a new game, enter the number of grids for the board to play on.");
                    numberOfGrids = Int32.Parse(Console.ReadLine());

                    if (numberOfGrids > 10 || numberOfGrids < 3)
                    {
                        Console.WriteLine("The board cannot be bigger than 10x10 or smaller than 3x3. Please enter a new number.");
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("This is not a valid input.");
                    continue;
                }
                break;
            }

            string[,] BattleshipBoard = new string[numberOfGrids, numberOfGrids];

            while (true)
            {
                Console.WriteLine("Please choose a game mode: for 2 players, enter \"1\", for player vs. AI, enter \"2\".");
                int input = Int32.Parse(Console.ReadLine());

                try
                {
                    if (input == 1)
                    {
                        StartNewGame.GameMode = GameScenario.RealVsReal;
                    }
                    else if (input == 2)
                    {
                        StartNewGame.GameMode = GameScenario.RealVsAI;
                    }
                    else
                    {
                        Console.WriteLine("You should enter \"1\" or \"2\".");
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("This is not a valid input.");
                    continue;
                }
                break;
            }

            //int Ship1CoordinateI = 0;
            //int Ship1CoordinateJ = 0;

            //int Ship2CoordinateI1 = 0;
            //int Ship2CoordinateJ1 = 0;
            //int Ship2CoordinateI2 = 0;
            //int Ship2CoordinateJ2 = 0;

            //Ship ship1 = new Ship(Ship1CoordinateI, Ship1CoordinateJ);
            //Ship ship2 = new Ship(Ship2CoordinateI1, Ship2CoordinateJ1, Ship2CoordinateI2, Ship2CoordinateJ2);

            Player player1 = new RealPlayer();
            Player player2;

            if (StartNewGame.GameMode == GameScenario.RealVsAI) player2 = new AIPlayer();
            else player2 = new RealPlayer();

            StartNewGame.NewGame(player1, player2, BattleshipBoard);

            GameEngine.PlayingTheGame(player1, player2, BattleshipBoard, numberOfGrids);

            Console.WriteLine($"The Winner is {Board.Winner}!");

            Console.ReadLine();
        }
    }
}
