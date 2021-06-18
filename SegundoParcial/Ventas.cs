using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    class Ventas
    {
        public static string[,] matrizCarro = new string[1, 6]
        {
            {"Vendedor","Venta N°", "ID Producto", "Nombre", "Cantidad", "Precio"}
        };

        public static string[,] facturaCompra = new string[1, 7]
        {
            {"Vendedor", "Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"}
        };

        public static string[,] acumuladoFacturas = new string[1, 7]
        {
            {"Vendedor", "Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"}
        };

        /// <summary>
        ///     Crea un carrito de ventas
        /// </summary>
        /// <param name="usuario">Usuario realizando la venta</param>
        /// <param name="idLibro">ID del libro a agregar al carro</param>
        /// <param name="numVenta">Numero de ventas realizadas</param>
        /// <returns>matrizCarro actualizada</returns>
        public static int CarroVenta(string usuario, int idLibro, int numVenta)
        {
            int indice = Procedimientos.EncontrarIndice(Stock.libros, idLibro.ToString());

            //Imprimir libro seleccionado
            Console.Clear();
            Visualizacion.titulo();
            Console.WriteLine("Datos del libro a vender: \n");
            for (int i = 0; i < Stock.libros.GetLength(1); i++)
            {
                Console.WriteLine($"{Stock.libros[0, i]}: {Stock.libros[indice, i]}");
            }

            //Guardar Nombre
            string nombre = Stock.libros[indice, 1];

            //Seleccionar cantidad a agregar
            Console.Write("\n\nIngrese la cantidad a vender: ");
            string dato = Console.ReadLine();
            float cantidadSeleccionada = Validaciones.ValidarFloat(dato);
            cantidadSeleccionada = Stock.ComprobarDisponibilidadStock(indice, cantidadSeleccionada);

            //Modificación de Stock
            float cantidadRestante = Stock.precioCantidad[indice, 1] - cantidadSeleccionada;
            Stock.libros[indice, 4] = cantidadRestante.ToString();
            Stock.precioCantidad[indice, 1] = cantidadRestante;

            //N° de venta
            for (int i = 1; i < acumuladoFacturas.GetLength(0); i++)
            {
                if (acumuladoFacturas[i, 1] != numVenta.ToString() && !string.IsNullOrEmpty(acumuladoFacturas[i, 1]))
                {
                    int.TryParse(acumuladoFacturas[i, 1], out int aux);
                    numVenta = aux;
                }
            }

            //Precio
            float precio = Stock.precioCantidad[indice, 2] * cantidadSeleccionada;

            //Armado del carro
            int filas = matrizCarro.GetLength(0) + 1;
            Procedimientos.ResizeArray<string>(ref matrizCarro, filas, 6);
            for (int i = 0; i < matrizCarro.GetLength(0); i++)
            {
                if (string.IsNullOrEmpty(matrizCarro[i, 0]))
                {
                    indice = i;
                    break;
                }
            }

            matrizCarro[indice, 0] = usuario;
            matrizCarro[indice, 1] = numVenta.ToString();
            matrizCarro[indice, 2] = idLibro.ToString();
            matrizCarro[indice, 3] = nombre;
            matrizCarro[indice, 4] = cantidadSeleccionada.ToString();
            matrizCarro[indice, 5] = precio.ToString();

            return numVenta;
        }


        /// <summary>
        ///     Vrea la factura de la venta
        /// </summary>
        /// <param name="usuario">Usuario encargado de la venta</param>
        /// <param name="numVenta">Número de ventas realizadas</param>
        /// <returns>Matriz facturas actualizada</returns>
        public static string[,] Factura(int numVenta)
        {
            //Asignar tamaño de Matriz
            int filas = matrizCarro.GetLength(0) + 1;
            Procedimientos.ResizeArray<string>(ref facturaCompra, filas, 7);

            //Armado de factura
            facturaCompra = ArmadoFactura(facturaCompra, numVenta.ToString());

            return facturaCompra;
        }

        public static string[,] ArmadoFactura(string[,] facturaCompra, string numVenta)
        {
            //Suma de totales
            float totalVenta = 0;
            for (int i = 0; i < matrizCarro.GetLength(0); i++)
            {
                if (matrizCarro[i, 1] == numVenta)
                {
                    float.TryParse(matrizCarro[i, 5], out float auxFloat);
                    totalVenta += auxFloat;
                }
            }

            //Carga de datos
            for (int i = 0; i < matrizCarro.GetLength(0); i++)
            {
                for (int j = 0; j < matrizCarro.GetLength(1); j++)
                {
                    facturaCompra[i, j] = matrizCarro[i, j];
                }
            }

            for (int i = 0; i < facturaCompra.GetLength(0); i++)
            {
                if (string.IsNullOrEmpty(facturaCompra[i,0]))
                {
                    facturaCompra[i, 6] = totalVenta.ToString();
                }
            }

            return facturaCompra;
        }

        public static void ImprimirFactura(string[,] facturaCompra)
        {
            //Títulos
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < 1; i++)
            {
                if (!string.IsNullOrEmpty(facturaCompra[i, 1]))
                {
                    Console.Write("|");
                    for (int j = 0; j < facturaCompra.GetLength(1); j++)
                    {
                        if (!string.IsNullOrEmpty(facturaCompra[i, j]))
                        {
                            Console.Write($"{facturaCompra[i, j],-15}|");
                        }
                    }
                    Console.WriteLine("");
                }
            }
            Console.ResetColor();

            //Items
            int auxIndiceI = 0;
            for (int i = 1; i < facturaCompra.GetLength(0); i++)
            {
                Console.Write("|");
                if (!string.IsNullOrEmpty(facturaCompra[i, 1]))
                {
                    for (int j = 0; j < facturaCompra.GetLength(1); j++)
                    {
                        if (!string.IsNullOrEmpty(facturaCompra[i, j]))
                        {
                            Console.Write($"{facturaCompra[i, j],-15}|");
                        }
                    }
                    Console.WriteLine("");
                }
                auxIndiceI++;
            }
            Console.Write($"                                                                                               |{facturaCompra[auxIndiceI, 6]}");

            //Vaciar elementos temporales
            //VaciarCarro(matrizCarro);
            //VaciarFactura(facturaCompra);
        }

        public static string[,] VaciarCarro(string[,] matrizCarro)
        {
            //Reasignar tamaño de Matriz
            Procedimientos.ResizeArray<string>(ref matrizCarro, 1, 7);

            //Agregar valores
            matrizCarro[0, 0] = "Factura N°";
            matrizCarro[0, 1] = "Venta N°";
            matrizCarro[0, 2] = "ID Producto";
            matrizCarro[0, 3] = "Nombre";
            matrizCarro[0, 4] = "Cantidad";
            matrizCarro[0, 5] = "Precio";

            return matrizCarro;
        }

        public static string[,] VaciarFactura(string[,] facturaCompra)
        {
            //Reasignar tamaño de Matriz
            Procedimientos.ResizeArray<string>(ref facturaCompra, 1, 6);

            //Agregar valores
            matrizCarro[0, 0] = "Vendedor";
            matrizCarro[0, 1] = "Factura N°";
            matrizCarro[0, 2] = "ID Producto";
            matrizCarro[0, 3] = "Nombre";
            matrizCarro[0, 4] = "Cantidad";
            matrizCarro[0, 5] = "Precio";
            matrizCarro[0, 6] = "Total";

            return facturaCompra;
        }

    }
}
