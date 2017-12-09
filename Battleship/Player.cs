using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    interface Player
    {
        int HitI { get; set; }
        int HitJ { get; set; }

        List<Ship> ships { get; set; }

        void InitializeTheShips();

        void PutTheShipOnTheBoard(int number, string[,] board);

        void HitTheBoard(int number, string[,] board);
    }
}
