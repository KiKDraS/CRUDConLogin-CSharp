using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Validaciones
    {
        #region Validaciones de usuario

            /// <summary>
            ///     Método que valida la existencia del nombre de usario ingresado
            /// </summary>
            /// <param name="mensaje">Mensaje a imprimir en pantalla</param>
            /// <param name="auxMatriz">Matriz que contiene los usarios del sistema</param>
            /// <returns>ID del usuario</returns>
            public static int validarUsuario(string mensaje, string[,] auxMatriz)
            {
                int id = -1;
                Console.Write(mensaje);
                string user = Console.ReadLine().Trim();

                while (user != "encontrado")
                {
                    for (int i = 0; i < auxMatriz.GetLength(0); i++)
                    {
                        if (user == auxMatriz[i, 1])
                        {
                            id = i;
                            user = "encontrado";
                            break;
                        }
                    }

                    if (user != "encontrado")
                    {
                        Console.Write("Ususario no existe. \n\nIngrese usuario: ");
                        user = Console.ReadLine().Trim();
                    }
                }

                return id;
            }

            /// <summary>
            ///     Método que valida la password ingresada por el usuario
            /// </summary>
            /// <param name="mensaje">Mensaje a imprimir en pantalla</param>
            /// <param name="auxMatriz">Matriz que contiene la password del sistema para validar</param>
            /// <param name="usuarioSeleccionado">Indice del usuario en la Matriz</param>
            /// <returns></returns>
            public static string validarPass(string mensaje, string[,] auxMatriz, int usuarioSeleccionado)
            {
                Console.Write(mensaje);
                string pass = Usuarios.OcultarPass().Trim();

                while (pass != auxMatriz[usuarioSeleccionado, 2])
                {
                    Console.Clear();
                    Visualizacion.titulo();
                    Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[usuarioSeleccionado, 1]}");
                    Console.Write("Contraseña incorrecta. Vuelva a ingresarla: ");
                    pass = Usuarios.OcultarPass();
                }

                string usuario = Usuarios.matrizUsuarios[usuarioSeleccionado, 1];

                return usuario;
            }

        #endregion

        #region Validaciones de Matrices

            /// <summary>
            ///     Método para validar la carga de datos en una matriz
            /// </summary>
            /// <param name="auxMatriz">Matriz de datos nuevos a cargar</param>
            /// <param name="auxOriginal">Matriz en la que se cargarán los datos</param>
            /// <returns>Matriz de datos validados</returns>
            public static string[,] ValidarCargaDatos(string[,] auxMatriz, string[,] auxOriginal)
            {
                bool continuar = true;

                Console.Clear();
                Visualizacion.titulo();
                Console.WriteLine("Datos ingresados:\n");
                for (int i = 0; i < auxMatriz.GetLength(0); i++)
                {
                    Procedimientos.ImprimirMatriz(auxOriginal);
                    Procedimientos.ImprimirMatriz(auxMatriz);
                }

                continuar = ValidarSalir("\n\nSi los datos son correctos presione ESC. Si quiere cambiarlos presione ENTER", continuar);

                while (continuar)
                {
                    Console.Clear();
                    Visualizacion.titulo();
                    Console.WriteLine("Datos ingresados:\n");
                    for (int i = 0; i < auxMatriz.GetLength(0); i++)
                    {
                        Procedimientos.ImprimirMatriz(auxOriginal);
                        Procedimientos.ImprimirMatriz(auxMatriz);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("");
                    int cantidadImpresa = Procedimientos.MenuOpcionesMatriz(auxOriginal, 0);
                    Console.WriteLine("");                
                    int opcion = ValidarOpcionMatiz("Seleccione dato a modificar: ", cantidadImpresa, auxOriginal);
                    Console.Write($"Escriba nuevo {auxOriginal[0, opcion]}: ");
                    string dato = Console.ReadLine();
                    auxMatriz[0, opcion] = dato;
                    continuar = ValidarSalir("\n\nPresione ENTER para cambiar otro dato, ESC para continuar", continuar);
                }

                return auxMatriz;
            }

            /// <summary>
            ///     Método para validar el ID seleccionado
            /// </summary>
            /// <param name="mensaje">Mensaje para pedir selección de ID</param>
            /// <param name="auxMatriz">Matriz para comprobar la existencia del ID</param>
            /// <returns>Interger que representa el ID validado</returns>
            public static int ValidarID(string mensaje, string[,] auxMatriz)
            {
                int id;
                int aux = -1;

                Console.Write(mensaje);

                do
                {

                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("El ID debe ser numérico");

                    }

                    for (int i = 0; i < auxMatriz.GetLength(0); i++)
                    {
                        if (auxMatriz[i, 0] == id.ToString())
                        {
                            int.TryParse(auxMatriz[i, 0], out id);
                            aux = id;
                            break;
                        }
                    }

                    if (aux == -1)
                    {
                        Console.Write("ID no encontrador. Ingrese uno nuevo: ");
                    }                    

                } while (aux == -1);

                return id;
            }

            /// <summary>
            ///     Método que valida la selección de opción de un menú creado a partir de una matriz
            /// </summary>
            /// <param name="mensaje">Mensaje para pedir el ingreso de la opción</param>
            /// <param name="opciones">Cantidad de opciones disponibles</param>
            /// <param name="auxMatriz">Matriz desde la que se creo el menú</param>
            /// <returns>Interger que representa la opción validada</returns>
            public static int ValidarOpcionMatiz<T>(string mensaje, int opciones, T[,] auxMatriz)
            {
                int opcionValidada = -1;
                int max = opciones + 1;

                Console.Write(mensaje);

                while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
                {
                    Console.Clear();
                    Visualizacion.titulo();
                    Procedimientos.MenuOpcionesMatriz(auxMatriz, 0);
                    Console.WriteLine("Error, reingresar un valor correcto");
                }

                return opcionValidada;
            }

            /// <summary>
            ///     Valida  el ingreso de un dato tipo float
            /// </summary>
            /// <param name="dato">Dato a validar</param>
            /// <returns>Float validado</returns>
            public static float ValidarFloat(string dato)
            {
                float datoFloat; 
                while(!float.TryParse(dato, out datoFloat))
                {
                    Console.Write("Ingrese un valor numérico: ");
                    dato = Console.ReadLine();
                }

                return datoFloat;
            }

        #endregion

        #region Validaciones de Arrays

            /// <summary>
            ///     Valida la opción elegida de un menú armado a partir de un Array de string. Pasar null/empty cuando no sea necesario evaluar el tipo de usuario
            /// </summary>
            /// <param name="mensaje">Mensaje a imprimir en pantalla para pedir el dato a validar</param>
            /// <param name="opciones">Cantidad de opciones impresas en el menú</param>
            /// <param name="auxArray">Array a partir del cual se imprime el menú</param>
            /// <param name="usuario">Usuario logueado</param>
            /// <returns></returns>
            public static int ValidarOpcionArray<T>(string mensaje, int opciones, T[] auxArray, string usuario)
            {
                int opcionValidada = -1;
                int max = opciones + 1;

                Console.Write(mensaje);

                if (usuario == "Admin")
                {
                    while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
                    {
                        Console.Clear();
                        Visualizacion.titulo();
                        Procedimientos.ImprimirArray(auxArray, usuario);
                        Console.WriteLine("Error, reingresar un valor correcto");
                    }
                }
                else
                {
                    while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
                    {
                        Console.Clear();
                        Visualizacion.titulo();
                        Procedimientos.ImprimirArray(auxArray, usuario);
                        Console.WriteLine("Error, reingresar un valor correcto");
                    }
                }

                return opcionValidada;
            }

        #endregion

        #region Otras

            /// <summary>
            ///     Validación de salida
            /// </summary>
            /// <param name="mensaje">Mensaje a imprimir en pantalla</param>
            /// <param name="exit">Booleano que maneja la salida</param>
            /// <returns>Estado del booleano</returns>
            public static bool ValidarSalir(string mensaje, bool exit)
            {            
                Console.WriteLine(mensaje);
                ConsoleKey salir = Console.ReadKey(true).Key;

                while (salir != ConsoleKey.Escape && salir != ConsoleKey.Enter)
                {
                    Console.WriteLine(mensaje);
                    salir = Console.ReadKey(true).Key;
                }

                switch (salir)
                {
                    case ConsoleKey.Escape:
                        Console.Clear();
                        exit = false;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        exit = true;
                        break;

                }

                return exit;
            }

        #endregion

    }
}
