using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        public static bool CheckTheSecondGridOfTheShip(int Ship2CoordinateI1, int Ship2CoordinateJ1, int Ship2CoordinateI2, int Ship2CoordinateJ2)
        {
            if (Ship2CoordinateI1 == Ship2CoordinateI2 && Ship2CoordinateJ1 - 1 == Ship2CoordinateJ2 ||
                Ship2CoordinateI1 == Ship2CoordinateI2 && Ship2CoordinateJ1 + 1 == Ship2CoordinateJ2 ||
                Ship2CoordinateI1 + 1 == Ship2CoordinateI2 && Ship2CoordinateJ1 == Ship2CoordinateJ2 ||
                Ship2CoordinateI1 - 1 == Ship2CoordinateI2 && Ship2CoordinateJ1 == Ship2CoordinateJ2) return true;

            else return false;
        }
    }
}
