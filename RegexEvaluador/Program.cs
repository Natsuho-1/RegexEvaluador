// See https://aka.ms/new-console-template for more information
using RegexEvaluador;
using System.Runtime.Serialization;
using System.Transactions;
quintuplo quintu = new quintuplo("q0", "a", "q1");

Console.ForegroundColor = ConsoleColor.Black;
Console.BackgroundColor = ConsoleColor.White;
Console.Clear();
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("Evaluador de Expresiones Regulares!");
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("Ingrese el quintuplo del Atomata");
Console.WriteLine("----------------------------------------------------");
Console.Write("Ingrese el conjunto de estados K: ");
string estados = Console.ReadLine();
Console.WriteLine("----------------------------------------------------");
Console.Write("Ingrese el alfabeto ∑: ");
string alfabeto = Console.ReadLine();
Console.WriteLine("----------------------------------------------------");
Console.Write("Ingrese los estados inciales s: ");
string starts  = Console.ReadLine();
Console.WriteLine("----------------------------------------------------");
Console.Write("Ingrese los estados finales F: ");
string finals  = Console.ReadLine();
Console.WriteLine("----------------------------------------------------");
Console.Clear();
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("Ingresa las transiciones en formato: estadoOrigen,símbolo,estadoDestino");
string condicion = "";
do
{
    Console.Write("Transición: ");
    string entrada = Console.ReadLine();
    quintuplo.Agregartrancicion(entrada);
    Console.WriteLine("Escribe 'fin' para terminar, presiona Enter para agregar otra transicion.");
    Console.WriteLine("----------------------------------------------------");
    Console.Write("Escribe fin para salir: ");
    condicion = Console.ReadLine();
} while (condicion == "");

Console.Clear();
int opcion;
Console.WriteLine("Digita lo que quieres hacer: ");
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("1. Ver Quintuplo");
Console.WriteLine("2. Generar Expresion");
Console.WriteLine("----------------------------------------------------");
Console.Write("Digitar opcion: ");
opcion = int.Parse(Console.ReadLine());
Console.WriteLine("----------------------------------------------------");
switch (opcion)
{
    case 1:
        quintu.convertirtoarray(estados, alfabeto, starts, finals);
        quintuplo.MostrarTodas();
        Console.ReadKey();
        break;
    case 2:
        //aqui falta poner el metodo o funion que genere la expresion
        break;
}