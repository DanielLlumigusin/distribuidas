using BookClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Consumiendo el servicio SOAP...");

        // Crear instancia del cliente SOAP
        ClientSOAP client = new ClientSOAP();

        while (true)
        {
            // Mostrar el menú y obtener la opción del usuario
            string opcion = Menu();

            switch (opcion)
            {
                case "1":
                    // Buscar todos los libros
                    var libros = await client.ObtenerTodosAsync();
                    Console.WriteLine("Lista de Libros:");
                    foreach (var libro in libros)
                    {
                        Console.WriteLine($"ID: {libro.Id}, Título: {libro.Title}, Autor: {libro.Author}, Descripción: {libro.Description}");
                    }
                    break;

                case "2":
                    // Buscar un libro por ID
                    Console.Write("Ingrese el ID del libro a buscar: ");
                    if (int.TryParse(Console.ReadLine(), out int idBuscar))
                    {
                        var libro = await client.ObtenerPorIdAsync(idBuscar);
                        if (libro != null)
                        {
                            Console.WriteLine($"ID: {libro.Id}, Título: {libro.Title}, Autor: {libro.Author}, Descripción: {libro.Description}");
                        }
                        else
                        {
                            Console.WriteLine("Libro no encontrado.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "3":
                    // Editar un libro
                    Console.Write("Ingrese el ID del libro a editar: ");
                    if (int.TryParse(Console.ReadLine(), out int idEditar))
                    {
                        Console.Write("Ingrese el nuevo título: ");
                        string nuevoTitulo = Console.ReadLine();
                        Console.Write("Ingrese el nuevo autor: ");
                        string nuevoAutor = Console.ReadLine();
                        Console.Write("Ingrese la nueva descripción: ");
                        string nuevaDescripcion = Console.ReadLine();

                        var libroEditado = new Libros
                        {
                            Id = idEditar,
                            Title = nuevoTitulo,
                            Author = nuevoAutor,
                            Description = nuevaDescripcion
                        };

                        await client.EditarLibroAsync(libroEditado);
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "4":
                    // Eliminar un libro
                    Console.Write("Ingrese el ID del libro a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int idEliminar))
                    {
                        await client.EliminarLibroAsync(idEliminar);
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "5":
                    Console.WriteLine("Gracias por probar nuestro microservicio SOAP del grupo 5 <3");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine();
        }
    }

    // Método para mostrar el menú
    public static string Menu()
    {
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1. Buscar todos los libros");
        Console.WriteLine("2. Buscar un libro por ID");
        Console.WriteLine("3. Editar un libro");
        Console.WriteLine("4. Eliminar un libro");
        Console.WriteLine("5. Salir");
        Console.Write("Opción: ");
        return Console.ReadLine();
    }
}
