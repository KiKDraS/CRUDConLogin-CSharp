using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Procedimientos
    {
        //PROCEDIMIENTOS DE MATRICES

        /// <summary>
        ///     Impresión de Matriz
        /// </summary>
        /// <param name="auxMatriz">Matriz a imprimir</param>
        public static void imprimirMatriz<T>(T[,] auxMatriz)
        {
            int numColumna = 1;

            for (int i = 0; i < auxMatriz.GetLength(0); i++)
            {
                Console.WriteLine("-------------------------");
                Console.Write("|");
                for (int j = 0; j < auxMatriz.GetLength(numColumna); j++)
                {
                    if (!string.IsNullOrEmpty(auxMatriz[i, j].ToString()) || auxMatriz[i, j].ToString() != "0")
                    {
                        Console.WriteLine($"{auxMatriz[i, j], -25}|");
                    }

                }
                Console.WriteLine("-------------------------");
                numColumna++;
            }
        }

        /// <summary>
        ///     Método que permite imprimir una columna o fila específica de la matriz.
        /// </summary>
        /// <param name="auxMatriz">Matiz a imprimir</param>
        /// <param name="numFila">Indice de la fila que se quiere imprimir</param>
        /// <param name="numColum">Indice de la columna que se quiere imprimir</param>
        public static void imprirFilaColumMatriz<T>(T[,] auxMatriz, int numFila, int numColum)
        {
            for (int i = 0; i < auxMatriz.GetLength(numFila); i++)
            {
                Console.WriteLine("-------------------------");
                Console.Write("|");
                for (int j = 0; j < auxMatriz.GetLength(numColum); j++)
                {
                    if (!string.IsNullOrEmpty(auxMatriz[i, j].ToString()) || auxMatriz[i, j].ToString() != "0")
                    {
                        Console.WriteLine($"{auxMatriz[i, j],-25}|");
                    }

                }
                Console.WriteLine("-------------------------");
            }
        }


        //PROCEDIMIENTOS DE ARRAYS

        /// <summary>
        ///     Impresión de Array. Pasar null en el segundo parámetro si no es necesario evaluar usuario logueado
        /// </summary>
        /// <param name="auxArray">Array a imprimir</param>
        /// <param name="usuario">Nombre del usuario logueado actualmente</param>
        /// <returns>Cantidad de opciones impresas</returns>
        public static int imprimirArray<T>(T[] auxArray, string usuario)
        {
            int cantidadImpresa = 0;

            if(usuario != null)
            {
                if (usuario == "Admin")
                {
                    for (int i = 0; i < auxArray.GetLength(0); i++)
                    {
                        if (!string.IsNullOrEmpty(auxArray[i].ToString()) || auxArray[i].ToString() != "0")
                        {
                            cantidadImpresa++;
                            Console.WriteLine($"{cantidadImpresa}. {auxArray[i]}");
                        }
                    }
                    cantidadImpresa++;
                    Console.WriteLine($"{cantidadImpresa}. Salir");
                }
                else
                {
                    cantidadImpresa++;
                    Console.WriteLine($"{cantidadImpresa}. Vender");
                    cantidadImpresa++;
                    Console.WriteLine($"{cantidadImpresa}. Salir");
                }
            }                     

            return cantidadImpresa;
        }


        //CRUD - Matriz

        /// <summary>
        ///     Método para armar una matriz auxiliar que permita cargar nuevos datos en otra matriz
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrizOriginal">Matriz de referencia para la creación de la matriz auxiliar</param>
        /// <returns>Matriz auxiliar con los datos para cargar en otra matriz</returns>
        public static string[,] armarMatrizDatosNuevos<T>(T[,] matrizOriginal)
        {
            int filasOriginal = matrizOriginal.GetLength(0);
            int columnasOriginal = matrizOriginal.GetLength(1);
            string[,] elemAgregar = new string [1, columnasOriginal];
            string[,] auxOriginal = new string[1, columnasOriginal];
            int id = -1;
            int datos = 0;
            bool continuar;

            for (int i = 0; i < filasOriginal; i++)
            {
                if (string.IsNullOrEmpty(matrizOriginal[i, 0].ToString()) || matrizOriginal[i, 0].ToString() == "0")
                {
                    id = i;
                }
            }
            
            if(id == -1)
            {
                id = filasOriginal + 1;
            }

            //Carga de datos en Matriz

            elemAgregar[1, 0] = id.ToString();
            while (datos < columnasOriginal)
            {
                for (int i = 1; i < columnasOriginal; i++)
                {
                    auxOriginal[0, i] = matrizOriginal[0, i].ToString();
                    Console.Write($"Ingrese {matrizOriginal[1,i]}");
                    elemAgregar[0, i] = armarString();
                    datos++;
                }

            }

            //Validación de datos cargados

            do
            {
                continuar = Validaciones.validarCargaDatos(elemAgregar, auxOriginal);

            } while (continuar);

            return elemAgregar; 
        }

        /// <summary>
        ///     Carga de datos de matriz auxiliar en matriz original
        /// </summary>
        /// <param name="matrizOriginal">Matriz que va a recibir los datos</param>
        /// <param name="elemAgregar">Matriz auxiliar con los datos a cargar</param>
        public static void agregarElementoMatrizStr(string[,] matrizOriginal, string[,] elemAgregar)
        {
            int indice = -1;

            //Buscar espacios vacíos en la Matriz

            for (int i = 0; i < matrizOriginal.GetLength(0); i++)
            {
                if (string.IsNullOrEmpty(matrizOriginal[i, 1].ToString()))
                {
                    indice = i;
                    break;
                }
            }

            //Cargar datos nuevos

            if (indice == -1)
            {
                indice = matrizOriginal.GetLength(0) + 1;
                ResizeArray<string>(ref matrizOriginal, indice, 3);
                for (int i = 0; i < elemAgregar.GetLength(0); i++)
                {
                    matrizOriginal[indice, i] = elemAgregar[0, i];
                }
            }
            else
            {
                for (int i = 0; i < elemAgregar.GetLength(0); i++)
                {
                    matrizOriginal[indice, i] = elemAgregar[0, i];
                }
                    
            }
        }



        //HELPERS

        /// <summary>
        ///     Método para agrandar matriz
        /// </summary>
        /// <param name="original">Matriz a agrandar</param>
        /// <param name="newCoNum">Nuevo tamaño de columnas</param>
        /// <param name="newRoNum">Nuevo tamaño de filas</param>
        static void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
        {
            var newArray = new T[newCoNum, newRoNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRoNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }

        /// <summary>
        ///     Método para armar un string utilizando StringBuilder
        /// </summary>
        /// <returns>String creado</returns>
        public static string armarString()
        {
            StringBuilder dato = new StringBuilder();

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && dato.Length > 0)
                {
                    dato.Remove(dato.Length - 1, 1);
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    dato.Append(key.KeyChar);
                }
            }

            return dato.ToString();
        }
    }
}
