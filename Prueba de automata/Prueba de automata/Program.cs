using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_automata
{
    class Program
    {
        static void Main(string[] args)
        {
            bool repetir = true;
            do
            {
                ClasePrincipal principal = new ClasePrincipal();
                Console.WriteLine("=== PRUEBA DE AFN CON TRANSICIONES VACÍAS ===\n");

                // Ingreso de estados
                bool estadoOK = false;
                do
                {
                    Console.Write("Ingrese los nombres de los estados separados por coma (ej: q0,q1,q2): ");
                    string est = Console.ReadLine();
                    estadoOK = principal.CapturarEstados(est);
                }
                while (!estadoOK);

                // Ingreso del estado inicial
                bool estadoinicalOK = false;
                do
                {
                    Console.Write("Ingrese el nombre del estado inicial: ");
                    string est = Console.ReadLine();
                    estadoinicalOK = principal.CapturarEstadoInicial(est);
                }
                while (!estadoinicalOK);

                // Ingreso de estados finales
                bool estadoFinalOK = false;
                do
                {
                    Console.Write("Ingrese los nombres de los estados finales separados por coma (ej: q2,q3): ");
                    string est = Console.ReadLine();
                    estadoFinalOK = principal.CapturarEstadosFinales(est);
                }
                while (!estadoFinalOK);


                // Ingreso del alfabeto 
                bool alfabetoOK = false;
                do
                {
                    Console.Write("Ingrese los símbolos del alfabeto separados por coma (ej: 0,1): ");
                    string est = Console.ReadLine();
                    alfabetoOK = principal.CapturarAlfabeto(est);
                }
                while (!alfabetoOK);

                // Definición de la función de transición δ (incluyendo transiciones ε)
                Console.WriteLine("\n--- Definición de la función de transición δ(q, símbolo) = q' ---");
                Console.WriteLine("Para transiciones vacías (ß), use 'ß' como símbolo(alt+225)");
                Console.WriteLine("Para múltiples estados destino (AFN), sepárelos por '|' (ej: q0|q1)");
                Console.WriteLine("Deje vacío si no hay transición para ese símbolo");
                List<string> errores = principal.EstableceTrancisiones();

                if (errores.Count > 0)
                {
                    Console.WriteLine("\n⚠️ Errores encontrados:");
                    foreach (string error in errores)
                    {
                        Console.WriteLine($"- {error}");
                    }
                }
                else
                {
                    Console.WriteLine("\n✅ Transiciones definidas correctamente.");
                }


                bool otraPalabra = true;
                do
                {
                    // Ingreso de la cadena a evaluar
                    Console.Write("\nIngrese la cadena a evaluar: ");
                    string cadena = Console.ReadLine().Trim();
                    // Recorrido desde el estado inicial
                    Console.WriteLine("\n=== Resultado del recorrido ===");
                    char resultado = principal.EmpezarRecorrido(cadena);
                    foreach (var estado in principal.obtenerEstados())
                    {
                        foreach (string traza in estado.Trazas)
                        {
                            Console.WriteLine(traza);
                        }
                    }

                    switch (resultado)
                    {
                        case 'e':
                            Console.WriteLine("Error, debido a que aún no se ha definido un quíntuplo completo de un AFN paraevaluar la e.r");
                            break;
                        case 'p':
                            Console.WriteLine("la palabra de entrada contiene símbolos que no pertenecen al alfabeto usadopor el Autómata");
                            break;
                        case 's':
                            Console.WriteLine("palabra de entrada ha satisfecho la e.r definido por el quíntuplo");
                            break;
                        case 'n':
                            Console.WriteLine("palabra de entrada no cumplió la e.r");
                            break;
                    }
                    Console.WriteLine("Probar otra palabra? (s/n)");
                    string opcion = Console.ReadLine();
                    if (opcion == "n")
                    {
                        Console.WriteLine("Probar otro quintuplo? (s/n)");
                        string opcion2 = Console.ReadLine();
                        if (opcion2 == "s")
                        {
                            principal.nuevoQuintuplo();
                        }
                        else
                        {
                            repetir = false;
                        }
                        otraPalabra =false;

                    }

                } while (otraPalabra);
            

            }
            while (repetir);

            Console.ReadKey();
        }
    }

    
}
