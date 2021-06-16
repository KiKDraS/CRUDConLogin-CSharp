﻿using System;
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
            "Ventas",
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
        ///     Login de usuario
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
        /// </summary>
        ///     <param name="usuario">Nombre del usuario logueado actualmente</param>
        ///     <param name="exit">Estado de booleano para salir del programa</param>
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

        #region Sub-Menus. Todos reciben usuario actual y estado del booleando exit manejar navegación

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
                            //Ventas
                            break;

                        case 4:
                            //Facturación del día
                            break;

                        case 5:
                            exit = false;
                            break;
                    }

                    exit = true; //Reseteo de variable

                } while (exit);

                return exit;
            }

            public static bool menuCrudUsuarios(string usuario, bool exit)
            {
                do
                {
                    bool continuar = true;
                    
                    Console.Clear();
                    titulo();
                    Console.WriteLine("\nMenu Opciones Usuarios\n");
                    int cantidadImpresa = Procedimientos.imprimirArray(menuOpcionesUsuarios, usuario);
                    int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                    switch (opcion)
                    {
                        case 1:

                        //Mostrar/Agregar
                        Console.Clear();
                        titulo();
                        Console.WriteLine("Lista de usuarios\n\n");
                        Procedimientos.imprimirMatriz(Usuarios.matrizUsuarios);
                        continuar = Validaciones.validarSalir("\n\nPresione Enter para agregar usuario o ESC para volver al menú anterior", continuar);

                        while(continuar)
                        {
                            Console.Clear();
                            titulo();
                            Console.WriteLine("Agregar Nuevo Usuario\n");
                            continuar = Usuarios.agregarUsuario(continuar);
                        } 

                        break;

                        case 2:

                        //Modificar
                        Console.Clear();
                        titulo();
                        Console.WriteLine("Lista de usuarios\n");
                        Procedimientos.imprimirMatriz(Usuarios.matrizUsuarios);
                        continuar = Validaciones.validarSalir("\n\nPresione Enter para modificar usuario o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            titulo();
                            Console.WriteLine("Modificar usuario\n");
                            Procedimientos.imprimirMatriz(Usuarios.matrizUsuarios);
                            //Seleccionar usuario
                            idSeleccionado = Validaciones.validarID("\nIngrese ID del usuario a modificar: ", Usuarios.matrizUsuarios);
                            //Modificar usuario
                            continuar = Usuarios.modificarUsuario(idSeleccionado.ToString(), continuar);
                        } 

                        break;

                        case 3:

                        //Eliminar
                        Console.Clear();
                        titulo();
                        Console.WriteLine("Lista de usuarios\n\n");
                        Procedimientos.imprimirMatriz(Usuarios.matrizUsuarios);
                        continuar = Validaciones.validarSalir("\n\nPresione Enter para eliminar usuario o ESC para volver al menú anterior", continuar);
                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            titulo();
                            Console.WriteLine("Eliminar usuario\n");
                            Procedimientos.imprimirMatriz(Usuarios.matrizUsuarios);
                            //Seleccionar usuario
                            idSeleccionado = Validaciones.validarID("\nIngrese ID del usuario a eliminar: ", Usuarios.matrizUsuarios);
                            //Eliminar usuario
                            continuar = Usuarios.eliminarUsuario(idSeleccionado.ToString(), continuar);
                        }

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
                    bool continuar = true;

                    Console.Clear();
                    titulo();
                    Console.WriteLine($"\nMenu Opciones Stock\n");
                    int cantidadImpresa = Procedimientos.imprimirArray(menuOpcionesStock, usuario);
                    int opcion = Validaciones.validarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                    switch (opcion)
                    {
                        case 1:
                        //Mostrar/Agregar
                        Console.Clear();
                        titulo();
                        Console.WriteLine("Libros en stock\n\n");
                        Procedimientos.imprimirMatriz(Stock.libros);
                        continuar = Validaciones.validarSalir("\n\nPresione Enter para agregar libro o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            Console.Clear();
                            titulo();
                            Console.WriteLine("Agregar Nuevo Libro\n");
                            continuar = Stock.agregarLibro(continuar);
                        }

                        break;

                        case 2:

                        //Modificar
                        Console.Clear();
                        titulo();
                        Console.WriteLine("Lista de libros en Stock\n");
                        Procedimientos.imprimirMatriz(Stock.libros);
                        continuar = Validaciones.validarSalir("\n\nPresione Enter para modificar libro o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            titulo();
                            Console.WriteLine("Modificar libro\n");
                            Procedimientos.imprimirMatriz(Stock.libros);
                            //Seleccionar libro
                            idSeleccionado = Validaciones.validarID("\nIngrese ID del libro a modificar: ", Stock.libros);
                            //Modificar libro
                            continuar = Stock.modificarLibro(idSeleccionado.ToString(), continuar);
                        }

                        break;

                        case 3:

                        //Eliminar
                        Console.Clear();
                        titulo();
                        Console.WriteLine("Lista de libros en Stock\n\n");
                        Procedimientos.imprimirMatriz(Stock.libros);
                        continuar = Validaciones.validarSalir("\n\nPresione Enter para eliminar libro o ESC para volver al menú anterior", continuar);
                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            titulo();
                            Console.WriteLine("Eliminar libro\n");
                            Procedimientos.imprimirMatriz(Stock.libros);
                            //Seleccionar usuario
                            idSeleccionado = Validaciones.validarID("\nIngrese ID del libro a eliminar: ", Stock.libros);
                            //Eliminar usuario
                            continuar = Stock.eliminarLibro(idSeleccionado.ToString(), continuar);
                        }

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
                            exit = menuVentas(usuario, exit);
                            break;

                        case 2:
                            exit = false;
                            break;
                    }

                } while (exit);

                return exit;
            }

            public static bool menuVentas(string usuario, bool exit)
            {
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

        #endregion

    }
}
