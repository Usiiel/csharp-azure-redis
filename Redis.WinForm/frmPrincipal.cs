///<remarks>Autor: Manuel Mejias S.</remarks>
using Redis.WinForm.Model;
using Redis.WinForm.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redis.WinForm
{
    public partial class frmPrincipal : Form
    {
        //Declaraciones.
        ProductoController prodController;
        IList<Producto> _productos;
        public frmPrincipal()
        {
            InitializeComponent();
            prodController = new ProductoController();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            obtenerProductos();//cargamos todos los productos.
        }

        private void lbxProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxProductos.SelectedItem != null)
                cargaProducto(lbxProductos.SelectedItem.ToString());
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            crearProducto();//agregamos el producto.
            obtenerProductos(); //cargamos todos los productos.
        }

        private void btnPrecioAdd_Click(object sender, EventArgs e)
        {
            dgvPrecios.Rows.Add(new string[] { txtPrecioNombre.Text, numPrecio.Value.ToString() });
        }

        private void btnPrecioDel_Click(object sender, EventArgs e)
        {
            if (dgvPrecios.SelectedRows.Count > 0)
                dgvPrecios.Rows.Remove(dgvPrecios.SelectedRows[0]);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lbxProductos.Items.Clear();
            if (txtBuscar.Text.Length > 0)
                obtenerProductos(txtBuscar.Text);
            else
                obtenerProductos();
        }

        /// <summary>
        /// Carga el/los productos obtenidos desde Redis en nuestra GUI.
        /// </summary>
        /// <param name="codigo"></param>
        private void obtenerProductos(string codigo = null)
        {
            if (codigo == null)
                _productos = prodController.getProductos();
            else
                _productos = new List<Producto> { prodController.getProducto(codigo) };

            lbxProductos.Items.Clear();
            foreach (Model.Producto product in _productos)
            {
                lbxProductos.Items.Add(string.Format("{0}", product.Id));
            }
        }

        /// <summary>
        /// obtiene el producto en base a su codigo dentro de la lista de productos almacenada localmente.
        /// </summary>
        /// <param name="codigo"></param>
        private void cargaProducto(string codigo)
        {
            Model.Producto producto = (from prod in _productos where prod.Id == codigo select prod).ToList<Model.Producto>()[0];

            if (producto != null)
            {
                txtCodigo.Text = producto.Id;
                txtNombre.Text = producto.Nombre;
                txtDescripcion.Text = producto.Descripcion;
                dgvPrecios.Rows.Clear();

                if (producto.Precios != null)
                    foreach (Precio precio in producto.Precios)
                    {
                        dgvPrecios.Rows.Add(new string[] { precio.Nombre, precio.precio.ToString() });
                    }
            }
        }
        /// <summary>
        /// construye un Producto con los objetos de la GUI
        /// </summary>
        private void crearProducto()
        {
            Model.Producto producto = new Model.Producto();
            producto.Id = txtCodigo.Text;
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Precios = new List<Model.Precio>();
            foreach (DataGridViewRow dr in dgvPrecios.Rows)
            {
                producto.Precios.Add(new Precio { Nombre = dr.Cells["nombre"].Value.ToString(), precio = Convert.ToInt32(dr.Cells["precio"].Value) });
            }
            prodController.setProducto(producto);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<Model.Producto> productos = new List<Producto>(); ;
            Model.Producto producto;
            int nuevosProductos = 2000;//Crea 2000 productos y toma el tiempo que tarda.


            for (int i = 1; i < nuevosProductos; i++)
            {
                producto = new Model.Producto();
                producto.Id = i.ToString().PadLeft(6, '0');
                producto.Nombre = "Producto N° " + i.ToString().PadLeft(6, '0');
                producto.Descripcion = "Producto Descripcion " + i.ToString().PadLeft(6, '0');
                producto.Precios = new List<Model.Precio>();
                producto.Precios.Add(new Precio { Nombre = "precio", precio = 1000 });
                productos.Add(producto);
            }
            DateTime dtIni = DateTime.Now;
            prodController.setProductos(productos);
            DateTime dtFin = DateTime.Now;
            MessageBox.Show("Se crearon " + nuevosProductos + " productos en " + (dtFin - dtIni).TotalSeconds + " segundos.");
            obtenerProductos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prodController.delProductos();
            obtenerProductos();
        }
    }
}

