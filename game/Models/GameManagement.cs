using System;
using System.Collections.Generic;

namespace game.Models;

public partial class GameManagement
{
    public int GameId { get; set; }

    public string? GameName { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<GamerManagement> Gamers { get; set; } = new List<GamerManagement>();
}
