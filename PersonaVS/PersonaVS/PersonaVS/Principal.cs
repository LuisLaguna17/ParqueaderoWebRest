using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace PersonaVS
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarForm form = new AgregarForm();
            form.MdiParent = this;  
            form.Show();
        }
        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscarFormActualizar form = new BuscarFormActualizar();
            form.MdiParent = this;
            form.Show();
        }

        private void eliminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuscEliminarForm form = new BuscEliminarForm();
            form.MdiParent = this;
            form.Show();
        }

        private void todosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListarForm form = new ListarForm();
            form.MdiParent = this;
            form.Show();
        }

        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListarActivosForm listarActivosForm = new ListarActivosForm();
            listarActivosForm.Show();
        }

        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Elaborado por:\n Luis Carlos Laguna\n Victor Andres Moreno\n Parqueadero versión 1.0", "Autores", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
