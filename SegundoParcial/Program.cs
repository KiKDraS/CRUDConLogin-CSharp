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
                string usuario = Procedimientos.login();

                switch (usuario)
                {
                    case "Administrador":
                        do
                        {
                            Console.Clear();
                            Procedimientos.menuAdmin(usuario, exit);
                            exit = Validaciones.validarSalir("\nPresione ESC para salir", exit);

                        } while (exit);
                        break;

                    case "":
                        exit = Validaciones.validarSalir("\nPresione ESC para salir", exit);
                        break;

                    default:
                        do
                        {
                            Console.Clear();
                            Procedimientos.menuAdmin(usuario, exit);
                            exit = Validaciones.validarSalir("\nPresione ESC para salir", exit);

                        } while (exit);
                        break;
                }

                exit = Validaciones.validarSalir("Presione ENTER para volver al Login o ESC para salir del programa", exit);

            } while (exit);
        }
        
        static void Main(string[] args)
        {
            //Título en consola
            Console.Title = "Librería on-line";

            App();

            Console.ReadKey();
        }
    }
}
