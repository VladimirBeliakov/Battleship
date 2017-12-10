using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class StartNewGame
    {
        public static GameScenario GameMode { get; set; }

        public static string[,] CreatNewBoard()
        {
            int numberOfGrids = ChooseTheBoardSize();

            string[,] board = new string[numberOfGrids, numberOfGrids];

            GameVisualisation.InitializeBoard(board);

            return board;
        }

        private static int ChooseTheBoardSize()
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
            return numberOfGrids;
        }

        public static Player DefineThePlayer()
        {
            Player player;

            ChooseGameMode();

            if (StartNewGame.GameMode == GameScenario.RealVsAI) player = new AIPlayer();

            else player = new RealPlayer();

            return player;
        }

        private static void ChooseGameMode()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please choose a game mode: for 2 players, enter \"1\", for player vs. AI, enter \"2\".");
                    int input = Int32.Parse(Console.ReadLine());

                    if (input == 1) GameMode = GameScenario.RealVsReal;

                    else if (input == 2) GameMode = GameScenario.RealVsAI;

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
        }

        public  static void AssignPlayers(Player player1, Player player2)
        {
            for (int i = 0; i < player1.ships.Count; i++)
            {
                player1.ships[i].Symbol = GridProperties.S.ToString() + (i + 1).ToString();
                player1.ships[i].OverWrittenSymbol = GridProperties.M1.ToString();
            }

            for (int i = 0; i < player2.ships.Count; i++)
            {
                player2.ships[i].Symbol = GridProperties.S.ToString() + (i + 4).ToString();
                player2.ships[i].OverWrittenSymbol = GridProperties.M1.ToString();
            }
        }

        public static bool CheckTheWinner(Player nextPlayer)
        {
            // I need to try the condition like pets.Any(p => p.Age > 1 && p.Vaccinated == false);
            return nextPlayer.ships[0].Settled == 1 &&
                   nextPlayer.ships[1].Settled == 1 &&
                   nextPlayer.ships[2].Settled == 1 ? true : false;
        }
    }
}
