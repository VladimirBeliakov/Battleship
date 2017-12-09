using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameVisualisation
    {
        public static void InitializeBoard(string[,] board)
        {
            Array.Clear(board, 0, board.Length);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j] = GridProperties.M1.ToString();
                }
            }
        }

        public static void PrintBoard(string[,] board, Player currentPlayer, Player nextPlayer)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    while (true)
                    {
                        if (!board[i, j].Contains(GridProperties.S.ToString()))
                        {
                            Console.Write(board[i, j]);
                            break;
                        }

                        foreach (Ship ship in nextPlayer.ships)
                        {
                            if (board[i, j] == ship.Symbol)
                            {
                                Console.Write(ship.OverWrittenSymbol);
                                break;
                            }
                        }

                        foreach (Ship ship in currentPlayer.ships)
                        {
                            if (board[i, j] == ship.Symbol)
                            {
                                Console.Write(ship.Symbol);
                                break;
                            }
                        }
                        break;
                    }
                    if (j == board.GetLength(0) - 1) Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
