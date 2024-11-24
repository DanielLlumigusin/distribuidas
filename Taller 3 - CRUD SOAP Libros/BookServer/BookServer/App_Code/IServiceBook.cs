using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IServiceBook
{

	[OperationContract]
	List<Book> GetBooks();

	[OperationContract]
	Book GetOneBook(int Id);

	[OperationContract]
	void AddBook(Book book);

	[OperationContract]
	void UpdateBook(Book book);

	[OperationContract]
	void DeleteBook(int Id);
}
