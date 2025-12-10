using System;
using System.Collections.Generic;

namespace DinoData.Model;

public partial class LocationDinosaur
{
    public int Id { get; set; }

    public int? LocationId { get; set; }

    public int? DinosaurId { get; set; }

    public string CreatedAt { get; set; } = null!;

    public virtual Dinosaur? Dinosaur { get; set; }

    public virtual Location? Location { get; set; }
}
