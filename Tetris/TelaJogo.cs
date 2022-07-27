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
        public int offSetVertical = 0;
        public int offSetHorizontal = 0;
        public int linha;
        public int coluna;
        private Fila fila = new Fila();
        public int[,] novaPeca;
        public int[,] pecaAtual;
        public int Giro = 0;
        public int[,] Grade = new int[20, 10];
        private Panel[,] gridVisual = new Panel[20, 10];
        public int pontos = 0;
        private bool pecaFixada = false;
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
                    panel.Margin = new System.Windows.Forms.Padding(0);

                    gridVisual[Linhas, Colunas] = panel;
                }
            }
        }

        public void InsertPeca()
        {
            int[,] grid = fila.ProximoBloco.GridBlocos[0];
            pecaAtual = grid;
            for (int linha = 0; linha < pecaAtual.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < pecaAtual.GetLength(1); coluna++)
                {
                    if (grid[linha, coluna] != 0)
                    {
                        if (fila.ProximoBloco.ID != 1)
                        {
                            Grade[linha, coluna + 3] = pecaAtual[linha, coluna];
                            offSetHorizontal = 3;
                        }
                        else
                        {
                            Grade[linha - 1, coluna + 3] = pecaAtual[linha, coluna];
                            offSetHorizontal = 3;
                        }
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
                    else
                    {
                        gridVisual[linha, coluna].BackColor = Color.Gray;
                    }
                }
            }

        }

        public void DescerPeca()
        {
            if (PodeDescer())
            {
                for (int linha = 19; linha >= 0; linha--)
                {
                    for (int coluna = 0; coluna < 10; coluna++)
                    {
                        if (Grade[linha, coluna] == 1 && linha < 19)
                        {
                            Grade[linha + 1, coluna] = Grade[linha, coluna];
                            Grade[linha, coluna] = 0;
                        }
                    }
                }
            }
            else
            {
                FixaPeca();
                NewPeca.Enabled = true;
            }
        }

        public void MovimentaPecaDireita()
        {
            if (PodeMoverDireita())
            {
                for (int linha = 0; linha < 20; linha++)
                {
                    for (int coluna = 9; coluna >= 0; coluna--)
                    {
                        if (Grade[linha, coluna] == 1)
                        {
                            Grade[linha, coluna + 1] = Grade[linha, coluna];
                            Grade[linha, coluna] = 0;
                        }
                    }
                }
                offSetHorizontal++;
            }
        }

        public void MovimentaPecaEsquerda()
        {
            if (PodeMoverEsquerda())
            {
                for (int linha = 19; linha >= 0; linha--)
                {
                    for (int coluna = 0; coluna < 10; coluna++)
                    {
                        if (Grade[linha, coluna] == 1)
                        {
                            Grade[linha, coluna - 1] = Grade[linha, coluna];
                            Grade[linha, coluna] = 0;
                        }
                    }
                }
                offSetHorizontal--;
            }
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
                        if (fila.ProximoBloco.ID != 1)
                        {
                            Grade[linha, coluna + 3] = novaPeca[linha, coluna];
                            offSetHorizontal = 3;
                        }
                        else
                        {
                            Grade[linha - 1, coluna + 3] = novaPeca[linha, coluna];
                            offSetHorizontal = 3;
                        }
                    }
                }
            }
        }

        private void Rotacao()
        {
            for (int linha = 0; linha < 20; linha++)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (Grade[linha, coluna] == 1)
                    {
                        if (PodeRotacionar())
                        {
                            Grade[linha, coluna] = 0;
                            AtualizaGrade();
                            PintarPeca();
                        }
                    }
                }
            }
        }

        public void AtualizaGrade()
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
            DescerPeca();

            offSetVertical++;

            if (offSetVertical == 19)
            {
                offSetVertical = 0;
            }

            VisualizarMatriz();

            PodeDescer();

            GameOver();

            ResetGame();
        }

        private void Movimentacao_Tick(object sender, EventArgs e)
        {
            lblPontos.Text = "Pontos: " + pontos;
            PintarPeca();
            LimpaLinhasCheias();
        }

        private void NewPeca_Tick(object sender, EventArgs e)
        {
            Giro = 0;
            InsertNovaPeca();
            NewPeca.Enabled = false;
        }

        private void TelaGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)TECLAS.ESQUERDA:
                    {
                        MovimentaPecaEsquerda();
                        break;
                    }
                case (int)TECLAS.DIREITA:
                    {

                        MovimentaPecaDireita();
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
                        DesligaTimers();
                        sqoClassDB.SaveResult(Grade,pontos);
                        break;
                    }
                case (int)TECLAS.CIMA:
                    {
                        Rotacao();
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

        public void VisualizarMatriz()
        {

            Console.WriteLine("offSetHorizontal: " + offSetHorizontal);
            Console.WriteLine("offSetVertical: " + offSetVertical);
            Console.WriteLine("Giro: " + Giro);
        }

        public void Pontuacao(int LinhasLimpas)
        {
            switch (LinhasLimpas)
            {
                case 1:
                    pontos += 100;
                    break;
                case 2:
                    pontos += 200;
                    break;
                case 3:
                    pontos += 400;
                    break;
                case 4:
                    pontos += 800;
                    break;
            }
        }

        public bool PodeDescer()
        {
            int podeDescerPeca = 0;

            for (int linha = 19; linha >= 0; linha--)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (linha < 19)
                    {
                        if (Grade[linha, coluna] == 1 && Grade[linha + 1, coluna] != 3)
                        {
                            podeDescerPeca++;
                        }
                    }
                }
            }
            if (podeDescerPeca == 4) return true;
            else return false;
        }

        public bool PodeMoverEsquerda()
        {
            int podeMoverPeca = 0;

            for (int linha = 19; linha >= 0; linha--)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (coluna > 0)
                    {
                        if (Grade[linha, coluna] == 1 && Grade[linha, coluna - 1] != 3)
                        {
                            podeMoverPeca++;
                        }
                    }
                }
            }
            if (podeMoverPeca == 4) return true;
            else return false;
        }
        public bool PodeMoverDireita()
        {
            int podeMoverPeca = 0;

            for (int linha = 19; linha >= 0; linha--)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {
                    if (coluna < 9)
                    {
                        if (Grade[linha, coluna] == 1 && Grade[linha, coluna + 1] != 3)
                        {
                            podeMoverPeca++;
                        }
                    }
                }
            }
            if (podeMoverPeca == 4) return true;
            else return false;
        }

        public int LinhaVazia(int linha)
        {
            int Cont = 0;
            for (int coluna = 0; coluna < 10; coluna++)
            {
                if (Grade[linha, coluna] == 3)
                {
                    Cont++;
                }
            }
            return Cont;
        }

        private void LimpaLinha(int linha)
        {
            for (int coluna = 0; coluna < 10; coluna++)
            {
                Grade[linha, coluna] = 0;
            }
        }

        private void DesceLinhas(int linha, int linhasCheias)
        {
            for (int coluna = 0; coluna < 10; coluna++)
            {
                Grade[linha + linhasCheias, coluna] = Grade[linha, coluna];
                Grade[linha, coluna] = 0;
            }
        }

        public int LimpaLinhasCheias()
        {
            int linhasLimpas = 0;
            for (int linha = 19; linha >= 0; linha--)
            {
                if (LinhaVazia(linha) == 10)
                {
                    LimpaLinha(linha);
                    linhasLimpas++;
                }
                else if (linhasLimpas > 0)
                {
                    DesceLinhas(linha, linhasLimpas);

                }
            }
            Pontuacao(linhasLimpas);
            return linhasLimpas;
        }

        public void DesligaTimers()
        {
            Ticks.Enabled = false;
            NewPeca.Enabled = false;
            Movimentacao.Enabled = false;
        }

        public bool PodeRotacionar()
        {
            int podeRotacionar = 0;
            int[,] grid = fila.ProximoBloco.GridBlocos[Giro];
            pecaAtual = grid;
            for (int linha = 0; linha < pecaAtual.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < pecaAtual.GetLength(1); coluna++)
                {
                    if (offSetHorizontal > 0 && offSetHorizontal < 8)
                    {
                        if (grid[linha, coluna] == 1 && Grade[linha + offSetVertical, coluna + offSetHorizontal] != 3)
                        {
                            podeRotacionar++;
                        }
                    }
                    else if (offSetHorizontal >= 8)
                    {
                        MovimentaPecaEsquerda();
                    }
                    else if (offSetHorizontal <= 0)
                    {
                        MovimentaPecaDireita();
                    }
                }
            }

            if (podeRotacionar == 4) return true;
            else return false;
        }

        public bool GameOver()
        {
            if (Grade[1, 4] == 3 || Grade[0, 4] == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetGame()
        {
            if (GameOver())
            {
                DesligaTimers();
                MessageBox.Show("GameOver!", "Fim da partida!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }
    }
}

