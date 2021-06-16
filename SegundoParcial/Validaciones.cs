﻿using System;
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
            ///     Método que valida la password ingresada por el usuario contra la password guardada en sistema
            /// </summary>
            /// <param name="mensaje">Mensaje a imprimir en pantalla</param>
            /// <param name="auxMatriz">Matriz que contiene la password del sistema para validar</param>
            /// <param name="usuarioSeleccionado">Indice del usuario en la Matriz</param>
            /// <returns></returns>
            public static string validarPass(string mensaje, string[,] auxMatriz, int usuarioSeleccionado)
            {
                Console.Write(mensaje);
                string pass = Usuarios.ocultarPass().Trim();

                while (pass != auxMatriz[usuarioSeleccionado, 2])
                {
                    Console.Clear();
                    Visualizacion.titulo();
                    Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[usuarioSeleccionado, 1]}");
                    Console.Write("Contraseña incorrecta. Vuelva a ingresarla: ");
                    pass = Usuarios.ocultarPass();
                }

                string usuario = Usuarios.matrizUsuarios[usuarioSeleccionado, 1];

                return usuario;
            }

        #endregion


        //VALIDACIONS MATRICES

        public static string[,] validarCargaDatos(string[,] auxMatriz, string[,]auxOriginal)
        {
            bool continuar = true;

            Console.Clear();
            Visualizacion.titulo();
            Console.WriteLine("Datos ingresados:\n");
            for (int i = 0; i < auxMatriz.GetLength(0); i++)
            {
                Procedimientos.imprimirMatriz(auxOriginal);
                Procedimientos.imprimirMatriz(auxMatriz);
            }
            
            continuar = validarSalir("\n\nSi los datos son correctos presione ESC. Si quiere cambiarlos presione ENTER", continuar);            

            while (continuar)
            {
                Console.Clear();
                Visualizacion.titulo();
                Console.WriteLine("Datos ingresados:\n");
                for (int i = 0; i < auxMatriz.GetLength(0); i++)
                {
                    Procedimientos.imprimirMatriz(auxOriginal);
                    Procedimientos.imprimirMatriz(auxMatriz);
                }
                int cantidadImpresa = Procedimientos.menuOpcionesMatriz(auxOriginal, 0);
                Console.WriteLine("");
                Console.WriteLine("");
                int opcion = validarOpcionMatiz("\nSeleccione dato a cambiar: ", cantidadImpresa ,auxOriginal);
                Console.Write($"Escriba nuevo {auxOriginal[0, opcion]}: ");
                string dato = Console.ReadLine();
                auxMatriz[0, opcion] = dato;
                continuar = validarSalir("\n\nPresione ENTER para cambiar otro dato, ESC para continuar", continuar);
            }

            return auxMatriz;
        }

        public static int validarID(string mensaje, string[,] auxMatriz)
        {
            int id = -1;

            Console.Write(mensaje);

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("El ID debe ser numérico");

            }

            while (id == -1)
            {
                for (int i = 0; i < auxMatriz.GetLength(0); i++)
                {
                    if (auxMatriz[i, 0] == id.ToString())
                    {
                        int.TryParse(auxMatriz[i, 0], out id);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("ID no encontrado. Ingrese un nuevo ID");
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("El ID debe ser numérico");

                        }
                    }
                }
            }                                    
            
            return id;
        }

        public static int validarOpcionMatiz<T>(string mensaje, int opciones, T[,] auxMatriz)
        {
            int opcionValidada = -1;
            int max = opciones + 1;

            Console.Write(mensaje);
 
            while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
            {
                Console.Clear();
                Visualizacion.titulo();
                Procedimientos.menuOpcionesMatriz(auxMatriz, 0);
                Console.WriteLine("Error, reingresar un valor correcto");
            }

            return opcionValidada;
        }





        //VALIDACIONES ARRAYS

        /// <summary>
        ///     Valida la opción elegida de un menú armado a partir de un Array de string. Pasar null/empty cuando no sea necesario evaluar el tipo de usuario
        /// </summary>
        /// <param name="mensaje">Mensaje a imprimir en pantalla para pedir el dato a validar</param>
        /// <param name="opciones">Cantidad de opciones impresas en el menú</param>
        /// <param name="auxArray">Array a partir del cual se imprime el menú</param>
        /// <param name="usuario">Usuario logueado</param>
        /// <returns></returns>
        public static int validarOpcionArray<T>(string mensaje, int opciones, T[] auxArray, string usuario)
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
                    Procedimientos.imprimirArray(auxArray, usuario);
                    Console.WriteLine("Error, reingresar un valor correcto");
                }
            }
            else
            {
                while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
                {
                    Console.Clear();
                    Visualizacion.titulo();
                    Procedimientos.imprimirArray(auxArray, usuario);
                    Console.WriteLine("Error, reingresar un valor correcto");
                }
            }

            return opcionValidada;
        }




        //OTRAS

        /// <summary>
        ///     Validación de salida
        /// </summary>
        /// <param name="mensaje">Mensaje a imprimir en pantalla</param>
        /// <param name="exit">Booleano que maneja la salida</param>
        /// <returns>Estado del booleano</returns>
        public static bool validarSalir(string mensaje, bool exit)
        {
            Console.WriteLine(mensaje);
            ConsoleKey salir = Console.ReadKey(true).Key;

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

    }
}
