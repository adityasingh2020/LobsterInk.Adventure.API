using System;
using System.Collections.Generic;
using System.Text;

namespace LobsterInk.Adventure.Domain
{
    public class Player
    {
        public string Email { get; set; }
        public int AdventureId { get; set; }
        public List<PlotEntity> SelectedPlots { get; set; }
    }
}
