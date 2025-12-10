using System;
using System.Collections.Generic;

namespace DinoData.Model;

public partial class Species
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();
}
