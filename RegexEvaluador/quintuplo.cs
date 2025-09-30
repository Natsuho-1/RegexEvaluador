using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RegexEvaluador
{
    internal class quintuplo
    {
        public string Origen {  get; set; }
        public string Simbolo { get; set; }
        public string Destino { get; set; }
        private static List<quintuplo> todasLasTransiciones = new List<quintuplo>();

        public quintuplo(string origen, string simbolo, string destino ) 
        { 
            Origen = origen;
            Simbolo = simbolo;
            Destino = destino;
        }
        public override string ToString()
        {
            return $"{Origen} --{Simbolo}--> {Destino}";
        }
        public static void Agregartrancicion(string entrada)
        {
            string[] partes = entrada.Split(',');
            if (partes.Length == 3)
            {
                todasLasTransiciones.Add(new quintuplo(partes[0], partes[1], partes[2]));
            }
            else
            {
                Console.WriteLine("Formato incorrecto. Usa: estadoOrigen,símbolo,estadoDestino");
            }
        }
        public static void MostrarTodas()
        {
            Console.WriteLine("\nTransiciones ingresadas:");
            foreach (var t in todasLasTransiciones)
            {
                Console.WriteLine(t);
            }
        }
        public void convertirtoarray(string estado, string alfabeto, string starts, string finals)
        {
            string[] estados2 = estado.Split(',');
            string[] alfabeto2 = alfabeto.Split(',');
            string[] starts2 = starts.Split(',');
            string[] finals2 = finals.Split(',');
            Console.Write("K: {");
            for (int i = 0; i < estados2.Length; i++)
            {
                Console.Write(estados2[i]);
                if (i < estados2.Length - 1)
                {
                    Console.Write(',');
                }
            }
            Console.Write("}\n");
            Console.Write("∑: {");
            for (int i = 0; i < alfabeto2.Length; i++)
            {
                Console.Write(alfabeto2[i]);
                if (i < alfabeto2.Length - 1)
                {
                    Console.Write(',');
                }
            }
            Console.Write("}\n");
            Console.Write("s: {");
            for (int i = 0; i < starts2.Length; i++)
            {
                Console.Write(starts2[i]);
                if (i < starts2.Length - 1)
                {
                    Console.Write(',');
                }
            }
            Console.Write("}\n");
            Console.Write("F: {");
            for (int i = 0; i < finals2.Length; i++)
            {
                Console.Write(finals2[i]);
                if (i < finals2.Length - 1)
                {
                    Console.Write(',');
                }
            }
            Console.Write("}\n");
        }
    }
}
