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

        public static bool modificarLibro(string id, bool exit)
        {
            int indice = -1;
            string dato;

            while (exit)
            {
                //Imprimir usuario seleccionado
                Console.Clear();
                Visualizacion.titulo();
                Console.WriteLine("Datos de libro a modificar: \n");
                for (int i = 0; i < libros.GetLength(0); i++)
                {
                    if (libros[i, 0].ToString() == id)
                    {
                        indice = i;
                        break;
                    }
                }
                for (int i = 0; i < libros.GetLength(1); i++)
                {
                    Console.WriteLine($"{libros[0, i]}: {libros[indice, i]}");
                }

                //Seleccionar dato a modificar
                Console.WriteLine("");
                Console.WriteLine("");
                int cantidadImpresa = Procedimientos.menuOpcionesMatriz(libros, 0);
                Console.WriteLine("");
                int opcion = Validaciones.validarOpcionMatiz("Seleccione dato a modificar: ", cantidadImpresa, libros);

                //Modificar dato
                Console.Write($"Escriba nuevo {libros[0, opcion]}: ");
                dato = Console.ReadLine();
                libros[indice, opcion] = dato;
                exit = Validaciones.validarSalir("\n\nPresione ENTER para cambiar otro dato, ESC para continuar", exit);

            }

            return exit;
        }

        public static bool eliminarLibro(string id, bool exit)
        {
            int indice = -1;
            bool borrar = true;

            //Imprimir usuario seleccionado
            Console.Clear();
            Visualizacion.titulo();
            Console.WriteLine("Datos del libro a eliminar: \n");
            for (int i = 0; i < libros.GetLength(0); i++)
            {
                if (libros[i, 0].ToString() == id)
                {
                    indice = i;
                    break;
                }
            }
            for (int i = 0; i < libros.GetLength(1); i++)
            {
                Console.WriteLine($"{libros[0, i]}: {libros[indice, i]}");
            }

            borrar = Validaciones.validarSalir("\nSi es correcto, presione ENTER. De lo contrario, presione ESC", borrar);

            //Eliminar usuario                             

            if (borrar)
            {
                Console.Clear();
                Visualizacion.titulo();
                for (int i = 0; i < libros.GetLength(1); i++)
                {
                    libros[indice, i] = "";

                }
                Console.WriteLine("Usuario eliminado");
                exit = Validaciones.validarSalir("\nPresione ESC para volver al menú anterior", exit);
            }

            return exit;
        }
    }
}
