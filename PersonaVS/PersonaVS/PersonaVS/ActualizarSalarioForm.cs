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
    public partial class ActualizarSalarioForm : Form
    {
        private Empleado empleado;

        public ActualizarSalarioForm(Empleado empleado)
        {
            InitializeComponent();
            this.empleado = empleado;
            // Agregar manejadores de eventos para los botones
            btnActualizar.Click += btnActualizar_Click;
            btnCancelar.Click += btnCancelar_Click;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el nuevo salario ingresado por el usuario
                double nuevoSalario = Convert.ToDouble(txtNuevoSalario.Text);

                // Actualizar el salario del empleado
                empleado.Salario = nuevoSalario;

                // Realizar una solicitud al servidor para actualizar el salario del empleado
                RestClient client = new RestClient("http://localhost:8080");
                RestRequest request = new RestRequest($"/empleados/{empleado.NumeroEmpleado}", Method.Put);
                request.AddJsonBody(empleado); // Enviar el objeto empleado actualizado en el cuerpo de la solicitud
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Salario actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el salario del empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Cerrar el formulario
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Ingrese un valor de salario válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Cerrar el formulario
            this.Close();
        }
    }
}