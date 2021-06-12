﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Usuarios
    {
        public static string[,] matrizUsuarios = new string[5, 3]
        {
            //ID     //User              //Pass   
            {"ID",   "Usuario",         "Password"},
            {"1",    "Admin",            "admin"},
            {"",     "" ,                ""},
            {"3",    "Vendedor1",        "vendedor1"},
            {"4",    "Vendedor2",        "vendedor2"}
        };

        //CRUD

        public static void agregarUsuario(string[,] matrizUsuarios)
        {
            string[,] elemAgregar = Procedimientos.armarMatrizDatosNuevos(matrizUsuarios);
            //Validación de carga
            Console.WriteLine("Revise los datos ingresados: ");
            Procedimientos.imprimirMatriz(elemAgregar);
            Validaciones.validarCargaDatos(elemAgregar);
            Procedimientos.agregarElementoMatrizStr(matrizUsuarios, elemAgregar);
        }

        /// <summary>
        ///     Método para evitar que se muestre la escritura de la contraseña en pantalla
        /// </summary>
        /// <returns>Password completa</returns>
        public static string ocultarPass()
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
    
    }
}
