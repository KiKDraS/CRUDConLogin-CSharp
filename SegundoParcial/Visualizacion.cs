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

        public static string[] menuOpcionesFacturacion =
        {
            "Buscar factura",
            "Total facturado"
        };


        /// <summary>
        ///     Impresión del título de la aplicación
        /// </summary>
        public static void Titulo()
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
        public static string Login()
        {
            Titulo();
            Console.WriteLine("Login\n");
            //validación de usuario
            int id = Validaciones.validarUsuario("Ingrese usuario: ", Usuarios.matrizUsuarios);
            //Validación de contraseña de usuario
            Console.WriteLine($"Usuario seleccionado: {Usuarios.matrizUsuarios[id, 1]}");
            string usuario = Validaciones.validarPass("Ingrese contraseña: ", Usuarios.matrizUsuarios, id);

            return usuario;

        }

        /// <summary>
        ///     Imprime el menú para los distintos usuarios
        /// </summary>
        ///     <param name="usuario">Nombre del usuario logueado actualmente</param>
        ///     <param name="exit">Estado de booleano para salir del programa</param>
        /// <returns>Estado de booleano exit para salir del programa</returns>
        public static bool MenuUser(string usuario, bool exit)
        {
            do
            {
                switch (usuario)
                {
                    case "Admin":
                        exit = MenuAdmin(usuario, exit);
                        if (exit == false)
                        {
                            exit = Validaciones.ValidarSalir("\nPresione ENTER para volver al menú o ESC para salir", exit);
                        }                                                    
                        break;

                    default:
                        exit = MenuVendedores(usuario, exit);
                        if (exit == false)
                        {
                            exit = Validaciones.ValidarSalir("\nPresione ENTER para volver al menú o ESC para salir", exit);
                        }
                        break;
                }

            } while (exit);

            return exit;
        }

        #region Sub-Menus. Todos reciben usuario actual y estado del booleando exit para manejar navegación

            public static bool MenuAdmin(string usuario, bool exit)
            {
                bool continuar = true;
                int opcion = 0;
                
                do
                {                   
                    Console.Clear();
                    Titulo();
                    Console.WriteLine($"\nMenu {usuario}\n");
                    int cantidadImpresa = Procedimientos.ImprimirArray(opcionesMenu, usuario);
                    opcion = Validaciones.ValidarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                    switch (opcion)
                    {
                        case 1:
                            //CRUD usuarios
                            continuar = MenuCrudUsuarios(usuario, exit);                            
                            break;

                        case 2:
                            //CRUD stock
                            continuar = MenuCrudStock(usuario, exit);
                            break;

                        case 3:
                            //Ventas
                            continuar = MenuVentas (usuario, exit);
                            break;

                        case 4:
                            //Facturación del día
                            continuar = MenuFacturacion(usuario, exit);
                            break;

                        case 5:
                            continuar = false;
                            break;
                    }

                } while (continuar);

                if (opcion == 5)
                {
                    exit = false;
                }                

                return exit;
            }

            public static bool MenuCrudUsuarios(string usuario, bool exit)
            {
                do
                {
                    bool continuar = true;
                    
                    Console.Clear();
                    Titulo();
                    Console.WriteLine("\nMenu Opciones Usuarios\n");
                    int cantidadImpresa = Procedimientos.ImprimirArray(menuOpcionesUsuarios, usuario);
                    int opcion = Validaciones.ValidarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                    switch (opcion)
                    {
                        case 1:

                        //Mostrar/Agregar
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Lista de usuarios\n\n");
                        Procedimientos.ImprimirMatriz(Usuarios.matrizUsuarios);
                        continuar = Validaciones.ValidarSalir("\n\nPresione Enter para agregar usuario o ESC para volver al menú anterior", continuar);

                        while(continuar)
                        {
                            Console.Clear();
                            Titulo();
                            Console.WriteLine("Agregar Nuevo Usuario\n");
                            continuar = Usuarios.AgregarUsuario(continuar);
                        } 

                        break;

                        case 2:

                        //Modificar
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Lista de usuarios\n");
                        Procedimientos.ImprimirMatriz(Usuarios.matrizUsuarios);
                        continuar = Validaciones.ValidarSalir("\n\nPresione Enter para modificar usuario o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            Titulo();
                            Console.WriteLine("Modificar usuario\n");
                            Procedimientos.ImprimirMatriz(Usuarios.matrizUsuarios);
                            //Seleccionar usuario por ID
                            idSeleccionado = Validaciones.ValidarID("\nIngrese ID del usuario a modificar: ", Usuarios.matrizUsuarios);
                            //Modificar usuario
                            continuar = Usuarios.ModificarUsuario(idSeleccionado.ToString(), continuar);
                        } 

                        break;

                        case 3:

                        //Eliminar
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Lista de usuarios\n\n");
                        Procedimientos.ImprimirMatriz(Usuarios.matrizUsuarios);
                        continuar = Validaciones.ValidarSalir("\n\nPresione Enter para eliminar usuario o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            Titulo();
                            Console.WriteLine("Eliminar usuario\n");
                            Procedimientos.ImprimirMatriz(Usuarios.matrizUsuarios);
                            //Seleccionar usuario por ID
                            idSeleccionado = Validaciones.ValidarID("\nIngrese ID del usuario a eliminar: ", Usuarios.matrizUsuarios);
                            //Eliminar usuario
                            continuar = Usuarios.EliminarUsuario(idSeleccionado.ToString(), continuar);
                        }

                        break;

                        case 4:
                            exit = false;
                            break;
                    }

                } while (exit);

                return exit;
            }

            public static bool MenuCrudStock(string usuario, bool exit)
            {
                do
                {
                    bool continuar = true;

                    Console.Clear();
                    Titulo();
                    Console.WriteLine($"\nMenu Opciones Stock\n");
                    int cantidadImpresa = Procedimientos.ImprimirArray(menuOpcionesStock, usuario);
                    int opcion = Validaciones.ValidarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                    switch (opcion)
                    {
                        case 1:
                        //Mostrar/Agregar
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Libros en stock\n\n");
                        Procedimientos.ImprimirMatriz(Stock.libros);
                        continuar = Validaciones.ValidarSalir("\n\nPresione Enter para agregar libro o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            Console.Clear();
                            Titulo();
                            Console.WriteLine("Agregar Nuevo Libro\n");
                            continuar = Stock.AgregarLibro(continuar);
                        }

                        break;

                        case 2:

                        //Modificar
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Lista de libros en Stock\n");
                        Procedimientos.ImprimirMatriz(Stock.libros);
                        continuar = Validaciones.ValidarSalir("\n\nPresione Enter para modificar libro o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            Titulo();
                            Console.WriteLine("Modificar libro\n");
                            Procedimientos.ImprimirMatriz(Stock.libros);
                            //Seleccionar libro por ID
                            idSeleccionado = Validaciones.ValidarID("\nIngrese ID del libro a modificar: ", Stock.libros);
                            //Modificar libro
                            continuar = Stock.ModificarLibro(idSeleccionado.ToString(), continuar);
                        }

                        break;

                        case 3:

                        //Eliminar
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Lista de libros en Stock\n\n");
                        Procedimientos.ImprimirMatriz(Stock.libros);
                        continuar = Validaciones.ValidarSalir("\n\nPresione Enter para eliminar libro o ESC para volver al menú anterior", continuar);

                        while (continuar)
                        {
                            int idSeleccionado;

                            Console.Clear();
                            Titulo();
                            Console.WriteLine("Eliminar libro\n");
                            Procedimientos.ImprimirMatriz(Stock.libros);
                            //Seleccionar libro por ID
                            idSeleccionado = Validaciones.ValidarID("\nIngrese ID del libro a eliminar: ", Stock.libros);
                            //Eliminar libro
                            continuar = Stock.EliminarLibro(idSeleccionado.ToString(), continuar);
                        }

                        break;

                        case 4:
                            exit = false;
                            break;
                    }

                } while (exit);

                return exit;
            }

            public static bool MenuVendedores(string usuario, bool exit)
            {
                bool continuar = true;
                int opcion = 0;

                do
                {
                    Console.Clear();
                    Titulo();
                    Console.WriteLine($"\nMenu {usuario}\n");
                    int cantidadImpresa = Procedimientos.ImprimirArray(opcionesMenu, usuario);
                    opcion = Validaciones.ValidarOpcionArray("\nSeleccione opción: ", cantidadImpresa, opcionesMenu, usuario);

                    switch (opcion)
                    {
                        case 1:
                            continuar = MenuVentas(usuario, exit);
                            break;

                        case 2:
                            continuar = MenuFacturacion(usuario, exit);
                            break;

                        case 3:
                            continuar = false;
                            break;
                    }

                } while (continuar);

                if (opcion == 3)
                {
                    exit = false;
                }

                return exit;
            }

            public static bool MenuVentas(string usuario, bool exit)
            {
                do
                {
                    bool facturar = false;
                    int numVenta = 1;

                    Console.Clear();
                    Titulo();
                    Console.WriteLine($"Libros en Stock\n");
                    Procedimientos.ImprimirMatriz(Stock.libros);
                    bool continuar = Validaciones.ValidarSalir("\n\nPresione Enter para vender o ESC para volver al menú anterior", exit);

                    if (continuar)
                    {
                        facturar = true;
                    }

                    while (continuar)
                    {
                        int idSeleccionado;                        

                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Realizar Venta\n");
                        Procedimientos.ImprimirMatriz(Stock.libros);
                        //Seleccionar libro por ID
                        idSeleccionado = Validaciones.ValidarID("\nIngrese ID del libro a vender: ", Stock.libros);
                        //Carga Carro
                        numVenta = Ventas.CarroVenta(usuario, idSeleccionado, numVenta);
                        //Cerrar Carro
                        continuar = Validaciones.ValidarSalir("\nProducto agregado al carro. \nPrecione ENTER para agregar otro o ESC para continuar", continuar);

                    }

                    if (facturar)
                    {
                        //Facturación
                        Console.Clear();
                        Titulo();
                        Console.WriteLine("Factura\n");
                        //Crear Factura
                        Ventas.Factura(numVenta);
                        //Imprimir Factura
                        Ventas.ImprimirFactura(Ventas.facturaCompra);
                        //Guardar Factura
                        Facturacion.acumuladoFacturas = Ventas.GuardarFactura(Ventas.facturaCompra);
                        //Vaciar elementos temporales
                        Ventas.matrizCarro = Ventas.VaciarTemporales(Ventas.matrizCarro);
                        Ventas.facturaCompra = Ventas.VaciarTemporales(Ventas.facturaCompra);
                        facturar = Validaciones.ValidarSalir("\n\nPresione ENTER para realizar una nueva venta o ESC para salir", exit);                        
                    }

                    if (facturar == false)
                    {
                        exit = false;
                    }

                } while (exit);

                return exit;
            }

            public static bool MenuFacturacion(string usuario, bool exit)
            {    
                do
                {
                    Console.Clear();
                    Titulo();
                    Console.WriteLine($"\nMenu Facturación\n");
                    int cantidadImpresa = Procedimientos.ImprimirArray(menuOpcionesFacturacion, usuario);
                    int opcion = Validaciones.ValidarOpcionArray("\nSeleccione opción: ", cantidadImpresa, menuOpcionesFacturacion, usuario);

                    switch (opcion)
                    {
                        case 1:
                            Console.Clear();
                            Titulo();
                            Console.WriteLine($"\nBuscar Factura\n");
                            Facturacion.BuscarFactura();
                            break;

                        case 2:
                            Console.Clear();
                            Titulo();
                            Console.WriteLine($"\nTotal Facturado\n");
                            Facturacion.TotalFacturado();
                            break;

                        case 3:
                            exit = false;
                            break;
                    }

                } while (exit);

                return exit;
            }          

        #endregion

    }
}
