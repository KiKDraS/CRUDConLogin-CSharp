using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Stock
    {
        //Matrices y arrays iniciales

        public static string[,] libros = new string[11, 4]
        {
                {"ID",  "Titulo",   "Autor",    "Género" },         
                {"1",   "Elantris",   "Brandon Sanderson",    "Fantasía épica" },
                {"",   "",   "",    "" },
                {"3",   "Juego de Tronos",   "George R.R. Martin",    "Fantasía épica" },
                {"",   "",   "",    "" },
                {"5",   "El factor Scarpetta",   "Patricia Cornwell",    "Policial" },
                {"",   "",   "",    "" },
                {"7",   "La perfección del silencio",   "Clara Asunción García",    "Romance" },
                {"",   "",   "",    "" },
                {"9",   "La filosofía de House",   "W. Irvin y H. Jacoby",    "Filosofía" },
                {"",  "",   "",    "" }
        };

        public static float[,] precioCantidad = new float[10, 3]
        {
                //ID //Cantidad  //Precio
                {1,     0,          0},
                {0,     0,          0},
                {3,     0,          0},
                {0,     0,          0},
                {5,     0,          0},
                {0,     0,          0},
                {7,     0,          0},
                {0,     0,          0},
                {9,     0,          0},
                {0,     0,          0}
        };

        //CRUD

        /// <summary>
        ///     Método para agregar dato a la matriz libros. Revisa si hay espacios disponible y, en caso de no haberlo, agranda el tamaño de la matriz
        /// </summary>
        /// <param name="exit">Booleano para manejar la nevegación por los distintos menues</param>
        /// <returns>Booleano para manejar navegación por los distintos menues</returns>
        public static bool agregarLibro(bool continuar)
        {

            //Cargar nuevo libro
            string[,] elemAgregar = Procedimientos.armarMatrizDatosNuevos(libros);

            //Agregar libro a la matriz
            libros = Procedimientos.agregarElementoMatrizStr(libros, elemAgregar);

            continuar = Validaciones.validarSalir("\n\n Presione ENTER para agregar otro libro o ESC para volver al menú anterior", continuar);

            return continuar;
        }
    }
}
