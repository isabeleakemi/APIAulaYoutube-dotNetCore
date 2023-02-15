using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("Clientes")]
    public class Cliente
    {
        private static DBContexto db = new DBContexto();
        public Cliente() { }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public Cliente Salvar()
        {
            if (this.Id > 0)
            {
                db.Clientes.Update(this);
            }
            else
            {
                db.Clientes.Add(this);
            }
            db.SaveChanges();
            return this;
        }

        public static List<Cliente> Todos()
        {
            return db.Clientes.ToList();
        }

        public static List<Cliente> Todos_Com_SqlConnection()
        {
            var lista = new List<Cliente>();

            SqlConnection conn = new SqlConnection(Conexao.Dados);
            conn.Open();

            SqlCommand cmd = new SqlCommand("select * from clientes", conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader["Nome"].ToString(),
                    Telefone = reader["Telefone"].ToString(),
                    Endereco = reader["Endereco"].ToString()
                });
            }

            conn.Close();
            conn.Dispose();
            cmd.Dispose();

            return lista;
        }

        public Cliente Salvar_Com_SqlConnection()
        {
            using (SqlConnection conn = new SqlConnection(Conexao.Dados))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("CriarCliente @nome, @telefone, @endereco", conn);
                cmd.Parameters.Add("@nome", SqlDbType.VarChar);
                cmd.Parameters["@nome"].Value = this.Nome;

                cmd.Parameters.Add("@telefone", SqlDbType.VarChar);
                cmd.Parameters["@telefone"].Value = this.Telefone;

                cmd.Parameters.Add("@endereco", SqlDbType.VarChar);
                cmd.Parameters["@endereco"].Value = this.Endereco;

                this.Id = Convert.ToInt32(cmd.ExecuteScalar());

                conn.Close();
            }

            return this;
        }
    }
}
