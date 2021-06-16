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
        public static bool agregarUsuario(bool continuar)
        {

            //Cargar nuevo usuario
            string[,] elemAgregar = Procedimientos.armarMatrizDatosNuevos(matrizUsuarios);

            //Agregar usuario a la matriz
            matrizUsuarios = Procedimientos.agregarElementoMatrizStr(matrizUsuarios, elemAgregar);

            continuar = Validaciones.validarSalir("\n\n Presione ENTER para agregar otro usuario o ESC para volver al menú anterior", continuar);

            return continuar;
        }

        public static bool modificarUsuario(string id, bool exit)
        {
            int indice = -1;
            string dato;

            if(id == "1")
            {
                Console.Clear();
                Visualizacion.titulo();
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
                Visualizacion.titulo();
                Console.WriteLine("Datos de usuario a modificar: \n");
                for (int i = 0; i < matrizUsuarios.GetLength(0); i++)
                {
                    if (matrizUsuarios[i, 0].ToString() == id)
                    {
                        indice = i;
                        break;
                    }
                }
                for (int i = 0; i < matrizUsuarios.GetLength(1); i++)
                {
                    Console.WriteLine($"{matrizUsuarios[0, i]}: {matrizUsuarios[indice, i]}");
                }

                //Seleccionar dato a modificar
                Console.WriteLine("");
                Console.WriteLine("");
                int cantidadImpresa = Procedimientos.menuOpcionesMatriz(matrizUsuarios, 0);
                Console.WriteLine("");                
                int opcion = Validaciones.validarOpcionMatiz("Seleccione dato a modificar: ", cantidadImpresa, matrizUsuarios);

                //Modificar dato
                Console.Write($"Escriba nuevo {matrizUsuarios[0, opcion]}: ");
                dato = Console.ReadLine();
                matrizUsuarios[indice, opcion] = dato;
                exit = Validaciones.validarSalir("\n\nPresione ENTER para cambiar otro dato, ESC para continuar", exit);

            } 
            
            return exit;
        }

        public static bool eliminarUsuario(string id, bool exit)
        {
            int indice = -1;
            bool borrar = true;

            if (id == "1")
            {
                Console.Clear();
                Visualizacion.titulo();
                Console.WriteLine("No se puede eliminar al usuario Admin. Presione cualquier tecla para volver al menú anterior");
                Console.ReadKey();
                exit = false;
            }
            else
            {

                //Imprimir usuario seleccionado
                Console.Clear();
                Visualizacion.titulo();
                Console.WriteLine("Datos del usuario a eliminar: \n");
                for (int i = 0; i < matrizUsuarios.GetLength(0); i++)
                {
                    if (matrizUsuarios[i, 0].ToString() == id)
                    {
                        indice = i;
                        break;
                    }
                }
                for (int i = 0; i < matrizUsuarios.GetLength(1); i++)
                {
                    Console.WriteLine($"{matrizUsuarios[0, i]}: {matrizUsuarios[indice, i]}");
                }

                borrar = Validaciones.validarSalir("\nSi es correcto, presione ENTER. De lo contrario, presione ESC", borrar);

                //Eliminar usuario                             

                if (borrar)
                {
                    Console.Clear();
                    Visualizacion.titulo();
                    for (int i = 0; i < matrizUsuarios.GetLength(1); i++)
                    {
                        matrizUsuarios[indice, i] = "";

                    }
                    Console.WriteLine("Usuario eliminado");
                    exit = Validaciones.validarSalir("\nPresione ESC para volver al menú anterior", exit);
                }

            }

            return exit;
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
