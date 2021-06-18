using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcial
{
    class Program
    {
        static void App()
        {
            bool exit = true;

            do
            {
                //Login
                //string usuario = Visualizacion.login();
                string usuario = "Admin";
                //string usuario = "Vendedor1";

                switch (usuario)
                {
                    case "Admin":
                        do
                        {
                            Console.Clear();
                            exit = Visualizacion.MenuUser("Admin", exit);

                        } while (exit);
                        break;

                    default:
                        do
                        {
                            Console.Clear();
                            exit = Visualizacion.MenuUser(usuario, exit);

                        } while (exit);
                        break;
                }

                exit = Validaciones.ValidarSalir("\nPresione ENTER para volver al login o ESC para salir de la aplicación", exit);

            } while (exit);
        }
        
        static void Main(string[] args)
        {
            //Título en consola
            Console.Title = "Librería on-line";

            App();

        }
    }
}
