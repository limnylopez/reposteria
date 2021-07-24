using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroPizza.BL
{
    public class CategoriasBL
    {
        Contexto _contexto;
        public List<Categoria> ListadeCategorias { get; set; }

        public CategoriasBL()
        {
            _contexto = new Contexto();
            ListadeCategorias = new List<Categoria>();
        }

        public List<Categoria> ObtenerCategorias()
        {
            ListadeCategorias = _contexto.Categorias.ToList();
            return ListadeCategorias;
        }

        public void GuardarCategoria(Categoria categoria) //recibe y guarda el producto
        {
            if (categoria.Id == 0)
            {
                _contexto.Categorias.Add(categoria); //guarda el producto que recibe de la vista
            }
            else
            {
                var categoriaExistente = _contexto.Categorias.Find(categoria.Id); //busca en bd el producto existente
                categoriaExistente.Descripcion = categoria.Descripcion;
                
            }

            _contexto.SaveChanges(); //guarda los cambios realizados
        }

        public Categoria ObtenerCategoria(int id)
        {
            var categoria = _contexto.Categorias.Find(id); //busca en bd el id
            return categoria;
        }

        public void EliminarCategoria(int id)
        {
            var categoria = _contexto.Categorias.Find(id);
            _contexto.Categorias.Remove(categoria);
            _contexto.SaveChanges();
        }
    }
}
