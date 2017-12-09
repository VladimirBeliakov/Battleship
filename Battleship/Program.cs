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
            string[,] battleshipBoard = StartNewGame.CreatNewBoard();

            Player player1 = new RealPlayer();
            Player player2 = StartNewGame.DefineThePlayer();

            player1.InitializeTheShips();
            player2.InitializeTheShips();

            StartNewGame.AssignPlayers(player1, player2);

            string winner = GameEngine.PlayingTheGame(player1, player2, battleshipBoard);

            Console.WriteLine($"The Winner is {winner}!");

            Console.ReadLine();
        }
    }
}
