using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    public static class Stock
    {
        //Matrices y arrays iniciales

        public static string[,] libros = new string[11, 4]
        {
                {"ID",  "Titulo",   "Autor",    "Género" },         
                {"1",   "Título1",   "Autor1",    "Género1" },
                {"2",   "Título2",   "Autor2",    "Género2" },
                {"3",   "Título3",   "Autor3",    "Género3" },
                {"4",   "Título4",   "Autor4",    "Género4" },
                {"5",   "Título5",   "Autor5",    "Género5" },
                {"6",   "Título6",   "Autor6",    "Género6" },
                {"7",   "Título7",   "Autor7",    "Género7" },
                {"8",   "Título8",   "Autor8",    "Género8" },
                {"9",   "Título9",   "Autor9",    "Género9" },
                {"10",  "Título10",   "Autor10",    "Género10" }
        };

        public static float[,] precioCantidad = new float[10, 3]
        {
                //ID //Cantidad  //Precio
                {1,     0,          0},
                {2,     0,          0},
                {3,     0,          0},
                {4,     0,          0},
                {5,     0,          0},
                {6,     0,          0},
                {7,     0,          0},
                {8,     0,          0},
                {9,     0,          0},
                {10,    0,          0}
        };
    }
}
