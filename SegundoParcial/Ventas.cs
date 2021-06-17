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

        public static string[,] facturaCompra = new string[1, 6]
        {
            {"Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"}
        };

        public static string[,] acumuladoFacturas = new string[1, 6]
        {
            {"Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"}
        };

        /// <summary>
        ///     Crea un carrito de ventas
        /// </summary>
        /// <param name="usuario">Usuario realizando la venta</param>
        /// <param name="idLibro">ID del libro a agregar al carro</param>
        /// <param name="numVenta">Numero de ventas realizadas</param>
        /// <returns>matrizCarro actualizada</returns>
        public static string[,] CarroVenta(string usuario, int idLibro, int numVenta)
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

            return matrizCarro;
        }


        /// <summary>
        ///     Vrea la factura de la venta
        /// </summary>
        /// <param name="usuario">Usuario encargado de la venta</param>
        /// <param name="numVenta">Número de ventas realizadas</param>
        /// <returns>Matriz facturas actualizada</returns>
        public static string[,] Factura(string usuario, int numVenta)
        {
            //Asignar tamaño de Matriz
            int filas = matrizCarro.GetLength(0) + 1;
            Procedimientos.ResizeArray<string>(ref facturaCompra, filas, 6);

            //Armado de factura
            facturaCompra = ArmadoFactura(facturaCompra, numVenta);

            return facturaCompra;
        }

        public static string[,] ArmadoFactura(string[,] facturaCompra, int numVenta)
        {
            //Carga N° Factura
            facturaCompra[1, 0] = numVenta.ToString();
            int totalItemsCarro = matrizCarro.GetLength(0);

            //Carga ID Producto
            for (int i = 2; i < totalItemsCarro; i++)
            {
                facturaCompra[1,1] = matrizCarro[1, 2];
                if (!string.IsNullOrEmpty(facturaCompra[i,1]) && facturaCompra[1, 1] != matrizCarro[i, 2])
                {
                    int aux = totalItemsCarro - i;
                    facturaCompra[aux, 1] = matrizCarro[i, 2];
                }
            }

            //Carga Nombre del Producto
            for (int i = 2; i < totalItemsCarro; i++)
            {
                facturaCompra[1, 2] = matrizCarro[1, 3];
                if (!string.IsNullOrEmpty(facturaCompra[i, 2]) && facturaCompra[1, 2] != matrizCarro[i, 3])
                {
                    int aux = totalItemsCarro - i;
                    facturaCompra[aux, 2] = matrizCarro[i, 3];
                }
            }

            //Carga Cantidades
            string auxID = facturaCompra[1, 1];
            int.TryParse(matrizCarro[1, 4], out int totalCantidades);

            for (int i = 1; i < totalItemsCarro; i++)
            {
                facturaCompra[1, 3] = totalCantidades.ToString();

                if (facturaCompra[i, 1] != auxID)
                {
                    int.TryParse(matrizCarro[i, 4], out int aux);
                    totalCantidades += aux;
                    int auxInd = totalItemsCarro - 1;
                    facturaCompra[auxInd, 3] = totalCantidades.ToString();

                }            
            }

            //Carga de precios
            for (int i = 1; i < totalItemsCarro; i++)
            {
                facturaCompra[1, 4] = matrizCarro[1,5];

                if (facturaCompra[i, 1] != auxID)
                {
                    int aux = totalItemsCarro - 1;
                    facturaCompra[aux, 4] = matrizCarro[i,5];

                }
            }

            //Suma de totales
            float totalVenta = 0;
            for (int i = 0; i < totalItemsCarro; i++)
            {
                if (matrizCarro[i, 1] == numVenta.ToString())
                {
                    float.TryParse(matrizCarro[i, 5], out float auxFloat);
                    totalVenta += auxFloat;
                }
            }

            facturaCompra[totalItemsCarro - 1, 5] = totalVenta.ToString();


            return facturaCompra;
        }
    }
}
