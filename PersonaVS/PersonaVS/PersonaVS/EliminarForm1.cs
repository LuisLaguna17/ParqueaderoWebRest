using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PersonaVS.ListarForm;

namespace PersonaVS
{
    public partial class EliminarForm1 : Form
    {
        private Empleado empleado;
        public EliminarForm1(Empleado empleado)
        {
            this.empleado = empleado;
            InitializeComponent();
            lbl.Text = $"¿Desea eliminar al empleado con ID: {empleado.Id} y número de empleado: {empleado.NumeroEmpleado}?";

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Realizar una solicitud al servidor para eliminar al empleado
            RestClient client = new RestClient("http://localhost:8080");
            RestRequest request = new RestRequest($"/empleados/{empleado.NumeroEmpleado}", Method.Delete);
            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                MessageBox.Show("Empleado eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al eliminar al empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cerrar el formulario
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
