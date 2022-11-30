using sqoClassLibraryAI0502Biblio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class sqoClassDB
    {

        private static string ConnectionString = @"Provider=SQLOLEDB.1;Password=sequor;Persist Security Info=True;User ID=sa;Initial Catalog=Tetris;Data Source=SQO-061\SQLEXPRESS";
        public static OleDbConnection oDBconnection = new OleDbConnection(ConnectionString);

        public static void SaveResult(int[,] array, int Pontuacao)
        {
            OleDbCommand oCommand = null;
            string sQuery = "";
            string Grade = "";

            foreach (int Num in array)
            {
                Grade += Num + ",";
            }

            try
            {
                oDBconnection.Open();
                sQuery = "UPDATE GradeSalva SET Grade = '" + Grade + "', Pontuacao = " + Pontuacao;


                oCommand = new OleDbCommand(sQuery, oDBconnection);
                oCommand.ExecuteNonQuery();

            }
            catch (OleDbException ex)
            {
                throw (new Exception("Erro ao inserir os dados. " + ex + ""));
            }
            finally
            {
                oCommand.Dispose();
                oDBconnection.Close();
            }
        }

        public static void GetGrade(int[,] Array)
        {
            OleDbCommand oCommand = null;
            string sQuery = "";
            string DataGrade = "";
            string resultado;
            try
            {
                oDBconnection.Open();
                sQuery = "SELECT Grade FROM GradeSalva";
                oCommand = new OleDbCommand(sQuery, oDBconnection);
                resultado = (string)oCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw (new Exception("Erro ao buscar os dados. " + ex + ""));
            }
            finally
            {

                oDBconnection.Close();
            }

            string[] MatrizBanco = resultado.Split(',');
            int IndiceMatriz = 0;
            for (int linha = 0; linha < 20; linha++)
            {
                for (int coluna = 0; coluna < 10; coluna++)
                {

                    Array[linha, coluna] = int.Parse(MatrizBanco[IndiceMatriz]);
                    IndiceMatriz += 1;
                }
            }

        }

        public static int GetPontos()
        {
            OleDbCommand oCommand = null;
            string sQuery = "";
            int resultado;
            try
            {
                oDBconnection.Open();
                sQuery = "SELECT Pontuacao FROM GradeSalva";
                oCommand = new OleDbCommand(sQuery, oDBconnection);
                resultado = (int)oCommand.ExecuteScalar();
                return resultado;
            }
            catch (Exception ex)
            {
                throw (new Exception("Erro ao buscar os dados. " + ex + ""));
            }
            finally
            {
                
                oDBconnection.Close();
            }
        }

    }
}
