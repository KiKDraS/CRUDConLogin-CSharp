using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Validaciones
    {
        //VALIDACIONES MATRICES

        /// <summary>
        ///     Valida la opción elegida del menú armado a partir de una Matriz de string. Pasar null/empty en el último parámetro cuando no sea necesario evaluar tipo de usuario
        /// </summary>
        /// <param name="mensaje">Mensaje a imprimir en pantalla para pedir el dato a validar</param>
        /// <param name="opciones">Cantidad de opciones impresas en el menú</param>
        /// <param name="auxMatriz">Matriz de string para reimprimir menú en caso de ser necesarioa</param>
        /// <param name="numColumna">Posición de la columna que se desea imprimir</param>
        /// <param name="usuario">Usuario logueado</param>
        /// <returns>Opción elegida con las validaciones realizadas</returns>
        public static int validarOpcionMatriz<T>(string mensaje, int opciones, T[,]auxMatriz, int numColumna, string usuario)
        {
            int opcionValidada;
            int max = opciones+1;

            Console.Write(mensaje);

            while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
            {
                Console.Clear();
                Procedimientos.titulo();
                Procedimientos.imprimirMatriz(auxMatriz, numColumna, usuario);
                Console.WriteLine("Error, reingresar un valor correcto");
            }

            return opcionValidada;
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

            while (pass != auxMatriz[usuarioSeleccionado - 1, 2])
            {
                Console.Clear();
                Procedimientos.titulo();
                Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[usuarioSeleccionado - 1, 1]}");
                Console.Write("Contraseña incorrecta. Vuelva a ingresarla: ");
                pass = Usuarios.ocultarPass();
            }

            string usuario = Usuarios.matrizUsuarios[usuarioSeleccionado - 1, 1];

            return usuario;
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

            if (usuario == "Administrador")
            {
                while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
                {
                    Console.Clear();
                    Procedimientos.titulo();
                    Procedimientos.imprimirArray(auxArray, usuario);
                    Console.WriteLine("Error, reingresar un valor correcto");
                }
            }
            else
            {
                while (!int.TryParse(Console.ReadLine(), out opcionValidada) || opcionValidada < 1 || opcionValidada > max)
                {
                    Console.Clear();
                    Procedimientos.titulo();
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
