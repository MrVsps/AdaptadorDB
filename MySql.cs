using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AdaptadorDB
{
    internal class MySql
    {

        internal class Consumo_CS
        {
            public int IdConsumo { get; set; }
            public int IdEmpleado { get; set; }
            public string TipoConsumo { get; set; }
            public DateTime FechaRegistro { get; set; }
            public Boolean FormaRegistro { get; set; }
        }

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
    }
}
