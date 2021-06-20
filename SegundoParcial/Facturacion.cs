using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    class Facturacion
    {
        public static string[,] acumuladoFacturas = new string[12, 7]
        {
            {"Vendedor", "Factura N°", "ID Producto", "Nombre", "Cantidad", "Precio", "Total"},
            {"Vendedor1", "1", "3", "Juego Tronos", "1", "200", ""},
            {"Vendedor1", "1", "5", "El fact Scarp", "1", "130", ""},
            {"", "", "", "", "", "", "330"},
            {"Vendedor1", "2", "1", "Elantris", "1", "150", ""},
            {"", "", "", "", "", "", "150"},
            {"Vendedor2", "3", "7", "La perf silen", "1", "160", ""},
            {"", "", "", "", "", "", "160"},
            {"Vendedor2", "4", "7", "La perf silen", "1", "160", ""},
            {"", "", "", "", "", "", "160"},
            {"Admin", "5", "3", "Juego Tronos", "1", "200", ""},
            {"", "", "", "", "", "", "200"}
        };

        public static bool TotalFacturado()
        {
            int cantidadVentas = 0;
            int totalRecaudado = 0;

            for (int i = 0; i < acumuladoFacturas.GetLength(0); i++)
            {
                if (string.IsNullOrEmpty(acumuladoFacturas[i,0]))
                {
                    cantidadVentas++;
                    int.TryParse(acumuladoFacturas[i, 6], out int aux);
                    totalRecaudado += aux;
                }
            }

            Console.WriteLine($"\nCantidad de ventas realizadas: {cantidadVentas}");
            Console.WriteLine($"Total recaudado: ${totalRecaudado}");
            Console.WriteLine("\n\n\nPresione cualquier tecla para salir");
            Console.ReadKey();
            bool exit = false;

            return exit;
        }

        public static bool FacturacionIndividual()
        {
            Console.Write("Ingrese nombre de vendedor a buscar: ");
            string vendedor = Console.ReadLine();

            //Almacenar indices de elementos de factura
            Console.WriteLine("");
            int[] arrayIndices = AlmacenarIndices(vendedor, 0);
            while (arrayIndices[0] == 0)
            {
                Console.WriteLine("Vendedor no encontrado");
                Console.Write("Ingrese nombre de vendedor a buscar: ");
                vendedor = Console.ReadLine();
                Console.WriteLine("");
                arrayIndices = AlmacenarIndices(vendedor, 0);
            }

            //Impresión de factura
            ImprimirFacturaEncontrada(arrayIndices);

            //Vaciar temporales
            VaciarTemporales(arrayIndices);

            Console.WriteLine("\n\nPresione una tecla para salir");
            Console.ReadKey();
            bool exit = false;

            return exit;
        }

        public static bool BuscarFactura()
        {
            Console.Write("Ingrese número de factura a buscar: ");
            string numFactura = Console.ReadLine();            

            //Almacenar indices de elementos de factura
            Console.WriteLine("");
            int[] arrayIndices = AlmacenarIndices(numFactura, 1);
            while (arrayIndices[0] == 0)
            {
                Console.WriteLine("Factura no encontrada");
                Console.Write("Ingrese número de factura a buscar: ");
                numFactura = Console.ReadLine();
                Console.WriteLine("");
                arrayIndices = AlmacenarIndices(numFactura, 1);
            }

            //Impresión de factura
            ImprimirFacturaEncontrada(arrayIndices);

            //Vaciar temporales
            VaciarTemporales(arrayIndices);

            Console.WriteLine("\n\nPresione una tecla para salir");
            Console.ReadKey();
            bool exit = false;

            return exit;

        }

        static void ImprimirFacturaEncontrada(int[] arrayIndices)
        {
            //Títulos
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < 1; i++)
            {
                if (!string.IsNullOrEmpty(acumuladoFacturas[i, 1]))
                {
                    Console.Write("|");
                    for (int j = 0; j < acumuladoFacturas.GetLength(1); j++)
                    {
                        if (!string.IsNullOrEmpty(acumuladoFacturas[i, j]))
                        {
                            Console.Write($"{acumuladoFacturas[i, j],-15}|");
                        }
                    }
                    Console.WriteLine("");
                }
            }

            Console.ResetColor();

            //Items
            int auxIndiceI; 
            for (int i = 0; i < arrayIndices.Length; i++)
            {
                auxIndiceI = arrayIndices[i];

                Console.Write("|");
                if (!string.IsNullOrEmpty(acumuladoFacturas[auxIndiceI, 1]))
                {
                    for (int j = 0; j < acumuladoFacturas.GetLength(1); j++)
                    {
                        if (!string.IsNullOrEmpty(acumuladoFacturas[auxIndiceI, j]))
                        {
                            Console.Write($"{acumuladoFacturas[auxIndiceI, j],-15}|");
                        }
                    }
                    Console.WriteLine("");
                }                
            }
            auxIndiceI = arrayIndices.Length - 1;
            auxIndiceI = arrayIndices[auxIndiceI];
            Console.Write($"                                                                                               |{acumuladoFacturas[auxIndiceI, 6]}");
        }

        static int[] VaciarTemporales(int[] auxArray)
        {
            int[] newArray = new int[1];
            auxArray = newArray;

            return auxArray;
        }

        static int[] AlmacenarIndices(string datoBuscar, int colum)
        {
            int[] arrayIndices = new int[1];

            for (int i = 0; i < acumuladoFacturas.GetLength(0); i++)
            {
                if (acumuladoFacturas[i, colum] == datoBuscar)
                {
                    for (int j = 0; j < arrayIndices.Length; j++)
                    {
                        if (arrayIndices[j] == 0)
                        {
                            arrayIndices[j] = i;
                        }
                    }
                    Array.Resize(ref arrayIndices, arrayIndices.Length + 1);
                }
            }

            if (arrayIndices[0] != 0)
            {
                //Agregar indice del total de la factura
                int auxIndice = arrayIndices.Length - 1;
                arrayIndices[auxIndice] = arrayIndices[auxIndice - 1] + 1;
            }            

            return arrayIndices;
        }
    }
}
