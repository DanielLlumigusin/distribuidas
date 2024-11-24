using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;

public class ServiceBook : IServiceBook
{
    private readonly string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

    // Obtener todos los libros
    public List<Book> GetBooks()
    {
        var books = new List<Book>();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books;", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Description = (string)reader["Description"],
                        Author = (string)reader["Author"]
                    });
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new FaultException("Error inesperado:" + ex.Message);
        }
        return books;
    }

    // Obtener un libro por su Id
    public Book GetOneBook(int Id)
    {
        Book book = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE Id = @Id;", conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    book = new Book
                    {
                        Id = (int)reader["Id"],
                        Title = (string)reader["Title"],
                        Description = (string)reader["Description"],
                        Author = (string)reader["Author"]
                    };
                }
            }
        }
        catch (SqlException ex)
        {
            // Log or handle the exception here
            Console.WriteLine("Error al obtener el libro: " + ex.Message);
        }
        return book;
    }

    // Agregar un nuevo libro
    public void AddBook(Book book)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title, Description, Author) VALUES (@Title, @Description, @Author)", conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Description", book.Description);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            // Log or handle the exception here
            Console.WriteLine("Error al agregar el libro: " + ex.Message);
        }
    }

    // Actualizar un libro
    public void UpdateBook(Book book)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Books SET Title=@Title, Description=@Description, Author=@Author WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", book.Id);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Description", book.Description);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            // Log or handle the exception here
            Console.WriteLine("Error al actualizar el libro: " + ex.Message);
        }
    }

    // Eliminar un libro
    public void DeleteBook(int Id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            // Log or handle the exception here
            Console.WriteLine("Error al eliminar el libro: " + ex.Message);
        }
    }

}
