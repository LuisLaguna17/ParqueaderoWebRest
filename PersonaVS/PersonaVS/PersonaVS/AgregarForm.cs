using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PersonaVS
{
    public partial class AgregarForm : Form
    {
        public AgregarForm()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los campos de texto
                string id = textId.Text;
                string placaVehiculo = textPlaca.Text;
                double salario = Convert.ToDouble(textSalario.Text);
                int numeroEmpleado = Convert.ToInt32(textNumero.Text);
                bool activo = chkActivo.Checked;

                // Verificar si todos los campos están completos
                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(placaVehiculo) || string.IsNullOrWhiteSpace(textSalario.Text) || string.IsNullOrWhiteSpace(textNumero.Text))
                {
                    throw new Exception("Por favor, rellena todos los campos");
                }

                // Enviar la solicitud HTTP al servidor para agregar el empleado
                RestClient client = new RestClient("http://localhost:8080");
                RestRequest request = new RestRequest("/empleados", Method.Post);
                request.AddJsonBody(new
                {
                    id = id,
                    placaVehiculo = placaVehiculo,
                    salario = salario,
                    numeroEmpleado = numeroEmpleado,
                    activo = activo,
                });
                RestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    MessageBox.Show("Empleado agregado correctamente", "Éxito");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("El empleado ya existe", "Error");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresa un formato válido para el salario y el número de empleado", "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LimpiarCampos()
        {
            textId.Text = "";
            textPlaca.Text = "";
            textSalario.Text = "";
            textNumero.Text = "";
            chkActivo.Checked = false;
        }
    
    }
}
