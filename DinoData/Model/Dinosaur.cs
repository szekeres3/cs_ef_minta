using System;
using System.Collections.Generic;

namespace DinoData.Model;

public partial class Dinosaur
{
    public int Id { get; set; }

    public string? Nickname { get; set; }

    public DateOnly Birth { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public double Length { get; set; }

    public string? Version { get; set; }

    public int? SpeciesId { get; set; }

    public virtual ICollection<LocationDinosaur> LocationDinosaurs { get; set; } = new List<LocationDinosaur>();

    public virtual Species? Species { get; set; }
}
