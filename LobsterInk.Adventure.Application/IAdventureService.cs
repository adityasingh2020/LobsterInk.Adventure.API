using LobsterInk.Adventure.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LobsterInk.Adventure.Application
{
    public interface IAdventureService
    {
        Task<Domain.Adventure> GetAdventureDetails(int Id);

        Task<Plot> GetPlot(int PlotId);

        Task SavePlayerData(string email, int adventureId, int[] plotIds);
    }
}
