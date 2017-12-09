using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
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

        public static void AnalyseTheResult(Player currentPlayer, Player nextPlayer, Ship result, Player player1)
        {
            if (nextPlayer.ships.Contains(result))
            {
                if (nextPlayer.ships[0] == result)
                {
                    if (currentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've settled the ship!!!");

                    else
                    Console.WriteLine("Your ship's been settled.");
                }

                if ((nextPlayer.ships[1] == result || nextPlayer.ships[2] == result))
                {
                    if (currentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've hit the ship!!!");

                    else
                    Console.WriteLine("Your ship's been hit.");
                }

                if (nextPlayer.ships[1].Settled == 1 && nextPlayer.ships[2].Settled == 1)
                {
                    if (currentPlayer.GetType() == player1.GetType())
                    Console.WriteLine("Congratulations! You've settled the ship!!!");

                    else
                    Console.WriteLine("Your ship's been settled.");
                }

                // An option to play without moving the ships.
                //else if (result == HitStatus.Repeat)
                //{
                //    if (currentPlayer.GetType() == player1.GetType()) Console.WriteLine("This grid has already been hit. Please enter different coordinates.");
                //    continue;
                //}

                else
                {
                    if (currentPlayer.GetType() == player1.GetType()) Console.WriteLine("You missed.");

                    else Console.WriteLine("Your opponent missed.");
                }
            }
        }
    }
}
