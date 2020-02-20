using System;
using System.Collections.Generic;
using System.Text;

namespace LobsterInk.Adventure.Domain
{
    public class Plot
    {
        public int PlotId { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public List<Plot> Choices { get; set; }
    }
}
