using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

public class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // Initialization code here
    }

    private void CargarDatos() { }
    private void btnEliminar_Click(object sender, EventArgs e) { }
    private void btnActualizar_Click(object sender, EventArgs e) { }
    private IContainer components;
    private DataGridView dataGridViewCustomers;
    private Button btnCargarDatos;
    private Button btnEliminar;
    private Label lblCustomerId;
    private Button btnActualizar;
    private TextBox txtCustomerId;
    protected override void Dispose(bool disposing) { }
    private void btnCargarDatos_Click(object sender, EventArgs e) { }
}
