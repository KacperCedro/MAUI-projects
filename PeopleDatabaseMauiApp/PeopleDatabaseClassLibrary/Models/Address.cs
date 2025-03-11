using System;
using System.Collections.Generic;

namespace PeopleDatabaseClassLibrary.Models;

public partial class Address
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;

    public string HouseNumber { get; set; } = null!;

    public string City { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
