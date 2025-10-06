using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_de_automata
{
    class ClasePrincipal
    {
        public Quintuplo quintuplo { get; private set; }
        public bool quintuploOK { get; private set; }
        public Dictionary<string, Estado> estados { get; private set; }

        public ClasePrincipal()
        {
            this.quintuplo = new Quintuplo();
            this.estados= new Dictionary<string, Estado>();
        }
        // recibe los estados
        public bool CapturarEstados(string estad)
        {
            string[] estados= estad.Split(',');
            if (estados.Length < 2)
            {
                return false;
            }
            else
            {
                quintuplo.nombresEstados = estados;
                return true;
            }

            

        }
        // Captura estado inicial
        public bool CapturarEstadoInicial(string estad)
        {
            try
            {
                quintuplo.nombreInicial = estad.Trim();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Ingreso de estados finales
        public bool CapturarEstadosFinales(string estad)
        {
            string[] estados = estad.Split(',');
            if (estados.Length < 1)
            {
                return false;
            }
            else
            {
                quintuplo.nombresFinales= estados;
                CrearEstados();
                return true;
            }
        }

        // Captura alfabeto
        public bool CapturarAlfabeto(string alfabeto)
        {
            try
            {
                quintuplo.alfabeto = Array.ConvertAll(alfabeto.Split(','), s => s.Trim()[0]); //Convierte string a char
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CrearEstados()
        {
            // Crear los estados
            foreach (string nombre in  quintuplo.nombresEstados)
            {
                bool esFinal = Array.Exists(quintuplo.nombresFinales, f => f.Trim() == nombre.Trim());
                estados[nombre.Trim()] = new Estado(esFinal, nombre.Trim());
            }
        }

        public List<string> EstableceTrancisiones()
        {
            List<string> errores = new List<string>();
            try
            {
                foreach (var estado in estados.Values)
                {
                    Dictionary<char, List<Estado>> transiciones = new Dictionary<char, List<Estado>>();

                    // Agregar ß como símbolo posible
                    List<char> simbolos = new List<char>(quintuplo.alfabeto);
                    simbolos.Add('ß');

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
                                    errores.Add($"El estado '{destinoTrim}' no existe. Se omitirá.");
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
                quintuploOK = true;
            }
            catch
            {

            }
            return errores;
        }

        // Ejecuta el recorrido desde el estado inicial
        public char EmpezarRecorrido(string palabra)
        {
            Estado estadoInicial = estados[quintuplo.nombreInicial];
            for(int i=0;i==palabra.Length;i++)
            {
                if (!quintuplo.alfabeto.Contains(palabra[i]))
                {
                    return 'p';//la palabra de entrada contiene símbolos que no pertenecen al alfabeto usada por el Autómata.
                }

            }
            if (!quintuploOK)//Error, debido a que aún no se ha definido un quíntuplo completo de un AF para evaluar la e.r.
            {
                return 'e';
            }
            else if (estadoInicial.evaluarAFN(palabra, estados))
            {
                return 's';//palabra de entrada ha satisfecho la e.r definido por el quíntuplo
            }
            else
            {
                return 'n'; //palabra de entrada no cumplió la e.r
            }
        }
        public List<Estado> obtenerEstados()
        {
            return estados.Values.ToList();
        }


        public void nuevoQuintuplo()
        {
            this.quintuplo = new Quintuplo();
            this.estados = new Dictionary<string, Estado>();
            quintuploOK = false;

        }
    }
}
