using System;
using System.Text;

namespace SegundoParcial
{
    public static class Procedimientos
    {
        //PROCEDIMIENTOS DE MATRICES

        /// <summary>
        ///     Impresión de Matriz. Permite seleccionar desde qué columna empzar la impresión
        /// </summary>
        /// <param name="auxMatriz">Matriz a imprimir</param>
        public static void imprimirMatriz<T>(T[,] auxMatriz)
        {
            for (int i = 0; i < auxMatriz.GetLength(0); i++)
            {
                if (!string.IsNullOrEmpty(auxMatriz[i, 1].ToString()) || auxMatriz[i, 1].ToString() == "0")
                {
                    Console.Write("|");
                    for (int j = 0; j < auxMatriz.GetLength(1); j++)
                    {
                        if (!string.IsNullOrEmpty(auxMatriz[i, j].ToString()) || auxMatriz[i, j].ToString() == "0")
                        {
                            Console.Write($"{auxMatriz[i, j],-25}|");
                        }
                    }
                    Console.WriteLine("");
                }
            }

        }

        /// <summary>
        ///     Método que permite imprimir fila específica de la matriz para crear un menú de opciones.
        /// </summary>
        /// <param name="auxMatriz">Matiz a imprimir</param>
        /// <param name="numFila">Indice de la fila que se quiere imprimir</param>
        public static int menuOpcionesMatriz<T>(T[,] auxMatriz, int numFila)
        {
            int cantidadImpresa = 0;

            for (int i = 1; i < auxMatriz.GetLength(1); i++)
            {
                if (!string.IsNullOrEmpty(auxMatriz[numFila, i].ToString()) || auxMatriz[numFila, i].ToString() != "0")
                {
                    cantidadImpresa++;
                    Console.WriteLine($"{i}. {auxMatriz[numFila, i]}");                    
                }
            }

            return cantidadImpresa;
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
            int columnasOriginal = matrizOriginal.GetLength(1);
            string [,] auxOriginal = new string[1, columnasOriginal];
            string[,] elemAgregar = new string [1, columnasOriginal];
            int id = matrizOriginal.GetLength(0);
            int datos = 1;

            //Carga de datos en Matriz

            elemAgregar[0, 0] = id.ToString();
            auxOriginal[0, 0] = matrizOriginal[0, 0].ToString();
            while (datos < columnasOriginal)
            {
                for (int i = 1; i < columnasOriginal; i++)
                {
                    auxOriginal[0, i] = matrizOriginal[0, i].ToString();
                    Console.Write($"{matrizOriginal[0,i]}: ");
                    string dato = Console.ReadLine();
                    elemAgregar[0, i] = dato;
                    datos++;
                }

            }

            //Validación de datos cargados

            elemAgregar = Validaciones.validarCargaDatos(elemAgregar, auxOriginal);

            return elemAgregar; 
        }

        /// <summary>
        ///     Carga de datos de matriz auxiliar en matriz original
        /// </summary>
        /// <param name="matrizOriginal">Matriz que va a recibir los datos</param>
        /// <param name="elemAgregar">Matriz auxiliar con los datos a cargar</param>
        public static string[,] agregarElementoMatrizStr(string[,] matrizOriginal, string[,] elemAgregar)
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
                for (int i = 0; i < elemAgregar.GetLength(1); i++)
                {
                    matrizOriginal[indice-1, i] = elemAgregar[0, i];
                }
            }
            else
            {
                for (int i = 0; i < matrizOriginal.GetLength(1); i++)
                {
                    matrizOriginal[indice-1, i] = elemAgregar[0, i];
                }
                    
            }

            return matrizOriginal;
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

        public static string centrarTexto(string texto)
        {
            Console.SetCursorPosition((Console.WindowWidth - texto.Length) / 2, Console.CursorTop);

            return texto;

        }
    }
}
