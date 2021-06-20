using System;
using System.Text;

namespace SegundoParcial
{
    public static class Procedimientos
    {
        #region Procedimientos de Matriz

            /// <summary>
            ///     Impresión de Matriz
            /// </summary>
            /// <param name="auxMatriz">Matriz a imprimir</param>
            public static void ImprimirMatriz<T>(T[,] auxMatriz)
            {
                //Títulos
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Black;

                for (int i = 0; i < 1; i++)
                {
                    if (!string.IsNullOrEmpty(auxMatriz[i, 1].ToString()))
                    {
                        Console.Write("|");
                        for (int j = 0; j < auxMatriz.GetLength(1); j++)
                        {
                            if (!string.IsNullOrEmpty(auxMatriz[i, j].ToString()))
                            {
                                Console.Write($"{auxMatriz[i, j],-15}|");
                            }
                        }
                        Console.WriteLine("");
                    }
                }
                Console.ResetColor();

                //Items
                for (int i = 1; i < auxMatriz.GetLength(0); i++)
                {
                    if (!string.IsNullOrEmpty(auxMatriz[i, 1].ToString()) || auxMatriz[i, 1].ToString() == "0")
                    {
                        Console.Write("|");
                        for (int j = 0; j < auxMatriz.GetLength(1); j++)
                        {
                            if (!string.IsNullOrEmpty(auxMatriz[i, j].ToString()) || auxMatriz[i, j].ToString() == "0")
                            {                               
                                Console.Write($"{auxMatriz[i, j], -15}|");
                            }
                        }
                        Console.WriteLine("");
                    }
                }

            }

            /// <summary>
            ///     Método para crear un menú de opciones a partir de la fila de una matriz.
            /// </summary>
            /// <param name="auxMatriz">Matiz a imprimir</param>
            /// <param name="numFila">Indice de la fila que se quiere imprimir</param>
            public static int MenuOpcionesMatriz<T>(T[,] auxMatriz, int numFila)
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
          
            /// <summary>
            ///     Método para agrandar matriz
            /// </summary>
            /// <param name="original">Matriz a agrandar</param>
            /// <param name="newCoNum">Nuevo tamaño de columnas</param>
            /// <param name="newRoNum">Nuevo tamaño de filas</param>
            public static void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
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
            ///     Busca el indice en la matriz del elemento por ID
            /// </summary>
            /// <param name="auxMatriz">Matriz en la que se buscará el elemento</param>
            /// <param name="id">ID del elemento a buscar</param>
            /// <returns>Indice en matriz</returns>
            public static int EncontrarIndice<T>(T[,]auxMatriz, string id)
            {
                int indice = -1;

                for (int i = 0; i < auxMatriz.GetLength(0); i++)
                {
                    if (auxMatriz[i, 0].ToString() == id)
                    {
                        indice = i;
                        break;
                    }
                }

                return indice;
            }            

            #region CRUD

                /// <summary>
                ///     Método para armar una matriz auxiliar que permita cargar nuevos datos en otra matriz
                /// </summary>
                /// <param name="matrizOriginal">Matriz de referencia para la creación de la matriz auxiliar</param>
                /// <returns>Matriz auxiliar con los datos para cargar en otra matriz</returns>
                public static string[,] ArmarMatrizDatosNuevos<T>(T[,] matrizOriginal)
                {
                    int columnasOriginal = matrizOriginal.GetLength(1);
                    string[,] auxOriginal = new string[1, columnasOriginal];
                    string[,] elemAgregar = new string[1, columnasOriginal];
                    int id = -1; 
                    int datos = 1;

                    //Creación de ID

                    for (int i = 0; i < matrizOriginal.GetLength(0); i++)
                    {
                        if (string.IsNullOrEmpty(matrizOriginal[i, 0].ToString()) || matrizOriginal[i, 0].ToString() == "0")
                        {
                            id = i;
                            break;
                        }
                        else
                        {
                            id = matrizOriginal.GetLength(0);
                        }
                    }

                    //Carga de datos en Matriz

                    elemAgregar[0, 0] = id.ToString();
                    auxOriginal[0, 0] = matrizOriginal[0, 0].ToString();
                    while (datos < columnasOriginal)
                    {
                        for (int i = 1; i < columnasOriginal; i++)
                        {                            
                            auxOriginal[0, i] = matrizOriginal[0, i].ToString();
                            Console.Write($"{matrizOriginal[0, i]}: ");                            
                            string dato = Console.ReadLine();
                            if (auxOriginal[0, i] == "Cantidad" || auxOriginal[0, i] == "Precio")
                            {
                                float datoFloat = Validaciones.ValidarFloat(dato);
                                dato = datoFloat.ToString();
                            }
                            elemAgregar[0, i] = dato;
                            datos++;
                        }

                    }

                    //Validación de datos cargados

                    elemAgregar = Validaciones.ValidarCargaDatos(elemAgregar, auxOriginal);

                    return elemAgregar;
                }

