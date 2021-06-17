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
                {"1",   "Elantris",   "Brandon Sanderson",    "Fantasía épica", "10", "150" },
                {"",   "",   "",    "", "", "" },
                {"3",   "Juego de Tronos",   "George R.R. Martin",    "Fantasía épica", "15", "200" },
                {"",   "",   "",    "", "", "" },
                {"5",   "El fact Scarpetta",   "Patricia Cornwell",    "Policial", "20", "130" },
                {"",   "",   "",    "", "", "" },
                {"7",   "La perf silencio",   "Clara García",    "Romance", "30", "160" },
                {"",   "",   "",    "", "", "" },
                {"9",   "La filo de House",   "Irvin y Jacoby",    "Filosofía", "15", "100" },
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
        public static bool agregarLibro(bool continuar)
        {

            //Cargar nuevo libro
            string[,] elemAgregar = Procedimientos.armarMatrizDatosNuevos(libros);

            //Agregar libro a la matriz
            libros = Procedimientos.agregarElementoMatrizStr(libros, elemAgregar);

            //Agregar cantidad-precio
            precioCantidad = Procedimientos.agregarElementoMatrizFloat(precioCantidad, elemAgregar);

            continuar = Validaciones.validarSalir("\n\n Presione ENTER para agregar otro libro o ESC para volver al menú anterior", continuar);

            return continuar;
        }

        public static bool modificarLibro(string id, bool exit)
        {
            int indice = -1;
            string dato;

            while (exit)
            {
                //Imprimir libro seleccionado
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
                if(libros[0, opcion].ToString() == "Cantidad" || libros[0, opcion].ToString() == "Precio")
                {
                    Console.Write($"Escriba nuevo {libros[0, opcion]}: ");
                    dato = Console.ReadLine();
                    float datoFloat = Validaciones.validarFloat(dato);
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
                for (int i = 0; i < precioCantidad.GetLength(1); i++)
                {
                    precioCantidad[indice, i] = 0;
                }
                Console.WriteLine("Libro eliminado");
                exit = Validaciones.validarSalir("\nPresione ESC para volver al menú anterior", exit);
            }

            return exit;
        }
    }
}
