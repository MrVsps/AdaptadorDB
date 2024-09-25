using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static AdaptadorDB.SqlServer;

namespace AdaptadorDB
{
    internal class SqlServer
    {
        internal class ConsumoCS
        {
            public int IdConsumo { get; set; }
            public int IdEmpleado { get; set; }
            public string TipoConsumo { get; set; }
            public DateTime FechaRegistro { get; set; }
        }

        public List<ConsumoCS> ListarCs()
        {
            List<ConsumoCS> lista = new List<ConsumoCS>();
            using (SqlConnection oconexion = new SqlConnection("Data Source=(local);Initial Catalog=ConsumoEmpleado;Integrated Security=True"))
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


        internal class EmpleadoCs
        {
            public int IdEmpleado { get; set; }
            public string NumeroDocumento { get; set; }
            public string NombreCompleto { get; set; }
            public string ZonaDeTrabajo { get; set; }
            public int NumeroConsumos { get; set; }
            public string Imagen { get; set; }
            public bool Estado { get; set; }
            public DateTime FechaRegistro { get; set; }


        }


        public List<EmpleadoCs> Listar()
        {
            List<EmpleadoCs> lista = new List<EmpleadoCs>();
            using (SqlConnection oconexion = new SqlConnection("Data Source=(local);Initial Catalog=ConsumoEmpleado;Integrated Security=True"))
            {
               
                {
                    try
                    {
                        string query = "select * from Empleado";
                        SqlCommand cmd = new SqlCommand(query, oconexion);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new EmpleadoCs
                                {
                                    IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                    NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                    NombreCompleto = reader["NombreCompleto"].ToString(),
                                    ZonaDeTrabajo = reader["ZonaDeTrabajo"].ToString(),
                                    NumeroConsumos = Convert.ToInt32(reader["NumeroConsumos"]),
                                    Imagen = reader["Imagen"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lista = new List<EmpleadoCs>();

                    }
                    oconexion.Close();
                }
                return lista;
            }
        }
        public void InsertarEmpleado(string numeroDocumento, string nombreCompleto, string zonaDeTrabajo, int consumos, string Ima, bool estado, DateTime hoy)
        {

            using (SqlConnection oconexion = new SqlConnection("Data Source=(local);Initial Catalog=ConsumoEmpleado;Integrated Security=True"))
            {
                oconexion.Open();
                SqlCommand command = oconexion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertarEmpleado";

                command.Parameters.AddWithValue("@NumeroDocumento", numeroDocumento);
                command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                command.Parameters.AddWithValue("@ZonaDeTrabajo", zonaDeTrabajo);
                command.Parameters.AddWithValue("@NumeroConsumos", consumos);
                command.Parameters.AddWithValue("@Imagen", Ima);
                command.Parameters.AddWithValue("@Estado", estado);
                command.Parameters.AddWithValue("@FechaRegistro", hoy);
                command.ExecuteNonQuery();
            }
        }
        public void AgregarConsumoCS(int idempleado, string tipoConsumo, DateTime fechaRegistro)
        {
            using (SqlConnection oconexion = new SqlConnection("Data Source=(local);Initial Catalog=ConsumoEmpleado;Integrated Security=True"))
            {
                oconexion.Open();
                SqlCommand command = oconexion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AgregarConsumoCS";  // Asegúrate de que este nombre coincida con el procedimiento almacenado
                command.Parameters.AddWithValue("@IdEmpleado", idempleado);
                command.Parameters.AddWithValue("@TipoConsumo", tipoConsumo);
                command.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);
                command.ExecuteNonQuery();
            }
        }
    }
    class ListarConsumoCS
    {
        private  SqlServer _queryConsumoCS = new SqlServer();
        public List<ConsumoCS> Listar()
        {
            return _queryConsumoCS.ListarCs();
        }
    }

    class listaruEmpleado
    {
        private SqlServer _queryEmpleado = new SqlServer();
        public List<EmpleadoCs> Listar()
        {
            return _queryEmpleado.Listar();
        }
    }

}
