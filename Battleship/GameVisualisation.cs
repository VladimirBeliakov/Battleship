using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameVisualisation
    {
        public void InitializeOrPrintBoard(string[,] board, IPlayer currentPlayer, IPlayer nextPlayer)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == null)
                    {
                        board[i, j] = GridProperties.M1.ToString();
                    }

                    else
                    {
                        if (board[i, j] == nextPlayer.symbol1 ||
                            board[i, j] == nextPlayer.symbol2 ||
                            board[i, j] == nextPlayer.symbol3)
                        {
                            if (board[i, j] == nextPlayer.symbol1) Console.Write(nextPlayer.OverWrittenSymmbolShip1);
                            if (board[i, j] == nextPlayer.symbol2) Console.Write(nextPlayer.OverWrittenSymmbolShip2S1);
                            if (board[i, j] == nextPlayer.symbol3) Console.Write(nextPlayer.OverWrittenSymmbolShip2S2);
                        }
                        else if (board[i, j] == currentPlayer.symbol1) Console.Write(currentPlayer.symbol1);

                        else if (board[i, j] == currentPlayer.symbol2) Console.Write(currentPlayer.symbol2);

                        else if (board[i, j] == currentPlayer.symbol3) Console.Write(currentPlayer.symbol3);

                        else Console.Write(board[i, j]);

                        if (j == board.GetLength(0) - 1) Console.WriteLine();
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
