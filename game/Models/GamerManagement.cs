using System;
using System.Collections.Generic;

namespace game.Models;

public partial class GamerManagement
{
    public int GamerId { get; set; }

    public string? GamerName { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<GameManagement> Games { get; set; } = new List<GameManagement>();
}
