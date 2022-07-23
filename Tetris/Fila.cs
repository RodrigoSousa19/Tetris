using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Fila
    {
        private readonly Random ran = new Random();
        private readonly Bloco[] blocos = new Bloco[]
        {
            new BlocoI(),
            new BlocoJ(),
            new BlocoL(),
            new BlocoO(),
            new BlocoS(),
            new BlocoT(),
            new BlocoZ()
        };

        public Bloco ProximoBloco { get; private set;}
        public Fila()
        {
            ProximoBloco = BlocoAleatorio();
        }
        private Bloco BlocoAleatorio()
        {
            return blocos[ran.Next(blocos.Length)];
        }

        public Bloco NovoBloco()
        {
            Bloco BlocoAtual = ProximoBloco;
            do
            {
                ProximoBloco = BlocoAleatorio();
            } while (BlocoAtual.ID == ProximoBloco.ID);
            return BlocoAtual;
        }
    }
}
