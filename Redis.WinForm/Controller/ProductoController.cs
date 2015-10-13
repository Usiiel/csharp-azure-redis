///<remarks>Autor: Manuel Mejias S.</remarks>
using Redis.WinForm.Model;
using ServiceStack.Redis;//redis
using ServiceStack.Redis.Generic;//redis
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.WinForm.Controller
{
    class ProductoController
    {
        private string host = "Host";
        private int port = 6379;
        private string password = "Password";


        public IList<Producto> getProductos()
        {
            IList<Producto> productos = null;//Creamos una lista que reciba nuestros productos
            using (IRedisClient redisClient = new RedisClient(host, port, password, 1))//creamos nuestro objeto de conexion.
            {
                //creamos un objeto IRedisTypedClient y le especificamos que trabajara con nuestra clase Producto como tipo. 
                //asi se limita solo a ese tipo de estructuras dentro de Redis.
                IRedisTypedClient<Producto> redisTypeClient = redisClient.As<Producto>();
                productos = redisTypeClient.GetAll();//obtenemos todos los registros contenidos en Redis del tipo Producto.
            }
            return productos;
        }


        public Producto getProducto(string Id)
        {
            Producto producto = null;//Creamos una lista que reciba nuestros productos
            using (IRedisClient redisClient = new RedisClient(host, port, password, 1))//creamos nuestro objeto de conexion.
            {
                //creamos un objeto IRedisTypedClient y le especificamos que trabajara con nuestra clase Producto como tipo. 
                //asi se limita solo a ese tipo de estructuras dentro de Redis.
                IRedisTypedClient<Producto> redisTypeClient = redisClient.As<Producto>();
                producto = redisTypeClient.GetById(Id);//obtenemos el producto especifico.
            }
            return producto;
        }

        public void setProducto(Producto producto)
        {
            using (IRedisClient redisClient = new RedisClient(host, port, password, 1))//creamos nuestro objeto de conexion.
            {
                //creamos un objeto IRedisTypedClient y le especificamos que trabajara con nuestra clase Producto como tipo. 
                IRedisTypedClient<Producto> redisTypeClient = redisClient.As<Producto>();
                redisTypeClient.Store(producto);//almacenamos nuestro producto en Reds.
            }
        }

        public void setProductos(IEnumerable<Producto> productos)
        {
            using (IRedisClient redisClient = new RedisClient(host, port, password, 1))//creamos nuestro objeto de conexion.
            {
                //creamos un objeto IRedisTypedClient y le especificamos que trabajara con nuestra clase Producto como tipo. 
                IRedisTypedClient<Producto> redisTypeClient = redisClient.As<Producto>();
                redisTypeClient.StoreAll(productos);//almacenamos todos nuestros productos.
            }
        }

        public void delProductos()
        {
            using (IRedisClient redisClient = new RedisClient(host, port, password, 1))//creamos nuestro objeto de conexion.
            {
                //creamos un objeto IRedisTypedClient y le especificamos que trabajara con nuestra clase Producto como tipo. 
                IRedisTypedClient<Producto> redisTypeClient = redisClient.As<Producto>();
                redisTypeClient.DeleteAll();//Eliminamos todos los productos.
            }
        }
    }
}
