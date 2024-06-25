using System;
using System.Collections.Generic;

namespace Hastane.Models;

public partial class Branslar
{
    public int BransId { get; set; }

    public string? BransAd { get; set; }

    public virtual ICollection<Doktorlar> Doktorlars { get; set; } = new List<Doktorlar>();
}
