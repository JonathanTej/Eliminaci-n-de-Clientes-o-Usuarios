using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public  Form1()
        {
            InitializeComponent();
        }

        // Método para cargar datos en el DataGridView
        private void CargarDatos()
        {
            try
            {
                using (var context = new NorthwindEntities1()) // Contexto generado por EF
                {
                    var customers = context.Customers.Select(c => new
                    {
                        c.CustomerID,
                        c.CompanyName,
                        c.ContactName,
                        c.ContactTitle,
                        c.Address,
                        c.City,
                        c.Region,
                        c.PostalCode,
                        c.Country,
                        c.Phone,
                        c.Fax
                    }).ToList(); // Seleccionamos las columnas que queremos mostrar

                    if (customers.Count > 0)
                    {
                        dataGridViewCustomers.DataSource = customers;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron clientes.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }

        // Eliminar el método duplicado btnEliminar_Click
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Lógica para eliminar cliente

            string customerId = null;

            // Si hay una fila seleccionada en el DataGridView
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                customerId = dataGridViewCustomers.SelectedRows[0].Cells["CustomerID"].Value.ToString();
            }
            // Si no hay fila seleccionada, tomamos el ID ingresado en el TextBox
            else if (!string.IsNullOrEmpty(txtCustomerId.Text))
            {
                customerId = txtCustomerId.Text.Trim();
            }

            if (customerId == null)
            {
                MessageBox.Show("Por favor, selecciona un cliente o ingresa el ID del cliente que deseas eliminar.");
                return;
            }

            try
            {
                using (var context = new NorthwindEntities1())
                {
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerId);
                    if (customer != null)
                    {
                        context.Customers.Remove(customer);
                        context.SaveChanges();
                        MessageBox.Show("Cliente eliminado correctamente.");
                        CargarDatos(); // Recargar datos
                        txtCustomerId.Clear(); // Limpiar el TextBox
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cliente: {ex.Message}\nDetalles: {ex.InnerException?.Message}");
            }
        }

        // Evento para actualizar los datos
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos(); // Recarga los datos
        }

        // Inicialización de los componentes
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.btnCargarDatos = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.lblCustomerId = new System.Windows.Forms.Label();

            // Configuración de DataGridView
            this.dataGridViewCustomers.AllowUserToOrderColumns = true;
            this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(184, 73);
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.Size = new System.Drawing.Size(540, 150);

            // Botón Cargar Datos
            this.btnCargarDatos.Location = new System.Drawing.Point(64, 40);
            this.btnCargarDatos.Name = "btnCargarDatos";
            this.btnCargarDatos.Size = new System.Drawing.Size(100, 30);
            this.btnCargarDatos.Text = "Cargar Datos";
            this.btnCargarDatos.UseVisualStyleBackColor = true;
            this.btnCargarDatos.Click += new System.EventHandler(this.btnCargarDatos_Click);

            // TextBox para ingresar el ID del cliente
            this.txtCustomerId.Location = new System.Drawing.Point(64, 160);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Size = new System.Drawing.Size(100, 30);

            // Label para el TextBox
            this.lblCustomerId.Text = "ID del Cliente:";
            this.lblCustomerId.Location = new System.Drawing.Point(64, 140);

            // Botón Eliminar
            this.btnEliminar.Location = new System.Drawing.Point(64, 80);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 30);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // Botón Actualizar
            this.btnActualizar.Location = new System.Drawing.Point(64, 120);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(100, 30);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            // Configuración del Formulario
            this.ClientSize = new System.Drawing.Size(754, 261);
            this.Controls.Add(this.btnCargarDatos);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.dataGridViewCustomers);
            this.Controls.Add(this.txtCustomerId);
            this.Controls.Add(this.lblCustomerId);
            this.Name = "Form1";
            this.Text = "Gestión de Clientes";
        }

        private Container components;
        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.Button btnCargarDatos;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Label lblCustomerId;


    }
}
