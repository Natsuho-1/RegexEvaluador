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
            Console.WriteLine("=== PRUEBA DE AFN CON TRANSICIONES VACÍAS ===\n");

            // Ingreso de estados
            Console.Write("Ingrese los nombres de los estados separados por coma (ej: q0,q1,q2): ");
            string[] nombresEstados = Console.ReadLine().Split(',');

            // Crear diccionario de estados
            Dictionary<string, Estado> estados = new Dictionary<string, Estado>();

            // Ingreso del estado inicial
            Console.Write("Ingrese el nombre del estado inicial: ");
            string nombreInicial = Console.ReadLine().Trim();

            // Ingreso de estados finales
            Console.Write("Ingrese los nombres de los estados finales separados por coma (ej: q2,q3): ");
            string[] nombresFinales = Console.ReadLine().Split(',');

            // Crear los estados
            foreach (string nombre in nombresEstados)
            {
                bool esFinal = Array.Exists(nombresFinales, f => f.Trim() == nombre.Trim());
                estados[nombre.Trim()] = new Estado(esFinal, nombre.Trim());
            }

            // Ingreso del alfabeto (incluyendo ε)
            Console.Write("Ingrese los símbolos del alfabeto separados por coma (ej: 0,1): ");
            char[] alfabeto = Array.ConvertAll(Console.ReadLine().Split(','), s => s.Trim()[0]);

            // Definición de la función de transición δ (incluyendo transiciones ε)
            Console.WriteLine("\n--- Definición de la función de transición δ(q, símbolo) = q' ---");
            Console.WriteLine("Para transiciones vacías (ε), use 'ε' como símbolo");
            Console.WriteLine("Para múltiples estados destino (AFN), sepárelos por '|' (ej: q0|q1)");
            Console.WriteLine("Deje vacío si no hay transición para ese símbolo");

            foreach (var estado in estados.Values)
            {
                Dictionary<char, List<Estado>> transiciones = new Dictionary<char, List<Estado>>();

                // Agregar ε como símbolo posible
                List<char> simbolos = new List<char>(alfabeto);
                simbolos.Add('ε');

                foreach (char simbolo in simbolos)
                {
                    Console.Write($"δ({estado.Name}, {simbolo}) = ");
                    string destinoInput = Console.ReadLine().Trim();

                    if (!string.IsNullOrEmpty(destinoInput))
                    {
                        string[] destinos = destinoInput.Split('|');
                        List<Estado> estadosDestino = new List<Estado>();

                        foreach (string destino in destinos)
                        {
                            string destinoTrim = destino.Trim();
                            if (estados.ContainsKey(destinoTrim))
                            {
                                estadosDestino.Add(estados[destinoTrim]);
                            }
                            else
                            {
                                Console.WriteLine($"El estado '{destinoTrim}' no existe. Se omitirá.");
                            }
                        }
                        transiciones[simbolo] = estadosDestino;
                    }
                    else
                    {
                        // No hay transición para este símbolo
                        transiciones[simbolo] = new List<Estado>();
                    }
                }
                estado.setTransiciones(transiciones);
            }

            // Ingreso de la cadena a evaluar
            Console.Write("\nIngrese la cadena a evaluar: ");
            string cadena = Console.ReadLine().Trim();

            // Recorrido desde el estado inicial
            Console.WriteLine("\n=== Resultado del recorrido ===");
            Estado estadoInicial = estados[nombreInicial];

            bool aceptada = estadoInicial.evaluarAFN(cadena, estados);

            Console.WriteLine($"\nResultado: {(aceptada ? "Cadena aceptada" : "Cadena rechazada")}");

            Console.ReadKey();
        }
    }

    
}
