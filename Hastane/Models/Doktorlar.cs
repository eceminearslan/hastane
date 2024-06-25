using System;
using System.Collections.Generic;

namespace Hastane.Models;

public partial class Doktorlar
{
    public int DoktorId { get; set; }

    public int? BransId { get; set; }

    public string? DoktorAdSoyad { get; set; }

    public virtual Branslar? Brans { get; set; }
}
