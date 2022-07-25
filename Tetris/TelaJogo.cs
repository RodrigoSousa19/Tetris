using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public partial class TelaGame : Form
    {
        private enum TECLAS
        {
            ESQUERDA = 37,
            DIREITA = 39,
            BAIXO = 40,
            ESPACO = 32,
            CIMA = 38
        }

        #region Variaveis
        public int[] Posicoes = new int[4];
        public int offSetVertical = 0;
        public int offSetHorizontal = 0;
        public int linha;
        public int coluna;
        private Fila fila = new Fila();
        public int[,] novaPeca;
        public int[,] pecaAtual;
        public int Giro = 1;
        public int[,] Grade = new int[20, 10];
        private Panel[,] gridVisual = new Panel[20, 10];
        public int pontos = 0;
        private bool clickEsquerdaHabilitado = true;
        private bool clickDireitoHabilitado = true;
        private bool clickBaixoHabilitado = true;
        private bool clickEspacoHabilitado = true;
        private bool clickCimaHabilitado = true;
        private bool pecaFixada = false;
        private int contaBlocos = 0;
        private int linhasEliminadas = 0;
        #endregion

        public TelaGame()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CriaGrid();
            InsertPeca();
            PintarPeca();
        }

        public void CriaGrid()
        {
            for (int Linhas = 0; Linhas < 20; Linhas++)
            {
                for (int Colunas = 0; Colunas < 10; Colunas++)
                {
                    Panel panel = new Panel();

                    Grid.Controls.Add(panel, Colunas, Linhas);
                    panel.BackColor = Color.White;
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.Dock = DockStyle.Fill;
                    panel.Margin = new System.Windows.Forms.Padding(1);

                    gridVisual[Linhas, Colunas] = panel;
                }
            }
        }

        public void InsertPeca()
        {
            int[,] grid = fila.ProximoBloco.GridBlocos[Giro];
            pecaAtual = grid;
            for (int linha = 0; linha < pecaAtual.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < pecaAtual.GetLength(1); coluna++)
                {
                    if (grid[linha, coluna] != 0)
                    {
                        Grade[linha, coluna + 3] = pecaAtual[linha, coluna];
                        offSetHorizontal = 3;
                    }
                }
            }
        }

        public void PintarPeca()
        {
            for (int linha = 0; linha < 20; linha++)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] == 1)
                    {
                        switch (fila.ProximoBloco.ID)
                        {
                            case 1:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(5, 219, 242);
                                break;
                            case 2:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(36, 5, 242);
                                break;
                            case 3:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(242, 101, 19);
                                break;
                            case 4:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(242, 187, 19);
                                break;
                            case 5:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(44, 191, 4);
                                break;
                            case 6:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(117, 3, 166);
                                break;
                            case 7:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(242, 27, 84);
                                break;
                        }
                    }
                    else if (Grade[linha, coluna] == 0)
                    {
                        gridVisual[linha, coluna].BackColor = Color.White;
                    }
                }
            }

        }

        public void DescerPeca()
        {

            int auxiliar;
            for (int linha = 19; linha >= 0; linha--)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] == 1 && linha < 19)
                    {
                        auxiliar = Grade[linha, coluna];
                        if (Grade[linha + 1, coluna] == 0 && Grade[linha + 1, coluna] != 3 && clickBaixoHabilitado)
                        {
                            Grade[linha + 1, coluna] = auxiliar;
                            Grade[linha, coluna] = 0;
                            gridVisual[linha, coluna].BackColor = Color.White;

                        }
                        else
                        {
                            FixaPeca();
                            PintarPecaFixa();
                            DesabilitaMovimentacao();
                            NewPeca.Enabled = true;
                        }
                    }
                }
            }
        }

        public void LimitaMovimentoLateral()
        {
            for (int i = 19; i >= 0; i--)
            {
                if (Grade[i, 0] == 1)
                {
                    clickEsquerdaHabilitado = false;
                }
                else if (Grade[i, 9] == 1)
                {
                    clickDireitoHabilitado = false;
                }

            }
        }

        public void LimiteInferior()
        {
            for (int i = 9; i >= 0; i--)
            {
                if (Grade[19, i] == 1)
                {
                    clickBaixoHabilitado = false;
                    return;
                }
            }
        }

        public bool LimitesMatrizEsquerda(int j)
        {
            if (j > 0 && j <= 9)
            {
                return true;
            }
            else
            {
                return false;
            };
        }

        public bool LimitesMatrizDireita(int j)
        {
            if (j >= 0 && j < 9)
                return true;
            else
                return false;
        }

        public void VerificaPecaFixada()
        {
            for (int i = 19; i >= 0; i--)
            {
                for (int j = 9; j >= 0; j--)
                {
                    if (Grade[i, j] == 3)
                    {
                        if (Grade[i - 1, j] == 1)
                        {
                            clickBaixoHabilitado = false;
                        }
                        else
                        {
                            clickBaixoHabilitado = true;
                        }
                    }
                }
            }

            for (int linha = 0; linha < 20; linha++)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] == 3)
                    {
                        if (Grade[linha - 1, coluna] == 1)
                        {
                            clickBaixoHabilitado = false;
                        }
                        else
                        {
                            clickBaixoHabilitado = true;
                        }
                    }
                }
            }
        }

        public void MovimentaPecaDireita()
        {
            int auxiliar;
            for (int linha = 0; linha < 20; linha++)
            {
                for (int coluna = 9; coluna >= 0; coluna--)
                {
                    if (Grade[linha, coluna] != 0 && Grade[linha, coluna] != 3 && clickDireitoHabilitado)
                    {
                        auxiliar = Grade[linha, coluna];
                        if (LimitesMatrizDireita(coluna) && Grade[linha, coluna + 1] == 0)
                        {
                            Grade[linha, coluna + 1] = auxiliar;
                            Grade[linha, coluna] = 0;
                            gridVisual[linha, coluna].BackColor = Color.White;

                        }
                        else
                        {
                            clickDireitoHabilitado = false;
                        }
                    }
                }
            }
        }

        public void MovimentaPecaEsquerda()
        {
            int auxiliar;
            for (int linha = 19; linha >= 0; linha--)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] != 0 && Grade[linha, coluna] != 3 && clickEsquerdaHabilitado)
                    {
                        auxiliar = Grade[linha, coluna];
                        if (LimitesMatrizEsquerda(coluna) && Grade[linha, coluna - 1] == 0)
                        {
                            Grade[linha, coluna - 1] = auxiliar;
                            Grade[linha, coluna] = 0;
                            gridVisual[linha, coluna].BackColor = Color.White;
                        }
                        else
                        {
                            clickEsquerdaHabilitado = false;
                        }
                    }
                }
            }
        }

        public void HabilitaMovimentacao()
        {
            clickBaixoHabilitado = true;
            clickCimaHabilitado = true;
            clickDireitoHabilitado = true;
            clickEsquerdaHabilitado = true;
            clickEspacoHabilitado = true;
        }

        public void DesabilitaMovimentacao()
        {
            clickBaixoHabilitado = false;
            clickCimaHabilitado = false;
            clickDireitoHabilitado = false;
            clickEsquerdaHabilitado = false;
            clickEspacoHabilitado = false;
        }

        public bool FixaPeca()
        {
            for (int linha = 19; linha >= 0; linha--)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] == 1)
                    {
                        Grade[linha, coluna] = 3;
                    }
                }
            }
            return pecaFixada = true;
        }

        public void PintarPecaFixa()
        {
            for (int linha = 0; linha < 20; linha++)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] == 3)
                    {
                        switch (fila.ProximoBloco.ID)
                        {
                            case 1:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(5, 219, 242);
                                break;
                            case 2:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(36, 5, 242);
                                break;
                            case 3:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(242, 101, 19);
                                break;
                            case 4:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(242, 187, 19);
                                break;
                            case 5:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(44, 191, 4);
                                break;
                            case 6:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(117, 3, 166);
                                break;
                            case 7:
                                gridVisual[linha, coluna].BackColor = Color.FromArgb(242, 27, 84);
                                break;
                        }
                    }
                }
            }
        }

        public void InsertNovaPeca()
        {
            pecaFixada = false;
            fila.NovoBloco();
            int[,] grid = fila.ProximoBloco.GridBlocos[Giro];
            novaPeca = grid;
            offSetVertical = fila.ProximoBloco.offSetVertical;

            for (int linha = 0; linha < novaPeca.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < novaPeca.GetLength(1); coluna++)
                {
                    if (grid[linha, coluna] != 0)
                    {
                        Grade[linha, coluna + 3] = novaPeca[linha, coluna];
                        offSetHorizontal = 3;
                    }
                }
            }
        }

        private void Rotacao(int Giro)
        {
            for (int linha = 0; linha < Grade.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < Grade.GetLength(1); coluna++)
                {
                    if (Grade[linha, coluna] != 0 && Grade[linha, coluna] != 3)
                    {
                        Grade[linha, coluna] = 0;
                        AtualizaGrade(Giro);
                        PintarPeca();
                    }
                }
            }
        }

        public void AtualizaGrade(int Giro)
        {
            Bloco grid = fila.ProximoBloco;
            pecaAtual = grid.GridBlocos[Giro];

            for (int linha = 0; linha < pecaAtual.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < pecaAtual.GetLength(1); coluna++)
                {
                    if (pecaAtual[linha, coluna] == 1)
                    {
                        Grade[offSetVertical + linha, offSetHorizontal + coluna] = pecaAtual[linha, coluna];
                    }
                }
            }
        }

        private void Ticks_Tick(object sender, EventArgs e)
        {
            lblPontos.Text = "Pontos: " + pontos;
            DescerPeca();
            LimiteInferior();
            offSetVertical++;
            if (offSetVertical == 19)
            {
                offSetVertical = fila.ProximoBloco.offSetVertical;
            }
            VisualizarMatriz();

            LimpaLinhasCheias();
        }

        private void Movimentacao_Tick(object sender, EventArgs e)
        {
            PintarPeca();
            VerificaPecaFixada();
            LimitaMovimentoLateral();
        }

        private void NewPeca_Tick(object sender, EventArgs e)
        {
            Giro = 0;
            InsertNovaPeca();
            NewPeca.Enabled = false;
            HabilitaMovimentacao();
        }

        private void TelaGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)TECLAS.ESQUERDA:
                    {
                        LiberarClick(TECLAS.ESQUERDA);
                        MovimentaPecaEsquerda();
                        if (offSetHorizontal >= 0)
                        {
                            offSetHorizontal--;
                        }
                        else
                        {
                            offSetHorizontal++;
                        }
                        break;
                    }
                case (int)TECLAS.DIREITA:
                    {
                        LiberarClick(TECLAS.DIREITA);
                        MovimentaPecaDireita();
                        if (offSetHorizontal <= 9)
                        {
                            offSetHorizontal++;
                        }
                        else
                        {
                            offSetHorizontal--;
                        }
                        break;
                    }
                case (int)TECLAS.BAIXO:
                    {
                        DescerPeca();
                        pontos += 1;
                        offSetVertical += 1;
                        break;
                    }
                case (int)TECLAS.ESPACO:
                    {
                        break;
                    }
                case (int)TECLAS.CIMA:
                    {
                        Rotacao(Giro);
                        if (Giro >= 3)
                        {
                            Giro = 0;
                        }
                        else
                        {
                            Giro++;
                        }
                        break;
                    }

            }
        }

        private void LiberarClick(TECLAS teclas)
        {
            switch (teclas)
            {
                case TECLAS.ESQUERDA:
                    {
                        clickDireitoHabilitado = true;
                        break;
                    }
                case TECLAS.DIREITA:
                    {
                        clickEsquerdaHabilitado = true;
                        break;
                    }
                case TECLAS.BAIXO:
                    {

                        break;
                    }
                case TECLAS.ESPACO:
                    {
                        break;
                    }
                default:
                    break;
            }
        }

        public int LinhaVazia(int linha)
        {
            int Cont = 0;
            for (int coluna = 0; coluna < 10; coluna++)
            {
                if(Grade[linha,coluna] == 3)
                {
                    Cont++;
                }
            }
            return Cont;
        }

        private void LimpaLinha(int linha)
        {
            for(int coluna = 0; coluna < 10; coluna++)
            {
                Grade[linha, coluna] = 0;
            }
        }

        private void DesceLinhas(int linha, int linhasCheias)
        {
            for(int coluna = 0; coluna < 10; coluna++)
            {
                Grade[linha + linhasCheias, coluna] = Grade[linha, coluna];
                Grade[linha, coluna] = 0;
            }
        }

        public int LimpaLinhasCheias()
        {
            int linhasLimpas = 0;
            for(int linha = 19; linha >= 0; linha--)
            {
                if (LinhaVazia(linha) == 10)
                {
                    LimpaLinha(linha);
                    linhasLimpas++;
                }else if(linhasLimpas > 0)
                {
                    DesceLinhas(linha, linhasLimpas);
                }
            }
            return linhasLimpas;
        }

        public void VisualizarMatriz()
        {

            Console.WriteLine("offSetHorizontal: " + offSetHorizontal);
            Console.WriteLine("offSetVertical: " + offSetVertical);
            Console.WriteLine("Giro: " + Giro);
            Console.WriteLine(Posicoes[0]);

        }
    }
}

