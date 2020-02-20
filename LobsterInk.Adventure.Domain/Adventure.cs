using System;
using System.Collections.Generic;
using System.Text;

namespace LobsterInk.Adventure.Domain
{
    public class Adventure
    {
        public int AdventureId { get; set; }
        public string Title { get; set; }
        public Plot FirstPlot { get; set; }
    }
}
