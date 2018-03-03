using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        private static Ship _shipHit;
        private static Player _currentPlayer;
        private static Player _nextPlayer;

        public int I { get; set; }
        public int J { get; set; }
        public string Symbol { get; set; }
        public string OverWrittenSymbol { get; set; }
        public int Settled { get; set; }

        public static bool CheckTheSecondGridOfTheShip(int Ship_I1, int Ship_J1, int Ship_I2, int Ship_J2)
        {
            if (Ship_I1 == Ship_I2 && Ship_J1 - 1 == Ship_J2 ||
                Ship_I1 == Ship_I2 && Ship_J1 + 1 == Ship_J2 ||
                Ship_I1 + 1 == Ship_I2 && Ship_J1 == Ship_J2 ||
                Ship_I1 - 1 == Ship_I2 && Ship_J1 == Ship_J2) return true;

            else return false;
        }

        public static bool SayWhereThePlayerHit(string[,] board, Player currentPlayer, Player nextPlayer, Player player1)
        {
            _currentPlayer = currentPlayer;
            _nextPlayer = nextPlayer;

            if (currentPlayer.ships.Any(s => s.Symbol == board[currentPlayer.HitI, currentPlayer.HitJ]))
            {
                if (_currentPlayer.GetType() == player1.GetType())
                {
                    Console.WriteLine("This is your ship. Please choose different coordinates.");
                    Console.WriteLine();
                }
                return false;
            }

            foreach (Ship ship in nextPlayer.ships)
            {
                if (board[currentPlayer.HitI, currentPlayer.HitJ] == ship.Symbol)
                {
                    board[currentPlayer.HitI, currentPlayer.HitJ] = GridProperties.X1.ToString();
                    ship.Settled = 1;
                    _shipHit = ship;
                    return true;
                }
            }

            board[currentPlayer.HitI, currentPlayer.HitJ] = GridProperties.O1.ToString();
            _shipHit = null;
            return true;
        }

        public static void SayWhereThePlayerHitInDetails(Player player1)
        {
            if (_nextPlayer.ships.Contains(_shipHit))
            {
                if (_nextPlayer.ships[0] == _shipHit)
                {
                    if (_currentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've settled the ship!!!");

                    else
                    Console.WriteLine("Your ship's been settled.");
                }

                else if ((_nextPlayer.ships[1] == _shipHit || _nextPlayer.ships[2] == _shipHit))
                {
                    if (_currentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've hit the ship!!!");

                    else
                    Console.WriteLine("Your ship's been hit.");

                    if (_nextPlayer.ships[1].Settled == 1 && _nextPlayer.ships[2].Settled == 1)
                    {
                        if (_currentPlayer.GetType() == player1.GetType())
                            Console.WriteLine("Congratulations! You've settled the ship!!!");

                        else
                            Console.WriteLine("Your ship's been settled.");
                    }
                }
                // An option to play without moving the ships.
                //else if (result == HitStatus.Repeat)
                //{
                //    if (currentPlayer.GetType() == player1.GetType()) Console.WriteLine("This grid has already been hit. Please enter different coordinates.");
                //    continue;
                //}
            }

            else
            {
                if (_currentPlayer.GetType() == player1.GetType()) Console.WriteLine("You missed.");

                else Console.WriteLine("Your opponent missed.");
            }
        }
    }
}
