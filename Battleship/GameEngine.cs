using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class GameEngine
    {
        private IPlayer _currentPlayer;
        private IPlayer _nextPlayer;

        private GameVisualisation _gameVisualisation;

        public GameEngine(GameVisualisation gameVisualisation)
        {
            _gameVisualisation = gameVisualisation;
        }


        public string PlayingTheGame(IPlayer player1, IPlayer player2, string[,] board, int numberOfGrids, Random random)
        {
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
                _gameVisualisation.InitializeOrPrintBoard(board, _currentPlayer, _nextPlayer);

                _currentPlayer.PutTheShipOnTheBoard(numberOfGrids, number, board);

                if (number == 2 && (Board.CheckIfTheGridTaken(_currentPlayer.Ship1CoordinateI, _currentPlayer.Ship1CoordinateJ, board) == true ||
                                    Board.CheckIfTheGridTaken(_currentPlayer.Ship2CoordinateI1, _currentPlayer.Ship2CoordinateJ1,
                                              _currentPlayer.Ship2CoordinateI2, _currentPlayer.Ship2CoordinateJ2, board) == true))
                {
                    Console.WriteLine("The ships are on the same grid. Please enter the coordinates again.");
                    StartNewGame.NewGame(player1, player2, board, _gameVisualisation);
                    _currentPlayer = player1;
                    _nextPlayer = player2;
                    continue;
                }

                board[_currentPlayer.Ship1CoordinateI, _currentPlayer.Ship1CoordinateJ] = _currentPlayer.symbol1;
                board[_currentPlayer.Ship2CoordinateI1, _currentPlayer.Ship2CoordinateJ1] = _currentPlayer.symbol2;
                board[_currentPlayer.Ship2CoordinateI2, _currentPlayer.Ship2CoordinateJ2] = _currentPlayer.symbol3;

                _nextPlayer = _currentPlayer;
                _currentPlayer = _currentPlayer == player1 ? player2 : player1;
   
                number++;
            }

            Console.WriteLine();

            string winner = null;

            while (winner == null)
            {
                number = _currentPlayer == player1 ? 1 : 2;

                //The beginning of the game

                if (_currentPlayer.GetType() == player1.GetType())
                _gameVisualisation.InitializeOrPrintBoard(board, _currentPlayer, _nextPlayer);

                _currentPlayer.HitTheBoard(numberOfGrids, number, board);
       
                HitStatus result = Board.CheckIfHitTheShip(board, _currentPlayer, _nextPlayer);

                if (result == HitStatus.Own)
                {
                    if (_currentPlayer.GetType() == player1.GetType())
                    {
                        Console.WriteLine("This is your ship. Please choose different coordinates.");
                        Console.WriteLine();
                    }
                    continue;
                }

                else if (result == HitStatus.Hit)
                {
                    if (_currentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've settled the ship!!!");

                    else Console.WriteLine("Your ship's been settled.");
                }
                else if (result == HitStatus.HalfHit)
                {
                    if (_currentPlayer.GetType() == player1.GetType()) Console.WriteLine("Congratulations! You've hit the ship!!!");

                    else Console.WriteLine("Your ship's been hit.");

                    if (_nextPlayer.Ship2_1_Settled == 1 && _nextPlayer.Ship2_2_Settled == 1)
                    {
                        if (_currentPlayer.GetType() == player1.GetType()) Console.WriteLine("Congratulations! You've settled the ship!!!");

                        else Console.WriteLine("Your ship's been settled.");
                    }
                }

                // An option to play without moving the ships.
                else if (result == HitStatus.Repeat)
                {
                    if (_currentPlayer.GetType() == player1.GetType()) Console.WriteLine("This grid has already been hit. Please enter different coordinates.");
                    continue;
                }
                else
                {
                    if (_currentPlayer.GetType() == player1.GetType()) Console.WriteLine("You missed.");

                    else Console.WriteLine("Your opponent missed.");
                }

                if (StartNewGame.CheckTheWinner(_nextPlayer) == true)
                {
                    winner = _currentPlayer == player1 ? "Player 1" : "Player 2";
                }

                //Board.MoveTheShip(numberOfGrids, board, random, _currentPlayer);

                _nextPlayer = _currentPlayer;
                _currentPlayer = _currentPlayer == player1 ? player2 : player1;
            }

            return winner;
        }
    }
}
