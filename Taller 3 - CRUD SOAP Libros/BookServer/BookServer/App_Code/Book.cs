using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Web;

    [DataContract]
public class Book
{
    [DataMember]
    public int Id { set; get; }
    [DataMember]
    public string Title { set; get; }

    [DataMember]
    public string Description { set; get; }
    [DataMember]
    public string Author { set; get; }
}