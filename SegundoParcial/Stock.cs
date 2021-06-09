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

        public static string[,] libros = new string[10, 5]
        {
                {"1","Título", "Autor", "Género", "Resumen" },
                {"2","Título", "Autor", "Género", "Resumen" },
                {"3","Título", "Autor", "Género", "Resumen" },
                {"4","Título", "Autor", "Género", "Resumen" },
                {"5","Título", "Autor", "Género", "Resumen" },
                {"6","Título", "Autor", "Género", "Resumen" },
                {"7","Título", "Autor", "Género", "Resumen" },
                {"8","Título", "Autor", "Género", "Resumen" },
                {"9","Título", "Autor", "Género", "Resumen" },
                {"10","Título", "Autor", "Género", "Resumen" }
        };

        public static float[,] precioCantidad = new float[10, 3]
        {
                {1,0,0},
                {2,0,0},
                {3,0,0},
                {4,0,0},
                {5,0,0},
                {6,0,0},
                {7,0,0},
                {8,0,0},
                {9,0,0},
                {10,0,0}
        };
    }
}
