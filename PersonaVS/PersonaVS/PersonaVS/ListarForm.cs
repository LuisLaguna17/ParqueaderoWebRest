using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonaVS
{
    public partial class ListarForm : Form
    {
        public ListarForm()
        {
            InitializeComponent();
            CargarEmpleados();
        }
        // Definir una clase para representar la estructura de un empleado
        public class Empleado
        {
            public string Id { get; set; }
            public DateTime HoraLlegada { get; set; }
            public string PlacaVehiculo { get; set; }
            public double Salario { get; set; }
            public int NumeroEmpleado { get; set; }
            public bool Activo { get; set; }
        }

        // En el método CargarEmpleados
        private void CargarEmpleados()
        {
            // Obtener los empleados desde el servicio web
            RestClient client = new RestClient("http://localhost:8080");
            RestRequest request = new RestRequest("/empleados", Method.Get);
            RestResponse<List<Empleado>> response = client.Execute<List<Empleado>>(request);

            if (response.IsSuccessful)
            {
                // Limpiar las columnas existentes en la grilla
                dataGridView1.Columns.Clear();

                // Asignar la lista de empleados al control DataGridView
                dataGridView1.DataSource = response.Data;

                // Ajustar el tamaño de las columnas automáticamente
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            else
            {
                MessageBox.Show("La lista se encuentra vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
