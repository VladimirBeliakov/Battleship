using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameEngine
    {
        private static Player _currentPlayer;
        private static Player _nextPlayer;

        public static string PlayingTheGame(Player player1, Player player2, string[,] board)
        {
            Random random = new Random();

            int number = 0;

            _currentPlayer = player1;
            _nextPlayer = player2;

            Console.WriteLine("You may put 2 ships on the board. The first one takes 1 grid, the second one 2. " +
                              "\nThe grids of the second ship must form a vertical or horizontal line.");
            Console.WriteLine();

            while (number < 3)
            {
                //The players set their ships on the board

                number = _currentPlayer == player1 ? 1 : 2;

                if (_currentPlayer.GetType() == player1.GetType())
                GameVisualisation.PrintBoard(board, _currentPlayer, _nextPlayer);

                _currentPlayer.PutTheShipOnTheBoard(number, board);

                if (number == 2 && (Board.CheckIfTheGridTaken(_currentPlayer.ships, board)))
                {
                    Console.WriteLine("The ships are on the same grid. Please enter the coordinates again.");
                    Console.WriteLine();
                    GameVisualisation.InitializeBoard(board);
                    _currentPlayer = player1;
                    _nextPlayer = player2;
                    continue;
                }

                foreach (Ship ship in _currentPlayer.ships)
                {
                    board[ship.I, ship.J] = ship.Symbol;
                }

                SwitchPlayers(player1, player2);

                number++;
            }

            Console.WriteLine();

            string winner = null;

            while (winner == null)
            {
                //The beginning of the game

                number = _currentPlayer == player1 ? 1 : 2;

                if (_currentPlayer.GetType() == player1.GetType())
                GameVisualisation.PrintBoard(board, _currentPlayer, _nextPlayer);

                _currentPlayer.HitTheBoard(number, board);

                if (!Ship.SayWhereThePlayerHit(board, _currentPlayer, _nextPlayer)) continue;

                Ship.SayWhereThePlayerHitInDetails(player1);

                if (StartNewGame.CheckTheWinner(_nextPlayer)) winner = _currentPlayer == player1 ? "Player 1" : "Player 2";

                Board.MoveTheShip(board, random, _currentPlayer);

                SwitchPlayers(player1, player2);
            }
            return winner;
        }

        private static void SwitchPlayers(Player player1, Player player2)
        {
            _nextPlayer = _currentPlayer;
            _currentPlayer = _currentPlayer == player1 ? player2 : player1;
        }
    }
}
