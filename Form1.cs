using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AdaptadorDB.SqlServer;
using static AdaptadorDB.MySql;

namespace AdaptadorDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<EmpleadoCs> lista = new listaruEmpleado().Listar();
            List<ConsumoCS> consumoCs = new ListarConsumoCS().Listar();
            MySql mySql = new MySql();
            SqlServer sqlServer = new SqlServer();
            foreach (EmpleadoCs es in lista)
            {
                mySql.InsertarEmpleado(es.NumeroDocumento, es.NombreCompleto,es.ZonaDeTrabajo,es.NumeroConsumos,es.Estado,es.FechaRegistro);
            }
            foreach (ConsumoCS cs in consumoCs)
            {
                mySql.AgregarConsumoCS(cs.IdEmpleado, cs.TipoConsumo, cs.FechaRegistro, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Empleado> lista = new ListarEmpleado().Listar();
            List<Consumo_CSMy> consumoCs = new ListarConsumoCSMy().Listar();
            MySql mySql = new MySql();
            SqlServer sqlServer = new SqlServer();

            foreach (Empleado es in lista)
            {
                sqlServer.InsertarEmpleado(es.NumeroDocumento, es.NombreCompleto, es.ZonaDeTrabajo, es.NumeroConsumos, "",es.Estado, es.FechaRegistro);
            }
            foreach (Consumo_CSMy cs in consumoCs)
            {
                sqlServer.AgregarConsumoCS(cs.IdEmpleado, cs.TipoConsumo, cs.FechaRegistro);
            }
        }

    }
}
