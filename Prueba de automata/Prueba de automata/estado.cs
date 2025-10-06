using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prueba_de_automata
{
    class Estado
    {
        public bool EsFinal { get; private set; }
        public string Name { get; private set; }
        private Dictionary<char, List<Estado>> Transiciones;

        public Estado(bool tipo, string name)
        {
            this.EsFinal = tipo;
            this.Name = name;
            this.Transiciones = new Dictionary<char, List<Estado>>();
        }

        public void setTransiciones(Dictionary<char, List<Estado>> transiciones)
        {
            this.Transiciones = transiciones;
        }

        // Función para obtener la ε-clausura de un estado
        private HashSet<Estado> obtenerEpsilonClausura()
        {
            HashSet<Estado> clausura = new HashSet<Estado>();
            Stack<Estado> pila = new Stack<Estado>();

            pila.Push(this);
            clausura.Add(this);

            while (pila.Count > 0)
            {
                Estado actual = pila.Pop();

                if (actual.Transiciones.ContainsKey('ß'))
                {
                    foreach (Estado siguiente in actual.Transiciones['ß'])
                    {
                        if (!clausura.Contains(siguiente))
                        {
                            clausura.Add(siguiente);
                            pila.Push(siguiente);
                        }
                    }
                }
            }

            return clausura;
        }

        // Función para obtener la ε-clausura de un conjunto de estados
        public static HashSet<Estado> obtenerEpsilonClausuraConjunto(HashSet<Estado> estados)
        {
            HashSet<Estado> clausura = new HashSet<Estado>();

            foreach (Estado estado in estados)
            {
                clausura.UnionWith(estado.obtenerEpsilonClausura());
            }

            return clausura;
        }

        // Función principal para evaluar AFN
        public bool evaluarAFN(string cadena, Dictionary<string, Estado> todosEstados)
        {
            // Estado actual como conjunto de estados (incluyendo ε-clausura)
            HashSet<Estado> estadosActuales = obtenerEpsilonClausuraConjunto(new HashSet<Estado> { this });

            Console.WriteLine($"Inicio: Estado actual = {{{string.Join(", ", estadosActuales.Select(e => e.Name))}}}");

            return evaluarAFNRecursivo(cadena, 0, estadosActuales);
        }

        private bool evaluarAFNRecursivo(string cadena, int posicion, HashSet<Estado> estadosActuales)
        {
            Console.WriteLine($"\nPosición {posicion}/{cadena.Length}, Estados actuales: {{{string.Join(", ", estadosActuales.Select(e => e.Name))}}}");

            // Si ya no ha caracteres en al cadena, verificar si algún estado actual es final
            if (posicion >= cadena.Length)
            {
                bool algunoEsFinal = estadosActuales.Any(estado => estado.EsFinal);
                Console.WriteLine($"Fin de cadena. Estados finales: {(algunoEsFinal ? "SÍ" : "NO")}");
                return algunoEsFinal;
            }

            char simbolo = cadena[posicion];
            Console.WriteLine($"Símbolo actual: '{simbolo}'");

            // Calcula los siguientes estados mediante transiciones con el símbolo actual
            HashSet<Estado> siguientesEstados = new HashSet<Estado>();

            foreach (Estado estadoActual in estadosActuales)
            {
                if (estadoActual.Transiciones.ContainsKey(simbolo))
                {
                    foreach (Estado siguiente in estadoActual.Transiciones[simbolo])
                    {
                        siguientesEstados.Add(siguiente);
                        Console.WriteLine($"  {estadoActual.Name} --'{simbolo}'--> {siguiente.Name}");
                    }
                }
            }

            if (siguientesEstados.Count == 0)
            {
                Console.WriteLine($"  No hay transiciones para '{simbolo}' desde los estados actuales");
                return false;
            }

            // Aplicar ε-clausura a los siguientes estados
            HashSet<Estado> siguientesConEpsilon = Estado.obtenerEpsilonClausuraConjunto(siguientesEstados);

            Console.WriteLine($"Estados después de ε-clausura: {{{string.Join(", ", siguientesConEpsilon.Select(e => e.Name))}}}");

            // Continua con el siguiente símbolo
            return evaluarAFNRecursivo(cadena, posicion + 1, siguientesConEpsilon);
        }

    }

}
