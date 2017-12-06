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

        public static void NewGame(Player player1, Player player2, string[,] board)
        {
            Player.AssignPlayers(player1, player2);
            Player.CurrentPlayer = player1;
            Player.NextPlayer = player2;
            Array.Clear(board, 0, board.Length);
            Board.InitializeOrPrintBoard(board);
            Board.CurrentPlayerOverWrittenSymmbolShip1 = GridProperties.M1.ToString();
            Board.CurrentPlayerOverWrittenSymmbolShip2S1 = GridProperties.M1.ToString();
            Board.CurrentPlayerOverWrittenSymmbolShip2S2 = GridProperties.M1.ToString();
            Board.NextPlayerOverWrittenSymmbolShip1 = GridProperties.M1.ToString();
            Board.NextPlayerOverWrittenSymmbolShip2S1 = GridProperties.M1.ToString();
            Board.NextPlayerOverWrittenSymmbolShip2S2 = GridProperties.M1.ToString();
        }

        public static bool CheckTheWinner()
        {
            return Player.NextPlayer.Ship1Settled == 1 && 
                   Player.NextPlayer.Ship2_1_Settled == 1 &&
                   Player.NextPlayer.Ship2_2_Settled == 1 ? true : false;
        }
    }
}
