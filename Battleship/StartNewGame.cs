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

        public static void NewGame(IPlayer player1, IPlayer player2, string[,] board, GameVisualisation gameVisualisation)
        {
            AssignPlayers(player1, player2);
            Array.Clear(board, 0, board.Length);
            gameVisualisation.InitializeOrPrintBoard(board, player1, player2);
            player1.OverWrittenSymmbolShip1 = GridProperties.M1.ToString();
            player1.OverWrittenSymmbolShip2S1 = GridProperties.M1.ToString();
            player1.OverWrittenSymmbolShip2S2 = GridProperties.M1.ToString();
            player2.OverWrittenSymmbolShip1 = GridProperties.M1.ToString();
            player2.OverWrittenSymmbolShip2S1 = GridProperties.M1.ToString();
            player2.OverWrittenSymmbolShip2S2 = GridProperties.M1.ToString();
        }

        public static bool CheckTheWinner(IPlayer nextPlayer)
        {
            return nextPlayer.Ship1Settled == 1 &&
                   nextPlayer.Ship2_1_Settled == 1 &&
                   nextPlayer.Ship2_2_Settled == 1 ? true : false;
        }

        private static void AssignPlayers(IPlayer player1, IPlayer player2)
        {
            player1.symbol1 = GridProperties.S1.ToString();
            player1.symbol2 = GridProperties.S2.ToString();
            player1.symbol3 = GridProperties.S3.ToString();
            player2.symbol1 = GridProperties.S4.ToString();
            player2.symbol2 = GridProperties.S5.ToString();
            player2.symbol3 = GridProperties.S6.ToString();
        }
    }
}
