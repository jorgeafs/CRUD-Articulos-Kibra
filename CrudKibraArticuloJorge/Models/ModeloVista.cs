using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Media.Imaging;
using Entity_Kibra;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CrudKibraArticulosJorge.Models
{
    public class ModeloVista
    {
        public ModeloVista()
        {
            articulo = new Articulo();
            proveedores = new List<Proveedores>(0);
        }
        public Articulo articulo { get; set; }
        public List<Proveedores> proveedores { get; set; }
    }
}