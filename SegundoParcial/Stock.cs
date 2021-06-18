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

        public static string[,] libros = new string[11, 6]
        {
                {"ID",  "Titulo",   "Autor",    "Género", "Cantidad", "Precio" },
                {"1",   "Elantris",   "Bran Sanderson",    "Fantasía épica", "10", "150" },
                {"",   "",   "",    "", "", "" },
                {"3",   "Juego Tronos",   "George Martin",    "Fantasía épica", "15", "200" },
                {"",   "",   "",    "", "", "" },
                {"5",   "El fact Scarp",   "Patri Cornwell",    "Policial", "20", "130" },
                {"",   "",   "",    "", "", "" },
                {"7",   "La perf silen",   "Clara García",    "Romance", "30", "160" },
                {"",   "",   "",    "", "", "" },
                {"9",   "La filo House",   "Irvin y Jacoby",    "Filosofía", "15", "100" },
                {"",  "",   "",    "", "", "" }
        };

        public static float[,] precioCantidad = new float[11, 3]
        {
                //ID //Cantidad  //Precio
                {-2,     -2,          -2},
                {1,     10,          150},
                {0,     0,          0},
                {3,     15,          200},
                {0,     0,          0},
                {5,     20,          130},
                {0,     0,          0},
                {7,     30,          160},
                {0,     0,          0},
                {9,     15,          100},
                {0,     0,          0}
        };

        //CRUD

        /// <summary>
        ///     Método para agregar dato a la matriz libros. Revisa si hay espacios disponible y, en caso de no haberlo, agranda el tamaño de la matriz
        /// </summary>
        /// <param name="exit">Booleano para manejar la nevegación por los distintos menues</param>
        /// <returns>Booleano para manejar navegación por los distintos menues</returns>
        public static bool AgregarLibro(bool continuar)
        {

            //Cargar nuevo libro
            string[,] elemAgregar = Procedimientos.ArmarMatrizDatosNuevos(libros);

            //Agregar libro a la matriz
            libros = Procedimientos.AgregarElementoMatrizStr(libros, elemAgregar);

            //Agregar cantidad-precio
            precioCantidad = Procedimientos.AgregarElementoMatrizFloat(precioCantidad, elemAgregar);

            continuar = Validaciones.ValidarSalir("\n\n Presione ENTER para agregar otro libro o ESC para volver al menú anterior", continuar);

            return continuar;
        }

        /// <summary>
        ///     Método para modificar dato en libros
        /// </summary>
        /// <param name="id">ID del usuario a modificar</param>
        /// <param name="exit">Boolano que maneja la navegación</param>
        /// <returns>Booleano que maneja la navegación</returns>
        public static bool ModificarLibro(string id, bool exit)
        {
            int indice = -1;
            string dato;

            while (exit)
            {
                //Imprimir libro seleccionado
                Console.Clear();
                Visualizacion.titulo();
                Console.WriteLine("Datos del libro a modificar: \n");
                indice = Procedimientos.EncontrarIndice(libros, id);
                for (int i = 0; i < libros.GetLength(1); i++)
                {
                    Console.WriteLine($"{libros[0, i]}: {libros[indice, i]}");
                }
                //Seleccionar dato a modificar
                Console.WriteLine("");
                Console.WriteLine("");
                int cantidadImpresa = Procedimientos.MenuOpcionesMatriz(libros, 0);
                Console.WriteLine("");
                int opcion = Validaciones.ValidarOpcionMatiz("Seleccione dato a modificar: ", cantidadImpresa, libros);

                //Modificar dato
                if(libros[0, opcion].ToString() == "Cantidad" || libros[0, opcion].ToString() == "Precio")
                {
                    Console.Write($"Escriba nuevo {libros[0, opcion]}: ");
                    dato = Console.ReadLine();
                    float datoFloat = Validaciones.ValidarFloat(dato);
                    libros[indice, opcion] = datoFloat.ToString();
                    if (libros[0, opcion].ToString() == "Cantidad")
                    {
                        precioCantidad[indice, 1] = datoFloat;
                    }
                    else
                    {
                        precioCantidad[indice, 2] = datoFloat;
                    }

                }
                else
                {
                    Console.Write($"Escriba nuevo {libros[0, opcion]}: ");
                    dato = Console.ReadLine();
                    libros[indice, opcion] = dato;
                }
                
                exit = Validaciones.ValidarSalir("\n\nPresione ENTER para cambiar otro dato, ESC para continuar", exit);

            }

            return exit;
        }

        /// <summary>
        ///     Método para eliminar dato en libros
        /// </summary>
        /// <param name="id">ID del usuario a eliminar</param>
        /// <param name="exit">Boolano que maneja la navegación</param>
        /// <returns>Boolano que maneja la navegación</returns>
        public static bool EliminarLibro(string id, bool exit)
        {
            int indice = -1;
            bool borrar = true;

            //Imprimir usuario seleccionado
            Console.Clear();
            Visualizacion.titulo();
            Console.WriteLine("Datos del libro a eliminar: \n");
            indice = Procedimientos.EncontrarIndice(libros, id);
            for (int i = 0; i < libros.GetLength(1); i++)
            {
                Console.WriteLine($"{libros[0, i]}: {libros[indice, i]}");
            }

            borrar = Validaciones.ValidarSalir("\nSi es correcto, presione ENTER. De lo contrario, presione ESC", borrar);

            //Eliminar usuario                             

            if (borrar)
            {
                Console.Clear();
                Visualizacion.titulo();
                for (int i = 0; i < libros.GetLength(1); i++)
                {
                    libros[indice, i] = "";

                }
                for (int i = 0; i < precioCantidad.GetLength(1); i++)
                {
                    precioCantidad[indice, i] = 0;
                }
                Console.WriteLine("Libro eliminado");
                exit = Validaciones.ValidarSalir("\nPresione ESC para volver al menú anterior", exit);
            }

            return exit;
        }

        public static float ComprobarDisponibilidadStock(int indice, float cantidadSeleccionada)
        {
            while (cantidadSeleccionada > Stock.precioCantidad[indice, 1])
            {
                Console.Write("Stock insuficiente. Seleccione otra cantidad: ");
                string dato = Console.ReadLine();
                cantidadSeleccionada = Validaciones.ValidarFloat(dato);
            }

            return cantidadSeleccionada;
        }
    }
}
