using System;
using ServiceReference1;
namespace CalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);

            try
            {
                bool keepRunning = true;

                while (keepRunning)
                {
                    Console.Clear();
                    Console.WriteLine("=== Calculadora ===");
                    Console.WriteLine("1. Sumar");
                    Console.WriteLine("2. Restar");
                    Console.WriteLine("3. Multiplicar");
                    Console.WriteLine("4. Dividir");
                    Console.WriteLine("5. Salir");
                    Console.Write("Selecciona una opción: ");
                    string choice = Console.ReadLine();

                    if (choice == "5")
                    {
                        Console.WriteLine("Saliendo del programa...");
                        keepRunning = false;
                        continue;
                    }

                    // Pedir los números al usuario
                    Console.Write("Introduce el primer número: ");
                    if (!int.TryParse(Console.ReadLine(), out int number1))
                    {
                        Console.WriteLine("Número no válido. Presiona Enter para intentar nuevamente.");
                        Console.ReadLine();
                        continue;
                    }

                    Console.Write("Introduce el segundo número: ");
                    if (!int.TryParse(Console.ReadLine(), out int number2))
                    {
                        Console.WriteLine("Número no válido. Presiona Enter para intentar nuevamente.");
                        Console.ReadLine();
                        continue;
                    }

                    // Procesar la opción seleccionada
                    switch (choice)
                    {
                        case "1":
                            var sumResult = client.AddAsync(number1, number2).Result;
                            Console.WriteLine($"Resultado de la suma: {number1} + {number2} = {sumResult}");
                            break;

                        case "2":
                            var subtractResult = client.SubtractAsync(number1, number2).Result;
                            Console.WriteLine($"Resultado de la resta: {number1} - {number2} = {subtractResult}");
                            break;

                        case "3":
                            var multiplyResult = client.MultiplyAsync(number1, number2).Result;
                            Console.WriteLine($"Resultado de la multiplicación: {number1} * {number2} = {multiplyResult}");
                            break;

                        case "4":
                            if (number2 == 0)
                            {
                                Console.WriteLine("Error: No se puede dividir entre cero.");
                            }
                            else
                            {
                                var divideResult = client.DivideAsync(number1, number2).Result;
                                Console.WriteLine($"Resultado de la división: {number1} / {number2} = {divideResult}");
                            }
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Intenta nuevamente.");
                            break;
                    }

                    Console.WriteLine("\nPresiona Enter para regresar al menú...");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consumir el servicio: {ex.Message}");
            }
            finally
            {
                client.CloseAsync().Wait();
            }
        }
    }
}
