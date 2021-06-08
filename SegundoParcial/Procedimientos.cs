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
        public static string menuUsuarios()
        {
            int opcion = -1;
            int cantidadImpresa = 0;
            string usuario = "";

            titulo();
            Console.WriteLine("Login\n");
            cantidadImpresa = imprimirMatrizStr(Usuarios.matrizUsuarios, 1, null);
            opcion = Validaciones.validarOpcionMatrizSrt("\nSeleccione opción: ", cantidadImpresa, Usuarios.matrizUsuarios, 1, usuario);

            if(opcion == cantidadImpresa)
            {
                return usuario;
            }
            else
            {
                //Validación de contraseña de usuario
                Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[opcion - 1, 1]}");
                usuario = Validaciones.validarPassMatriz("Ingrese contraseña: ", Usuarios.matrizUsuarios, opcion);
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
            int opcion = -1;
            int cantidadImpresa = 0;

            titulo();
            Console.WriteLine($"\nMenu {usuario}\n");
            cantidadImpresa = imprimirArrayStr(Usuarios.opcionesMenu, usuario);
            opcion = Validaciones.validarOpcionArraySrt("\nSeleccione opción: ", cantidadImpresa, Usuarios.opcionesMenu, usuario);

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
        ///     Impresión de Matriz de string. Pasar empty/null en el tercer parámetro si no se necesita evaluar tipo de usuario.
        /// </summary>
        /// <param name="auxMatriz">Matriz de string de la que se quiere imprimir la columna</param>
        /// <param name="numColumna">Columna de la Matriz a imprimir</param>
        /// <param name="usuario">Nombre del usuario logueado actualmente pasar null/empty cuando no sea necesario</param>
        /// <returns>Cantidad de elementos impresos</returns>
        public static int imprimirMatrizStr(string[,] auxMatriz, int numColumna, string usuario)
        {
            int cantidadImpresa = 0;

            for (int i = 0; i < auxMatriz.GetLength(0); i++)
            {
                if (!string.IsNullOrEmpty(auxMatriz[i, numColumna]))
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
        ///     Impresión de Array de string. Pasar empty/null en el segundo parámetro cuando no sea necesario evaluar tipo de usuario
        /// </summary>
        /// <param name="auxArray">Array a imprimir</param>
        /// <param name="usuario">Nombre del usuario logueado actualmente pasar null/empty cuando no sea necesario</param>
        /// <returns></returns>
        public static int imprimirArrayStr(string[] auxArray, string usuario)
        {
            int cantidadImpresa = 0;

            if (usuario == "Administrador")
            {
                for (int i = 0; i < auxArray.GetLength(0); i++)
                {
                    if (!string.IsNullOrEmpty(auxArray[i]))
                    {
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



        //HELPERS

        /// <summary>
        ///     Método para evitar que se muestre la escritura de la contraseña en pantalla
        /// </summary>
        /// <returns>Password completa</returns>
        static string ocultarPass()
        {
            StringBuilder password = new StringBuilder();

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            return password.ToString();
        }

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


        //CRUD - Matriz

        public static string[,] agregarElementoMatrizStr(string[,] auxMatriz, string usuario, string password)
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
    }
}
