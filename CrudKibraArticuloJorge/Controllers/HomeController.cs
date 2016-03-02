using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL_Kibra;
using Entity_Kibra;
using CrudKibraArticulosJorge.Models;
using System.IO;

namespace CrudKibraArticulosJorge.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        

        public ActionResult Index()
        {
            List<ModeloVista> mostrar = new List<ModeloVista>(0);
            try {
                ManejadoraBL manejaBL = new ManejadoraBL();
                List<Articulo> manejaArticulo = manejaBL.listaArticulosBL();
                foreach (Articulo articulo in manejaArticulo)
                {
                    ModeloVista modelo = new ModeloVista();
                    modelo.articulo = articulo;
                    foreach (Proveedores proveedores in manejaBL.listaProveedoresBL())
                    {
                        if (articulo.idProveedor == proveedores.idProveedor)
                        {
                            modelo.articulo.proveedor = proveedores;
                            mostrar.Add(modelo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return RedirectToAction("errorPage");
            }

            return View(mostrar);
        }

        public ActionResult errorPage()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            ModeloVista articuloMostrar = null;
            try
            {
                articuloMostrar = new ModeloVista();
                ManejadoraBL manejaBL = new ManejadoraBL();
                articuloMostrar.articulo = manejaBL.seleccionaArticuloBL(id);
                articuloMostrar.articulo.proveedor = manejaBL.seleccionaProveedorBL(articuloMostrar.articulo.idProveedor);
                articuloMostrar.proveedores = manejaBL.listaProveedoresBL();
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return View("errorPage");
            }
            return View(articuloMostrar);
        }

        //public ActionResult Edit(int id, ModeloVista articulo)
        //{
        //    ModeloVista articuloMostrar = null;
        //    try
        //    {
        //        articuloMostrar = articulo;
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["EX"] = ex;
        //        return View("errorPage");
        //    }
        //    return View(articuloMostrar);
        //}

        [HttpPost]
        public ActionResult Edit(ModeloVista modelo)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/jpg",
                "image/pjpeg",
                "image/png"
            };

            //var upload = articulo.upload;

            //if (!validImageTypes.Contains(modelo.upload.GetType().ToString()))
            //{
            //    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            //}
            ManejadoraBL manejaBl = new ManejadoraBL();
            modelo.articulo.proveedor = manejaBl.seleccionaProveedorBL(modelo.articulo.idProveedor);
            String accion = null;
            if (modelo.upload != null)                                  //(upload != null) //si la imagen existe 
            {
                modelo.articulo.imagenArt = convierteImagenEnArrayDeBytes(modelo.upload);
            }
            if (ModelState.IsValid)
            {
                

                accion = "ConfirmacionSalvar";
            }
            else
            {
                accion = "Edit";
            }
            return RedirectToAction(accion, modelo);
        }

        public ActionResult ConfirmacionSalvar(ModeloVista articulo)
        {
            return View(articulo);
        }

        [HttpPost, ActionName("ConfirmacionSalvar")]
        public ActionResult ConfirmacionEditar(ModeloVista articulo)
        {
            try
            {
                ManejadoraBL manejaBL = new ManejadoraBL();
                Articulo salvar = new Articulo(articulo.articulo.nombreArt, articulo.articulo.imagenArt, articulo.articulo.descArt, articulo.articulo.precioArt, articulo.articulo.stock, articulo.articulo.stockMinimo, articulo.articulo.idProveedor);
                salvar.idArticulo = articulo.articulo.idArticulo;
                manejaBL.actualizarArticuloBL(salvar);
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return View("errorPage");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            ManejadoraBL manejaBL = null;
            ModeloVista articulo = null;
            try
            {
                manejaBL = new ManejadoraBL();
                articulo = new ModeloVista();
                articulo.articulo = manejaBL.seleccionaArticuloBL(id);
                articulo.articulo.proveedor = manejaBL.seleccionaProveedorBL(articulo.articulo.idProveedor);
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return RedirectToAction("errorPage");
            }
            return View(articulo);
        }

        public ActionResult Create()
        {
            ManejadoraBL manejaBL = null;
            ModeloVista articulos = null;
            try
            {
                articulos = new ModeloVista();
                manejaBL = new ManejadoraBL();
                articulos.proveedores = manejaBL.listaProveedoresBL();
                return View(articulos);
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return RedirectToAction("errorPage");
            }
        }

        [HttpPost]
        public ActionResult Create(ModeloVista articulo)
        {
            ManejadoraBL manejaBL = null;
            Articulo nuevo = null;
            try
            {
                manejaBL = new ManejadoraBL();
                articulo.articulo.imagenArt = convierteImagenEnArrayDeBytes(articulo.upload);
                nuevo = new Articulo(articulo.articulo.nombreArt, articulo.articulo.imagenArt,articulo.articulo.descArt, articulo.articulo.precioArt, articulo.articulo.stock, articulo.articulo.stockMinimo, articulo.articulo.idProveedor);
                manejaBL.insertArticuloBL(nuevo);
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return RedirectToAction("errorPage");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            ManejadoraBL manejaBL = null;
            ModeloVista articulo = null;
            try
            {
                manejaBL = new ManejadoraBL();
                articulo = new ModeloVista();
                articulo.articulo = manejaBL.seleccionaArticuloBL(id);
                articulo.articulo.proveedor = manejaBL.seleccionaProveedorBL(articulo.articulo.idProveedor);
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return RedirectToAction("errorPage");
            }
            return View(articulo);
        }

        [HttpPost]
        public ActionResult Delete(ModeloVista pasoPrevio)
        {
            ManejadoraBL manejaBL = null;
            try
            {
                manejaBL = new ManejadoraBL();
                manejaBL.borrarArticuloBL(pasoPrevio.articulo);
            }
            catch (Exception ex)
            {
                TempData["EX"] = ex;
                return RedirectToAction("errorPage");
            }
            return RedirectToAction("Index");
        }

        private byte[] convierteImagenEnArrayDeBytes(HttpPostedFileBase imageIn)
        {
            MemoryStream target = new MemoryStream();
            imageIn.InputStream.CopyTo(target);
            return target.ToArray();
        }
        //Codigo antiguo, lo dejo aqui para expediciones arqueologicas y para consultar
        //public ActionResult Edit(int id)
        //{
        //    Articulos articuloMostrar = null;
        //    try
        //    {                
        //        ManejadoraBL manejaBL = new ManejadoraBL();
        //        Articulo articulo = manejaBL.seleccionaArticuloBL(id);
        //        List<Proveedores> proveedores = manejaBL.listaProveedoresBL();
        //        foreach (Proveedores item in proveedores)
        //        {
        //            if (item.idProveedor == articulo.idProveedor)
        //            {
        //                articuloMostrar = new Articulos(articulo.idArticulo, articulo.nombreArt, articulo.descArt, articulo.precioArt, articulo.stock, articulo.stockMinimo, item.empresaPro);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["EX"] = ex;
        //        return View("errorPage");
        //    }
        //    return View(articuloMostrar);
        //}

        //[HttpPost]
        //public ActionResult Edit(Articulos articulos)
        //{
        //    String pagina;
        //    if (ModelState.IsValid)
        //    {
        //        pagina = "confirmar";
        //    }
        //    else
        //    {
        //        pagina = "Edit";
        //    }
        //    return RedirectToAction(pagina,articulos);
        //}

        //public ActionResult confirmar(Articulos articulos)
        //{
        //    return View(articulos);
        //}
    }
}
