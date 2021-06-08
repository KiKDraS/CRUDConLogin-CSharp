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

        public static string[,] libros = new string[10, 4]
        {
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" },
                {"Título", "Autor", "Género", "Resumen" }
        };

        public static float[,] precioCantidad = new float[10, 2]
        {
                {0,0},
                {0,0},
                {0,0},
                {0,0},
                {0,0},
                {0,0},
                {0,0},
                {0,0},
                {0,0},
                {0,0}
        };
    }
}
