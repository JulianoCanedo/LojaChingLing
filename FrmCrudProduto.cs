using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LojaCL
{
    public partial class FrmCrudProduto : Form
    {
        SqlConnection con = clConexao.obterConexao();
        public FrmCrudProduto()
        {
            InitializeComponent();
        }

        public void CarregaDgvProduto()
        {
            //SqlConnection con = clConexao.obterConexao();
            //String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
            // SqlConnection con = new SqlConnection(str);
            String query = "select * from produto";
            SqlCommand cmd = new SqlCommand(query, con);
            clConexao.obterConexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable produto = new DataTable();
            da.Fill(produto);
            DgvProduto.DataSource = produto;
            clConexao.fecharConexao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                //String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                //SqlConnection con = new SqlConnection(str);

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "InserirProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                cmd.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal,3).Value = txtValor.Text;
                clConexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvProduto();
                MessageBox.Show("Registro inserido com sucesso!", "Cadastro", MessageBoxButtons.OK);
                clConexao.fecharConexao();
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
               // SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "AtualizarProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@nome", this.txtNome.Text);
                cmd.Parameters.AddWithValue("@tipo", this.txtTipo.Text);
                cmd.Parameters.AddWithValue("@quantidade", this.txtQuantidade.Text);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal, 3).Value = txtValor.Text;
                clConexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvProduto();
                MessageBox.Show("Registro atualizado com sucesso!", "Atualizar Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clConexao.fecharConexao();
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                //String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                //SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "ExcluirProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                clConexao.obterConexao();
                cmd.ExecuteNonQuery();
                CarregaDgvProduto();
                MessageBox.Show("Registro apagado com sucesso!", "Excluir Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clConexao.fecharConexao();
                txtId.Text = "";
                txtNome.Text = "";
                txtTipo.Text = "";
                txtQuantidade.Text = "";
                txtValor.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                //String str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programas\\LojaChingLing-master\\DbLoja.mdf;Integrated Security=True;Connect Timeout=30";
                //SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "LocalizarProduto";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                clConexao.obterConexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["Id"].ToString();
                    txtNome.Text = rd["nome"].ToString();
                    txtTipo.Text = rd["tipo"].ToString();
                    txtQuantidade.Text = rd["quantidade"].ToString();
                    txtValor.Text = rd["valor"].ToString();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
            }
        }

        private void FrmCrudProduto_Load(object sender, EventArgs e)
        {
            CarregaDgvProduto();
        }

        private void DgvProduto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DgvProduto.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtTipo.Text = row.Cells[2].Value.ToString();
                txtQuantidade.Text = row.Cells[3].Value.ToString();
                txtValor.Text = row.Cells[4].Value.ToString();
            }
        }
    }
}
