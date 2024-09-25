using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MySqlConnector;
using static AdaptadorDB.SqlServer;

namespace AdaptadorDB
{
    
    internal class Empleado
    {

        public int IdEmpleado { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string ZonaDeTrabajo { get; set; }
        public int NumeroConsumos { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
    internal class Consumo_CSMy
    {
        public int IdConsumo { get; set; }
        public int IdEmpleado { get; set; }
        public string TipoConsumo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Boolean FormaRegistro { get; set; }
    }

    internal class MySql
    {

        public void AgregarConsumoCS(int idempleado, string tipoConsumo, DateTime fechaRegistro, bool registro)
        {
            using (MySqlConnection oconexion = new MySqlConnection("server=127.0.0.1;database=consumoempleado;uid=root;password=123456789;"))
            {
                try
                {
                    oconexion.Open();
                    MySqlCommand command = oconexion.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AgregarConsumoCS";
                    command.Parameters.AddWithValue("@IdEmpleado", idempleado);
                    command.Parameters.AddWithValue("@TipoConsumo", tipoConsumo);
                    command.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);
                    command.Parameters.AddWithValue("@Registro", registro ? 1 : 0); // BIT en MySQL
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                }
                finally
                {
                    oconexion.Close();
                }
            }
        }

        public void InsertarEmpleado(string numeroDocumento, string nombreCompleto, string zonaDeTrabajo, int consumos, bool estado, DateTime hoy)
        {
            using (MySqlConnection oconexion = new MySqlConnection("server=127.0.0.1;database=consumoempleado;uid=root;password=123456789;"))
            {
                oconexion.Open();
                MySqlCommand command = oconexion.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertarEmpleado";

                command.Parameters.AddWithValue("@p_NumeroDocumento", numeroDocumento);
                command.Parameters.AddWithValue("@p_NombreCompleto", nombreCompleto);
                command.Parameters.AddWithValue("@p_ZonaDeTrabajo", zonaDeTrabajo);
                command.Parameters.AddWithValue("@p_NumeroConsumos", consumos);
                command.Parameters.AddWithValue("@p_Estado", estado);
                command.Parameters.AddWithValue("@p_FechaRegistro", hoy);
                command.ExecuteNonQuery();
            }
        }
        public List<Consumo_CSMy> ListarCs()
        {
            List<Consumo_CSMy> lista = new List<Consumo_CSMy>();
            using (MySqlConnection oconexion = new MySqlConnection("server=127.0.0.1;database=consumoempleado;uid=root;password=123456789;"))
            {
                try
                {
                    string query = "CALL ObtenerConsumoCS();";  // Usamos CALL para procedimientos en MySQL
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Consumo_CSMy
                            {
                                IdConsumo = Convert.ToInt32(reader["IdConsumo"]),
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                TipoConsumo = reader["TipoConsumo"].ToString(),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                                FormaRegistro = Convert.ToBoolean(reader["Registro"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Consumo_CSMy>();
                    // Manejo de excepciones
                }
                finally
                {
                    oconexion.Close();
                }
            }
            return lista;
        }
        public List<Empleado> ListarE()
        {
            List<Empleado> lista = new List<Empleado>();
            using (MySqlConnection oconexion = new MySqlConnection("server=127.0.0.1;database=consumoempleado;uid=root;password=123456789;"))
            {
                try
                {
                    string query = "SELECT * FROM Empleado";
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                NumeroDocumento = reader["NumeroDocumento"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                ZonaDeTrabajo = reader["ZonaDeTrabajo"].ToString(),
                                NumeroConsumos = Convert.ToInt32(reader["NumeroConsumos"]),
                                Estado = Convert.ToBoolean(reader["Estado"]),
                                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Empleado>();
                    // Manejo del error
                }
                oconexion.Close();
            }
            return lista;
        }
       
    } 
    class ListarEmpleado
        {
            private MySql _queryEmpleado = new MySql();
            public List<Empleado> Listar()
            {
                return _queryEmpleado.ListarE();
            }
        }
        class ListarConsumoCSMy
        {
            private MySql _queryConsumoCS = new MySql();
            public List<Consumo_CSMy> Listar()
            {
                return _queryConsumoCS.ListarCs();
            }
        }
}
