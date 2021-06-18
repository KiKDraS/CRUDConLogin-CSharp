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
            {"Vendedor","N° Venta", "ID Producto", "Nombre", "Cantidad", "Precio"}
        };

        public static string[,] facturaCompra = new string[1, 7]
        {
            {"Vendedor", "Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"}

        };

        public static string[,] acumuladoFacturas = new string[8, 7]
        {
            {"Vendedor", "Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"},
            {"Vendedor1", "1", "3", "Juego Tronos", "1", "200", ""},
            {"Vendedor1", "1", "5", "El fact Scarp", "1", "130", ""},
            {"", "", "", "", "", "", "330"},
            {"Vendedor1", "2", "1", "Elantris", "1", "150", ""},
            {"", "", "", "", "", "", "150"},
            {"Vendedor2", "3", "7", "La perf silen", "1", "160", ""},
            {"", "", "", "", "", "", "160"}
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
            numVenta = CalcularVenta();            

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
            if(matrizCarro.GetLength(0) >= facturaCompra.GetLength(0))
            {
                int filas = matrizCarro.GetLength(0) + 1;
                Procedimientos.ResizeArray<string>(ref facturaCompra, filas, 7);
            }            

            //Armado de factura
            facturaCompra = ArmadoFactura(facturaCompra, numVenta.ToString());

            return facturaCompra;
        }

        /// <summary>
        ///     Almacena la factura actual en la matriz acumuladoFacturas
        /// </summary>
        /// <param name="facturaCompra">Factura actual</param>
        /// <returns>Matriz acumuladoFacturas actualizada</returns>
        public static string[,] GuardarFactura(string[,] facturaCompra)
        {
            //Tamaño de matriz            
            if (acumuladoFacturas.GetLength(0) == 1)
            {
                int filas = facturaCompra.GetLength(0);
                Procedimientos.ResizeArray<string>(ref acumuladoFacturas, filas, 7);
            }
            else
            {
                int filas = (acumuladoFacturas.GetLength(0) + facturaCompra.GetLength(0)) - 1;
                Procedimientos.ResizeArray<string>(ref acumuladoFacturas, filas, 7);

            }

            //Buscar último indice ocupado
            int indice = -1;
            for (int i = 1; i < acumuladoFacturas.GetLength(0); i++)
            {
                if (string.IsNullOrEmpty(acumuladoFacturas[i, 0]) && string.IsNullOrEmpty(acumuladoFacturas[i, 6]))
                {
                    indice = i;
                    break;
                }
            }

            //Agregar elementos
            int itemsAgregar = facturaCompra.GetLength(0) - 1;

            for (int i = 0; i < itemsAgregar; i++)
            {
                for (int j = 0; j < acumuladoFacturas.GetLength(1); j++)
                {
                    acumuladoFacturas[indice, j] = facturaCompra[i+1, j];
                }

                if (indice >= acumuladoFacturas.GetLength(0)-1)
                {
                    indice = acumuladoFacturas.GetLength(0)-1;
                }
                else
                {
                    indice++;
                }
                
                acumuladoFacturas[indice, 6] = facturaCompra[i+1, 6];
                
            }

            return acumuladoFacturas;
        }

        /// <summary>
        ///     Arma la factura para imprimir en pantalla y almacenar en matriz acumuladoFacturas
        /// </summary>
        /// <param name="numVenta">Número de la venta actual</param>
        /// <returns>Matriz facturaCompra armada</returns>
        public static string[,] ArmadoFactura(string numVenta)
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

        /// <summary>
        ///     Imprime la matriz en pantalla
        /// </summary>
        /// <param name="facturaCompra">Matriz a imprimir</param>
        public static void ImprimirFactura(string[,] facturaCompra)
        {
            //Títulos
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;

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
            
        }

        /// <summary>
        ///     Resetea los valores de una matriz
        /// </summary>
        /// <param name="auxMatriz">Matriz a resetear</param>
        /// <returns>Matriz reseteada</returns>
        public static string[,] VaciarTemporales(string[,] auxMatriz)
        {           
            if (auxMatriz.GetLength(1) == 7)
            {
                string[,] newArray = new string[1, 7];
                auxMatriz = newArray;
                auxMatriz[0, 0] = "Vendedor";
                auxMatriz[0, 1] = "Factura N°";
                auxMatriz[0, 2] = "ID Producto";
                auxMatriz[0, 3] = "Nombre";
                auxMatriz[0, 4] = "Cantidad";
                auxMatriz[0, 5] = "Precio";
                auxMatriz[0, 6] = "Total";
            }
            else
            {
                string[,] newArray = new string[1, 6];
                auxMatriz = newArray;
                auxMatriz[0, 0] = "Vendedor";
                auxMatriz[0, 1] = "N° Venta";
                auxMatriz[0, 2] = "ID Producto";
                auxMatriz[0, 3] = "Nombre";
                auxMatriz[0, 4] = "Cantidad";
                auxMatriz[0, 5] = "Precio";
            }            

            return auxMatriz;
        }

        /// <summary>
        ///     Recorre la matriz acumuladoFacturas para obtener el número de la venta actual
        /// </summary>
        /// <returns>Número de venta actual</returns>
        static int CalcularVenta()
        {
            int numVenta = 0;

            for (int i = 1; i < acumuladoFacturas.GetLength(0); i++)
            {
                if (acumuladoFacturas[i, 1] != numVenta.ToString() && !string.IsNullOrEmpty(acumuladoFacturas[i, 6]))
                {
                    int.TryParse(acumuladoFacturas[i - 1, 1], out int aux);
                    numVenta = aux + 1;
                }
            }

            return numVenta;
        }
    }
}
