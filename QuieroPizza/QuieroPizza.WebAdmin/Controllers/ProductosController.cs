﻿using QuieroPizza.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuieroPizza.WebAdmin.Controllers
{
    [Authorize] //autoriza todo productocontroller
    public class ProductosController : Controller
    {
        ProductosBL _productosBL; //variable global 
        CategoriasBL _categoriasBL;

        public ProductosController()
        {
            _productosBL = new ProductosBL();
            _categoriasBL = new CategoriasBL();
        }
        // GET: Productos // envia pagina al cliente
        public ActionResult Index()
        {
            var listadeProductos = _productosBL.ObtenerProductos();

            return View(listadeProductos);
        }

        public ActionResult Crear() //GET para crear un producto
        {
            var nuevoProducto = new Producto();
            var categorias = _categoriasBL.ObtenerCategorias();


            ViewBag.CategoriaId =
                new SelectList(categorias, "Id", "Descripcion");

            return View(nuevoProducto);
        }

        [HttpPost] //atributo que envia de regreso
        public ActionResult Crear(Producto producto, HttpPostedFileBase imagen) //recibe un producto de regreso
        {
            if (ModelState.IsValid)
            {
                if (producto.CategoriaId == 0)
                {
                    ModelState.AddModelError("CategoriaId", "Seleccione una categoria");
                    return View(producto);
                }

                if(imagen != null)
                {
                    producto.UrlImagen = GuardarImagen(imagen);
                }

                _productosBL.GuardarProducto(producto); // Guarda el producto creado por el usuario

                return RedirectToAction("Index"); //Nos redireciona a l a vista index
            }

            var categorias = _categoriasBL.ObtenerCategorias();

            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Descripcion");

            return View(producto);
        }

        public ActionResult Editar(int id)
        {
            var producto = _productosBL.ObtenerProducto(id);
            var categorias = _categoriasBL.ObtenerCategorias();

            ViewBag.CategoriaId =
                new SelectList(categorias, "Id", "Descripcion", producto.CategoriaId);

            return View(producto);
        }

        [HttpPost]
        public ActionResult Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (producto.CategoriaId == 0)
                {
                    ModelState.AddModelError("CategoriaId", "Seleccione una categoria");
                    return View(producto);
                }

                _productosBL.GuardarProducto(producto); // Guarda el producto creado por el usuario

                return RedirectToAction("Index"); //Nos redireciona a l a vista index
            }

            var categorias = _categoriasBL.ObtenerCategorias();

            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Descripcion");

            return View(producto);
        }

        public ActionResult detalle(int id) //mantenimiento a detalle
        {
            var producto = _productosBL.ObtenerProducto(id);
            return View(producto);
        }

        public ActionResult Eliminar(int id)
        {
            var producto = _productosBL.ObtenerProducto(id);
            return View(producto);
        }

        [HttpPost]
        public ActionResult Eliminar (Producto producto)
        {
            _productosBL.EliminarProducto(producto.Id);
            return RedirectToAction("Index");
        }

        private string GuardarImagen(HttpPostedFileBase imagen)
        {
            string path = Server.MapPath("~/Imagenes/" + imagen.FileName);
            imagen.SaveAs(path);

            return "/Imagenes/" + imagen.FileName;
        }
    }
}