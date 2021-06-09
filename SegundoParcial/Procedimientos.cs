using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Procedimientos
    {
        /// <summary>
        ///     Impresión del título de la aplicación
        /// </summary>
        public static void titulo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(".~~~~~~~~~~~~~~~~~.");
            Console.WriteLine("| Librería OnLine |");
            Console.WriteLine("'~~~~~~~~~~~~~~~~~'\n");

            Console.ResetColor();
        }


        //MENUS DE OPCIONES

        /// <summary>
        ///     Impresión del menú para la selección del usuario
        /// </summary>
        /// <returns>Usuario validado</returns>
        public static string login()
        {
            string usuario = "";

            titulo();
            Console.WriteLine("Login\n");
            int cantidadImpresa = imprimirMatriz(Usuarios.matrizUsuarios, 1, null);
            int opcion = Validaciones.validarOpcionMatriz<string>("\nSeleccione opción: ", cantidadImpresa, Usuarios.matrizUsuarios, 1, usuario);

            if (opcion == cantidadImpresa)
            {
                return usuario;
            }
            else
            {
                //Validación de contraseña de usuario
                Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[opcion - 1, 1]}");
                usuario = Validaciones.validarPass("Ingrese contraseña: ", Usuarios.matrizUsuarios, opcion);
            }

            return usuario;

        }

        /// <summary>
        ///     Imprime el menú para los distintos usuarios
        ///     <param name="usuario">Nombre del usuario logueado actualmente</param>
        /// </summary>
        /// <returns>Estado de booleano exit para salir del programa</returns>
        public static bool menuAdmin(string usuario, bool exit)
        {
            titulo();
            Console.WriteLine($"\nMenu {usuario}\n");
            int cantidadImpresa = imprimirArray(Usuarios.opcionesMenu, usuario);
            int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, Usuarios.opcionesMenu, usuario);

            if (opcion == cantidadImpresa)
            {
                exit = false;
                return exit;
            }
            else
            {
                switch (usuario)
                {
                    case "Administrador":
                        switch (opcion)
                        {
                            case 1:
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }

            return exit;
        }



        //PROCEDIMIENTOS DE MATRICES

        /// <summary>
        ///     Impresión de Matriz.
        /// </summary>
        /// <param name="auxMatriz">Matriz de string de la que se quiere imprimir la columna</param>
        /// <param name="numColumna">Columna de la Matriz a imprimir</param>
        /// <param name="usuario">Nombre del usuario logueado actualmente</param>
        /// <returns>Cantidad de elementos impresos</returns>
        public static int imprimirMatriz<T>(T[,] auxMatriz, int numColumna, string usuario)
        {
            int cantidadImpresa = 0;

            for (int i = 0; i < auxMatriz.GetLength(0); i++)
            {
                if (!string.IsNullOrEmpty(auxMatriz[i, numColumna].ToString()))
                {
                    cantidadImpresa++;
                    Console.WriteLine($"{cantidadImpresa}. {auxMatriz[i, numColumna]}");
                }
            }
            cantidadImpresa++;
            Console.WriteLine($"{cantidadImpresa}. Salir");

            return cantidadImpresa;
        }



        //PROCEDIMIENTOS DE ARRAYS

        /// <summary>
        ///     Impresión de Array
        /// </summary>
        /// <param name="auxArray">Array a imprimir</param>
        /// <param name="usuario">Nombre del usuario logueado actualmente</param>
        /// <returns>Cantidad de opciones impresas</returns>
        public static int imprimirArray<T>(T[] auxArray, string usuario)
        {
            int cantidadImpresa = 0;

            if (usuario == "Administrador")
            {   
                //getType()
                for (int i = 0; i < auxArray.GetLength(0); i++)
                {
                    if (!string.IsNullOrEmpty(auxArray[i].ToString()))
                    {
                        //comparar como 0
                        cantidadImpresa++;
                        Console.WriteLine($"{cantidadImpresa}. {auxArray[i]}");
                    }
                }
                cantidadImpresa++;
                Console.WriteLine($"{cantidadImpresa}. Salir");
            }
            else
            {
                cantidadImpresa++;
                Console.WriteLine($"{cantidadImpresa}. Vender");
                cantidadImpresa++;
                Console.WriteLine($"{cantidadImpresa}. Salir");
            }         

            return cantidadImpresa;
        }


        //CRUD - Matriz

        public static string[,] agregarElementoMatriz(string[,] auxMatriz, string usuario, string password)
        {
            bool continuar = true;
            int indice = -1;

            do
            {
                //Buscar elementos espacios vacíos en la Matriz

                for (int i = 0; i < auxMatriz.GetLength(0); i++)
                {
                    if (string.IsNullOrEmpty(auxMatriz[i,1]))
                    {
                        indice = i;
                        break;
                    }
                }

                if (indice == -1)
                {
                    indice = auxMatriz.GetLength(0) + 1;
                    ResizeArray<string>(ref auxMatriz, indice, 3);
                }
                else
                {
                    auxMatriz[indice, 1] = usuario;
                    auxMatriz[indice, 2] = password;
                }

            } while (continuar);

            return auxMatriz;
        }



        //HELPERS


        static void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
        {
            var newArray = new T[newCoNum, newRoNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRoNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }
    }
}
