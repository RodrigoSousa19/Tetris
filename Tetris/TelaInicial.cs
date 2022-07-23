using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            lblData.Text =  DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            TelaGame Navegar = new TelaGame();

            Navegar.Show();
        }

        private void Hora_Tick(object sender, EventArgs e)
        {

            lblData.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
