using System;
using System.Collections.Generic;

namespace DinoData.Model;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<LocationDinosaur> LocationDinosaurs { get; set; } = new List<LocationDinosaur>();
}
