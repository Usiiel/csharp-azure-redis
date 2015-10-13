///<remarks>Autor: Manuel Mejias S.</remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.WinForm.Model
{
    [Serializable]
    public class Producto
    {
        public string Id { get; set; }//sera nuestro codigo
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IList<Precio> Precios { get; set; }
    }

    [Serializable]
    public class Precio
    {
        public string Nombre { get; set; }
        public int precio { get; set; }
    }
}
