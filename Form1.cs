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

        }
    }
}
