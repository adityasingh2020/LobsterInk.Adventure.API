using LobsterInk.Adventure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobsterInk.Adventure.Infrastructure
{
    public class AdventureRepository : IAdventureRepository
    {
        private static List<Domain.Adventure> _adventures;
        private static List<Player> _players;
        private static List<PlotEntity> _plots;
        public AdventureRepository()
        {
            LoadPlots();
            _adventures = LoadAdventures();
            _players = new List<Player>();
        }

        public async Task<Domain.Adventure> GetAdventureDetails(int Id)
        {
            var adv = _adventures.Where(n => n.AdventureId == Id).FirstOrDefault();
            return await Task.FromResult<Domain.Adventure>(adv);
        }

        public async Task<Plot> GetPlot(int PlotId)
        {
            var adv = GetPlotData(PlotId);
            return await Task.FromResult<Domain.Plot>(adv);
        }

        public async Task SavePlayerData(string email, int adventureId, int[] plotIds)
        {
            await Task.Run(() =>
            {
                _players.Add(new Player
                {
                    Email = email,
                    AdventureId = adventureId,
                    SelectedPlots = _plots.Where(n => plotIds.Contains(n.PlotId)).ToList()
                });
            });
            
        }

        private Plot GetPlotData(int PlotId)
        {
            var plot = _plots.Where(n => n.PlotId == PlotId).FirstOrDefault();
            return plot == null ? null :
            new Plot
            {
                PlotId = plot.PlotId,
                Action = plot.Action,
                Description = plot.Description,
                Choices = GetSubordinates(PlotId)
            };
        }
        private List<Plot> GetSubordinates(int ParentPlotId)
        {
            return _plots.Where(n => n.ParentPlotId == ParentPlotId).Select(p =>
                 new Plot
                 {
                     PlotId = p.PlotId,
                     Action = p.Action,
                     Description = p.Description
                 }).ToList();
        }
        private List<Domain.Adventure> LoadAdventures()
        {
            return new List<Domain.Adventure>
            {
                {
                    new Domain.Adventure 
                    { 
                        AdventureId = 1,
                        Title = "Doughnut",
                        FirstPlot = GetPlotData(1)
                    } 
                }
            };
        }

        public void LoadPlots()
        {
            _plots = new List<PlotEntity>
            {
                {new PlotEntity { PlotId=1, Description="Do I want a doughnut ?", Action="", ParentPlotId = null } }, //2,3
                {new PlotEntity { PlotId=2, Description="Do I deserve it ?", Action="YES", ParentPlotId = 1} }, //4,5
                {new PlotEntity { PlotId=3, Description="Maybe you want an apple ?", Action="NO", ParentPlotId = 1} }, //null
                {new PlotEntity { PlotId=4, Description="Are You Sure ?", Action="YES", ParentPlotId = 2 } }, // 6,7
                {new PlotEntity { PlotId=5, Description="Is it a good doughnut ?", Action="NO", ParentPlotId = 2 } }, // 8,9
                {new PlotEntity { PlotId=6, Description="Get it.", Action="YES", ParentPlotId = 4} }, // null
                {new PlotEntity { PlotId=7, Description="Do jumping jack first.", Action="NO", ParentPlotId = 4} }, // null
                {new PlotEntity { PlotId=8, Description="What are you waiting for? Grab it now.", Action="YES", ParentPlotId = 5} }, // null
                {new PlotEntity { PlotId=9, Description="Wait till you find a sinful, unforgettable doughnut.", Action="NO", ParentPlotId =5} } //null
            };
        }

    }
}
