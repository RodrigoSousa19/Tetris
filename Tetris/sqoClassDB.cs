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

        public static void SaveResult(int[,] array,int Pontuacao)
        {
            OleDbCommand oCommand = null;
            String sQuery = "";
            for (int linhas = 0; linhas < 20; linhas++)
            {
                for (int colunas = 0; colunas < 10; colunas++)
                {
                    try
                    {
                        oDBconnection.Open();
                        sQuery = "UPDATE GridSalvo SET Coluna"+Convert.ToString(colunas) + " = " + array[linhas,colunas] +", Pontuacao = " + Pontuacao + " WHERE Linha = " + linhas;


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
            }
        }
    }
}
