using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class BlocoJ : Bloco
    {
        private readonly List<int[,]> Grid = new List<int[,]>() {
             new int[,]{
                {1,0,0},
                {1,1,1},
                {0,0,0}
            },
            new int[,]
            {
                {0,1,1},
                {0,1,0},
                {0,1,0}
            },
            new int[,]
            {
                {0,0,0},
                {1,1,1},
                {0,0,1}
            },
            new int[,]
            {
                {0,1,0},
                {0,1,0},
                {1,1,0}
            }
        };

        public override int ID => 2;

        public override List<int[,]> GridBlocos => Grid;

        public override int offSetHorizontal => 1;

        public override int offSetVertical => 1;
    }
}
