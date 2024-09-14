using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptadorDB
{
    internal class SqlServer
    {
        public List<ConsumoCS> ListarCs()
        {
            List<ConsumoCS> lista = new List<ConsumoCS>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.catena))
            {
                try
                {
                    string query = "exec ObtenerConsumoCS;";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ConsumoCS
                            {
                                IdConsumo = Convert.ToInt32(reader["IdConsumo"]),
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                TipoConsumo = reader["TipoConsumo"].ToString(),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<ConsumoCS>();
                }
                oconexion.Close();
            }

            return lista;
        }
    }
}
