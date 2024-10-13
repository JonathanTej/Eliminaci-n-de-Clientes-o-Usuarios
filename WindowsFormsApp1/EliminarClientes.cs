using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Cargar datos de clientes en el DataGridView
        private void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new NorthwindEntities1())  // Contexto de Entity Framework
                {
                    var customers = context.Customers.ToList();  // Obtener todos los clientes
                    if (customers.Any())
                    {
                        dataGridViewCustomers.DataSource = customers;  // Mostrar en el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron clientes.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos: {ex.Message}");
            }
        }

        // Evento para eliminar cliente por ID ingresado
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtiene el ID del cliente del TextBox
            string customerId = txtCustomerId.Text.Trim();

            // Verifica que el campo no esté vacío
            if (string.IsNullOrEmpty(customerId))
            {
                MessageBox.Show("Por favor, ingresa el ID del cliente que deseas eliminar.");
                return;
            }

            try
            {
                using (var context = new NorthwindEntities1())
                {
                    // Busca el cliente en la base de datos
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerId);

                    if (customer != null)
                    {
                        // Elimina el cliente
                        context.Customers.Remove(customer);
                        context.SaveChanges(); // Guarda los cambios en la base de datos
                        MessageBox.Show("Cliente eliminado correctamente.");

                        // Recarga los datos en el DataGridView
                        CargarDatos();

                        // Limpia el TextBox
                        txtCustomerId.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cliente: {ex.Message}");
            }
        }

    }
}

