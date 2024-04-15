﻿using RestSharp;
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
using static System.Net.Mime.MediaTypeNames;

namespace PersonaVS
{
    public partial class BuscarFormActualizar : Form
    {
        private Empleado empleado;
        public BuscarFormActualizar()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string tipoBusqueda = string.Empty;
                string valorBusqueda = string.Empty;

                // Verificar si se proporcionó un número de empleado y si es válido
                if (!string.IsNullOrWhiteSpace(txtNumEmpleado.Text) && int.TryParse(txtNumEmpleado.Text, out _))
                {
                    tipoBusqueda = "numero";
                    valorBusqueda = txtNumEmpleado.Text;
                }
                // Verificar si se proporcionó un ID
                else if (!string.IsNullOrWhiteSpace(txtId.Text))
                {
                    tipoBusqueda = "id";
                    valorBusqueda = txtId.Text;
                }
                // Verificar si se proporcionó una placa
                else if (!string.IsNullOrWhiteSpace(txtPlaca.Text))
                {
                    tipoBusqueda = "placa";
                    valorBusqueda = txtPlaca.Text;
                }
                else
                {
                    throw new Exception("Por favor, introduce un valor válido para buscar al empleado");
                }

                // Realizar la solicitud al servidor para obtener los detalles del empleado
                RestClient client = new RestClient("http://localhost:8080");
                RestRequest request = new RestRequest($"/empleados/{tipoBusqueda}/{valorBusqueda}", Method.Get);
                RestResponse<Empleado> response = client.Execute<Empleado>(request);

                if (response.IsSuccessful)
                {
                    // Mostrar los detalles del empleado en el mensaje
                    empleado = response.Data;
                    string mensaje = $"ID: {empleado.Id}\n" +
                                     $"Número de empleado: {empleado.NumeroEmpleado}\n" +
                                     $"Hora de llegada: {empleado.HoraLlegada}\n" +
                                     $"Placa de vehículo: {empleado.PlacaVehiculo}\n" +
                                     $"Salario: {empleado.Salario}\n" +
                                     $"Activo: {empleado.Activo}";

                    MessageBox.Show(mensaje, "Detalles del Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el formulario para actualizar el salario
                    ActualizarSalarioForm actualizarSalarioForm = new ActualizarSalarioForm(empleado);
                    actualizarSalarioForm.Show();
                }
                else
                {
                    // Si no se encuentra el empleado, mostrar un mensaje de error
                    MessageBox.Show("Este empleado no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   

    }
}
