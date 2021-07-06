using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposteria.BL
{
    public class Producto
    {
        public Producto()
        {
            Activo = true;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese la descripción")]
        [MinLength (3, ErrorMessage = "Ingrese minimo 3 caracteres")]
        [MaxLength (20, ErrorMessage = "Ingrese un máximo de 20 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Ingrese el precio")]
        [Range (0,1000, ErrorMessage = "Ingrese un precio entre 0 y 1000")]
        public double Precio { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        [Display (Name = "Imagen")]
        public string UrlImage { get; set; }

        public bool Activo { get; set; }
    }
}
