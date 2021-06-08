using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Usuarios
    {
        public static string[,] matrizUsuarios = new string[4, 3]
        {
            {"1", "Administrador" , "admin"},
            {"2", "" , ""},
            {"3", "Vendedor1" , "vendedor1"},
            {"4", "Vendedor2" , "vendedor2"}
        };

        public static string[] opcionesMenu = 
        { 
            "Mostrar/Agregar Libros",
            "Modificar Libros",
            "Eliminar Libro",
            "Mostrar/Agregar Usuarios",
            "Modificar Usuarios",
            "Eliminar Usuarios",
            "Vender",
            "Facturación del día"
        };
    }
}