                /// <summary>
                ///     Carga de datos de matriz auxiliar en matriz de string original
                /// </summary>
                /// <param name="matrizOriginal">Matriz que va a recibir los datos</param>
                /// <param name="elemAgregar">Matriz auxiliar con los datos a cargar</param>
                /// <returns>Matriz modificada</returns>
                public static string[,] AgregarElementoMatrizStr(string[,] matrizOriginal, string[,] elemAgregar)
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
                                    matrizOriginal[indice - 1, i] = elemAgregar[0, i];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < matrizOriginal.GetLength(1); i++)
                                {
                                    matrizOriginal[indice, i] = elemAgregar[0, i];
                                }

                            }

                            return matrizOriginal;
                        }

                /// <summary>
                ///     Carga de datos de matriz auxiliar en matriz de float original
                /// </summary>
                /// <param name="matrizOriginal">Matriz que va a recibir los datos</param>
                /// <param name="elemAgregar">Matriz auxiliar con los datos a cargar</param>
                /// <returns>Matriz modificada</returns>
                public static float[,] AgregarElementoMatrizFloat(float[,] matrizOriginal, string[,] elemAgregar)
                                {
                                    int indice = -1;

                                    //Buscar espacios vacíos en la Matriz

                                    for (int i = 0; i < matrizOriginal.GetLength(0); i++)
                                    {
                                        if (matrizOriginal[i, 1] == 0)
                                        {
                                            indice = i;
                                            break;
                                        }
                                    }

                                    //Cargar datos nuevos

                                    if (indice == -1)
                                    {
                                        indice = matrizOriginal.GetLength(0) + 1;
                                        ResizeArray<float>(ref matrizOriginal, indice, 3);
                                        for (int i = 0; i < elemAgregar.GetLength(1); i++)
                                        {
                                            float dato;
                                            float.TryParse(elemAgregar[0, i], out dato);
                                            matrizOriginal[indice - 1, i] = dato;
                                        }
                                    }
                                    else
                                    {
                                        float dato;
                                        float.TryParse(elemAgregar[0, 0], out dato);
                                        matrizOriginal[indice, 0] = dato;
                                        for (int i = 1; i < matrizOriginal.GetLength(1); i++)
                                        {                            
                                            float.TryParse(elemAgregar[0, i+3], out dato);
                                            matrizOriginal[indice, i] = dato;
                                        }

                                    }

                                    return matrizOriginal;

                                }


            #endregion

        #endregion

        #region Procedimientos de Array

            /// <summary>
            ///     Impresión de Array. Pasar null en el segundo parámetro si no es necesario evaluar usuario logueado
            /// </summary>
            /// <param name="auxArray">Array a imprimir</param>
            /// <param name="usuario">Nombre del usuario logueado actualmente</param>
            /// <returns>Cantidad de opciones impresas</returns>
            public static int ImprimirArray<T>(T[] auxArray, string usuario)
            {
                int cantidadImpresa = 0;

                if (usuario != null)
                {
                    if (usuario == "Admin")
                    {
                        for (int i = 0; i < auxArray.Length; i++)
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
                        for (int i = 0; i < auxArray.Length; i++)
                        {
                            if (auxArray[i].ToString() == "Ventas" || auxArray[i].ToString() == "Facturación del día")
                            {
                                cantidadImpresa++;
                                Console.WriteLine($"{cantidadImpresa}. {auxArray[i]}");
                            }
                        }
                        cantidadImpresa++;
                        Console.WriteLine($"{cantidadImpresa}. Salir");
                    }
                }

                return cantidadImpresa;
            }

        #endregion

    }
}
