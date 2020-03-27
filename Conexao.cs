using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LojaCL
{
    class clConexao
    {
        //conectar SqlServer Express, com a String de conexão.
        private static string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
        //representa a conxão com o banco.
        private static SqlConnection con = null;
        // metodo que obtem a conexão com banco.
        public static SqlConnection obterConexao()
        {
            con = new SqlConnection(str);
            //verificar conexão
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }
            catch (SqlException sqle)
            {
                con = null;
            }
            return con;
        }
        public static void fecharConexao()
        {
            //se receber conexão nula, ele fecha a conexão.
            if (con != null)
            {
                con.Close();
            }
        }
    }
}
