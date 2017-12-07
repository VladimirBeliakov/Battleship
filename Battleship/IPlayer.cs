using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    interface IPlayer
    {
        string symbol1 { get; set; }
        string symbol2 { get; set; }
        string symbol3 { get; set; }

        int Ship1CoordinateI { get; set; }
        int Ship1CoordinateJ { get; set; }

        int Ship2CoordinateI1 { get; set; }
        int Ship2CoordinateJ1 { get; set; }
        int Ship2CoordinateI2 { get; set; }
        int Ship2CoordinateJ2 { get; set; }

        int HitCoordinateI { get; set; }
        int HitCoordinateJ { get; set; }

        string OverWrittenSymmbolShip1 { get; set; }
        string OverWrittenSymmbolShip2S1 { get; set; }
        string OverWrittenSymmbolShip2S2 { get; set; }

        int Ship1Settled { get; set; }
        int Ship2_1_Settled { get; set; }
        int Ship2_2_Settled { get; set; }

        void PutTheShipOnTheBoard(int numberOfGrids, int number, string[,] board);
        void HitTheBoard(int numberOfGrids, int number, string[,] board);
    }
}
