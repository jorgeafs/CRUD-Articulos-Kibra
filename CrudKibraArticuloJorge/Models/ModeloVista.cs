using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using Entity_Kibra;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Drawing;

namespace CrudKibraArticulosJorge.Models
{
    public class ModeloVista
    {
        public ModeloVista()
        {
            articulo = new Articulo();
            proveedores = new List<Proveedores>(0);
            upload = null;
        }
        public Articulo articulo { get; set; }
        public List<Proveedores> proveedores { get; set; }
        [AllowHtml]
        public Image upload { get; set; }
    }
}