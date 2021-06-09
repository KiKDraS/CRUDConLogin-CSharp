using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    class Visualizacion
    {
        public static string[] opcionesMenu =
        {
            "Opciones de usuarios",
            "Opciones de stock",
            "Vender",
            "Facturación del día"
        };

        public static string[] menuOpcionesUsuarios =
        {
            "Mostrar/Agregar Usuarios",
            "Modificar Usuarios",
            "Eliminar Usuarios",
        };

        public static string[] menuOpcionesStock =
        {
            "Mostrar/Agregar Libros",
            "Modificar Libros",
            "Eliminar Libro",
        };


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
            //validación de usuario
            int id = Validaciones.validarUsuario("Ingrese usuario: ", Usuarios.matrizUsuarios);
            //Validación de contraseña de usuario
            Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[id, 1]}");
            usuario = Validaciones.validarPass("Ingrese contraseña: ", Usuarios.matrizUsuarios, id);

            return usuario;

        }

        /// <summary>
        ///     Imprime el menú para los distintos usuarios
        ///     <param name="usuario">Nombre del usuario logueado actualmente</param>
        /// </summary>
        /// <returns>Estado de booleano exit para salir del programa</returns>
        public static bool menuUser(string usuario, bool exit)
        {
            do
            {
                switch (usuario)
                {
                    case "Admin":
                        menuAdmin(usuario, exit);
                        exit = Validaciones.validarSalir("\nPresione ENTER para volver al menú o ESC para salir", exit);
                        break;

                    default:
                        menuVendedores(usuario, exit);
                        exit = Validaciones.validarSalir("\nPresione ENTER para volver al menú o ESC para salir", exit);
                        break;
                }

            } while (exit);

            return exit;
        }

        public static bool menuAdmin(string usuario, bool exit)
        {
            do
            {
                Console.Clear();
                titulo();
                Console.WriteLine($"\nMenu {usuario}\n");
                int cantidadImpresa = Procedimientos.imprimirArray(opcionesMenu, usuario);
                int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                switch (opcion)
                {
                    case 1:
                        menuCrudUsuarios(usuario, exit);
                        break;

                    case 2:
                        menuCrudStock(usuario, exit);
                        break;

                    case 3:
                        break;

                    case 4:

                        break;

                    case 5:
                        exit = false;
                        break;
                }

            } while (exit);

            return exit;
        }
    
        public static bool menuCrudUsuarios(string usuario, bool exit)
        {
            do
            {
                Console.Clear();
                titulo();
                Console.WriteLine($"\nMenu Opciones Usuarios\n");
                int cantidadImpresa = Procedimientos.imprimirArray(menuOpcionesUsuarios, usuario);
                int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                switch (opcion)
                {
                    case 1:
                        
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        exit = false;
                        break;
                }

            } while (exit);

            return exit;
        }

        public static bool menuCrudStock(string usuario, bool exit)
        {
            do
            {
                Console.Clear();
                titulo();
                Console.WriteLine($"\nMenu Opciones Stock\n");
                int cantidadImpresa = Procedimientos.imprimirArray(menuOpcionesStock, usuario);
                int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                switch (opcion)
                {
                    case 1:

                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        exit = false;
                        break;
                }

            } while (exit);

            return exit;
        }

        public static bool menuVendedores(string usuario, bool exit)
        {
            do
            {
                Console.Clear();
                titulo();
                Console.WriteLine($"\nMenu {usuario}\n");
                int cantidadImpresa = Procedimientos.imprimirArray(opcionesMenu, usuario);
                int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                switch (opcion)
                {
                    case 1:
                        exit = menuVentas();
                        break;

                    case 2:
                        exit = false;
                        break;
                }

            } while (exit);

            return exit;
        }

        public static bool menuVentas()
        {
            bool exit = true;

            do
            {
                Console.Clear();
                titulo();
                Console.WriteLine($"\nMenu Ventas\n");
                Console.WriteLine("1. PRUEBA DE MENU");
                Console.WriteLine("2. Salir");
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:

                        break;

                    case 2:
                        exit = false;
                        break;
                }

            } while (exit);

            return exit;
        }
    }
}
