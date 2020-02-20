using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LobsterInk.Adventure.Domain;

namespace LobsterInk.Adventure.Infrastructure
{
    public interface IAdventureRepository
    {
        Task<Domain.Adventure> GetAdventureDetails(int Id);

        Task<Plot> GetPlot(int parentPlotId);

        Task SavePlayerData(string email, int adventureId, int[] plotIds);
    }
}
