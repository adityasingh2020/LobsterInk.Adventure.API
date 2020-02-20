using LobsterInk.Adventure.Domain;
using LobsterInk.Adventure.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LobsterInk.Adventure.Application
{
    public class AdventureService : IAdventureService
    {
        private readonly IAdventureRepository _adventureRepository;

        public AdventureService(IAdventureRepository adventureRepository)
        {
            _adventureRepository = adventureRepository;
        }
        public async Task<Domain.Adventure> GetAdventureDetails(int Id)
        {
            return await _adventureRepository.GetAdventureDetails(Id);
        }

        public async Task<Plot> GetPlot(int PlotId)
        {
            return await _adventureRepository.GetPlot(PlotId);
        }

        public async Task SavePlayerData(string email, int adventureId, int[] plotIds)
        {
            await _adventureRepository.SavePlayerData(email, adventureId, plotIds);
        }
    }
}
