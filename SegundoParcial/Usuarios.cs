using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Usuarios
    {
        public static string[,] matrizUsuarios = new string[4, 3]
        {  
            {"ID",   "Usuario",         "Password"},
            {"1",    "Admin",            "admin"},
            {"2",    "Vendedor1",        "vendedor1"},
            {"3",    "Vendedor2",        "vendedor2"}
        };

        //CRUD

        /// <summary>
        ///     Método para agregar dato a matrizUsuarios. Revisa si hay espacios disponible y, en caso de no haberlo, agranda el tamaño de la matriz
        /// </summary>
        /// <param name="exit">Booleano para manejar la nevegación por los distintos menues</param>
        /// <returns>Booleano para manejar navegación por los distintos menues</returns>
        public static bool AgregarUsuario(bool continuar)
        {

            //Cargar nuevo usuario
            string[,] elemAgregar = Procedimientos.ArmarMatrizDatosNuevos(matrizUsuarios);

            //Agregar usuario a la matriz
            matrizUsuarios = Procedimientos.AgregarElementoMatrizStr(matrizUsuarios, elemAgregar);

            continuar = Validaciones.ValidarSalir("\n\n Presione ENTER para agregar otro usuario o ESC para volver al menú anterior", continuar);

            return continuar;
        }

        /// <summary>
        ///     Método para modificar dato en matrizUsuarios
        /// </summary>
        /// <param name="id">ID del usuario a modificar</param>
        /// <param name="exit">Boolano que maneja la navegación</param>
        /// <returns>Booleano que maneja la navegación</returns>
        public static bool ModificarUsuario(string id, bool exit)
        {
            int indice = -1;
            string dato;

            if(id == "1")
            {
                Console.Clear();
                Visualizacion.Titulo();
                Console.WriteLine("El usuario Admin sólo admite modificación de contraseña");                
                Console.Write("Escriba nueva contraseña: ");
                dato = Console.ReadLine();
                matrizUsuarios[1, 2] = dato;
                Console.WriteLine("Constraseña modificada. Presione cualquier tecla para salir");
                Console.ReadKey();
                exit = false;
            }

            while (exit)
            {
                //Imprimir usuario seleccionado
                Console.Clear();
                Visualizacion.Titulo();
                Console.WriteLine("Datos de usuario a modificar: \n");
                indice = Procedimientos.EncontrarIndice(matrizUsuarios, id);
                for (int i = 0; i < matrizUsuarios.GetLength(1); i++)
                {
                    Console.WriteLine($"{matrizUsuarios[0, i]}: {matrizUsuarios[indice, i]}");
                }

                //Seleccionar dato a modificar
                Console.WriteLine("");
                Console.WriteLine("");
                int cantidadImpresa = Procedimientos.MenuOpcionesMatriz(matrizUsuarios, 0);
                Console.WriteLine("");                
                int opcion = Validaciones.ValidarOpcionMatiz("Seleccione dato a modificar: ", cantidadImpresa, matrizUsuarios);

                //Modificar dato
                Console.Write($"Escriba nuevo {matrizUsuarios[0, opcion]}: ");
                dato = Console.ReadLine();
                matrizUsuarios[indice, opcion] = dato;
                exit = Validaciones.ValidarSalir("\n\nPresione ENTER para cambiar otro dato, ESC para continuar", exit);

            } 
            
            return exit;
        }

        /// <summary>
        ///     Método para eliminar dato en matrizUsuarios
        /// </summary>
        /// <param name="id">ID del usuario a eliminar</param>
        /// <param name="exit">Boolano que maneja la navegación</param>
        /// <returns>Boolano que maneja la navegación</returns>
        public static bool EliminarUsuario(string id, bool exit)
        {
            int indice = -1;
            bool borrar = true;

            if (id == "1")
            {
                Console.Clear();
                Visualizacion.Titulo();
                Console.WriteLine("No se puede eliminar al usuario Admin. Presione cualquier tecla para volver al menú anterior");
                Console.ReadKey();
                exit = false;
            }
            else
            {

                //Imprimir usuario seleccionado
                Console.Clear();
                Visualizacion.Titulo();
                Console.WriteLine("Datos del usuario a eliminar: \n");
                indice = Procedimientos.EncontrarIndice(matrizUsuarios, id);
                for (int i = 0; i < matrizUsuarios.GetLength(1); i++)
                {
                    Console.WriteLine($"{matrizUsuarios[0, i]}: {matrizUsuarios[indice, i]}");
                }

                borrar = Validaciones.ValidarSalir("\nSi es correcto, presione ENTER. De lo contrario, presione ESC", borrar);

                //Eliminar usuario                             

                if (borrar)
                {
                    Console.Clear();
                    Visualizacion.Titulo();
                    for (int i = 0; i < matrizUsuarios.GetLength(1); i++)
                    {
                        matrizUsuarios[indice, i] = "";

                    }
                    Console.WriteLine("Usuario eliminado");
                    exit = Validaciones.ValidarSalir("\nPresione ESC para volver al menú anterior", exit);
                }

            }

            return exit;
        }

        /// <summary>
        ///     Método para evitar que se muestre la escritura de la contraseña en pantalla
        /// </summary>
        /// <returns>Password completa</returns>
        public static string OcultarPass()
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
