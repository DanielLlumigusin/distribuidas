using ServiceBook;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClient
{
    public class ClientSOAP
    {
        // Método para obtener todos los libros
        public async Task<List<Libros>> ObtenerTodosAsync()
        {
            using (ServiceBookClient client = new ServiceBookClient())
            {
                try
                {
                    // Llamar al método del servicio SOAP
                    var result = await client.GetBooksAsync();

                    // Convertir los resultados al modelo local Libros
                    var libros = new List<Libros>();
                    foreach (var item in result)
                    {
                        libros.Add(new Libros
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description,
                            Author = item.Author
                        });
                    }

                    return libros;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener todos los libros: {ex.Message}");
                    return new List<Libros>();
                }
            }
        }

        // Método para obtener un libro por ID
        public async Task<Libros> ObtenerPorIdAsync(int id)
        {
            using (ServiceBookClient client = new ServiceBookClient())
            {
                try
                {
                    var result = await client.GetOneBookAsync(id);

                    // Verificar si el resultado no es nulo y mapear al modelo local
                    return result != null
                        ? new Libros
                        {
                            Id = result.Id,
                            Title = result.Title,
                            Author = result.Author,
                            Description = result.Description
                        }
                        : null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener el libro con ID {id}: {ex.Message}");
                    return null;
                }
            }
        }

        // Método para editar un libro
        public async Task EditarLibroAsync(Libros libro)
        {
            using (ServiceBookClient client = new ServiceBookClient())
            {
                try
                {
                    await client.UpdateBookAsync(new Book
                    {
                        Id = libro.Id,
                        Title = libro.Title,
                        Author = libro.Author,
                        Description = libro.Description
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al editar el libro con ID {libro.Id}: {ex.Message}");
                    
                }
            }
        }

        // Método para eliminar un libro
        public async Task EliminarLibroAsync(int id)
        {
            using (ServiceBookClient client = new ServiceBookClient())
            {
                try
                {
                    await client.DeleteBookAsync(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar el libro con ID {id}: {ex.Message}");
                   
                }
            }
        }
    }

 
}
