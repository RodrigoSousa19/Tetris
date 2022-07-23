using System;
using System.Collections.Generic;
namespace Tetris
{
    public abstract class Bloco
    {
        public abstract List<int[,]> GridBlocos{ get; }
        public abstract int ID { get; }
        public abstract int offSetHorizontal { get; }
        public abstract int offSetVertical { get; }

    }
}
