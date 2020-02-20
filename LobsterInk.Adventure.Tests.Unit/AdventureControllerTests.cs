using LobsterInk.Adventure.API.Controllers;
using LobsterInk.Adventure.Application;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace LobsterInk.Adventure.Tests.Unit
{
    public class AdventureControllerTests
    {
        private Mock<IAdventureService> _adventureServiceMock;

        public AdventureControllerTests()
        {
            _adventureServiceMock = new Mock<IAdventureService>();
        }

        [Fact]
        public async Task GetAdventure_OK()
        {
            var result = new Domain.Adventure
            {
                AdventureId = 1,
                FirstPlot = new Domain.Plot(),
                Title = "abc"
            };
            _adventureServiceMock.Setup(adv => adv.GetAdventureDetails(1)).ReturnsAsync(result);

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetAdventure(1);
            var objResult = res.Result as ObjectResult;

            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.OK);
            var value = Assert.IsType<Domain.Adventure>(objResult.Value);
            Assert.Equal(result.AdventureId, value.AdventureId);
            Assert.Equal(result.Title, value.Title);
            Assert.Equal(result.FirstPlot.PlotId, value.FirstPlot.PlotId);

        }

        [Fact]
        public async Task GetAdventure_Failed()
        {
            _adventureServiceMock.Setup(opp => opp.GetAdventureDetails(1)).ThrowsAsync(new Exception());

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetAdventure(1);
            var ObjRes = res.Result as StatusCodeResult;

            Assert.NotNull(res);
            Assert.True(ObjRes.StatusCode == 500);

        }

        [Fact]
        public async Task GetAdventure_NotFound()
        {
            _adventureServiceMock.Setup(adv => adv.GetAdventureDetails(1)).ReturnsAsync(null as Domain.Adventure);

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetAdventure(1);
            var objResult = res.Result as NotFoundResult;

            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAdventure_BadRequest()
        {
            var result = new Domain.Adventure
            {
                AdventureId = 1,
                FirstPlot = new Domain.Plot(),
                Title = "abc"
            };
            _adventureServiceMock.Setup(adv => adv.GetAdventureDetails(It.IsAny<int>())).ReturnsAsync(result);

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetAdventure(-1);
            var ObjRes = res.Result as BadRequestResult;

            Assert.NotNull(res);
            Assert.True(ObjRes.StatusCode == 400);

        }

        [Fact]
        public async Task GetPlot_OK()
        {
            var result = new Domain.Plot
            {
                PlotId = 1,
                Choices = new List<Domain.Plot>(),
                Action = "Yes",
                Description = "test"
            };
            _adventureServiceMock.Setup(adv => adv.GetPlot(It.IsAny<int>())).ReturnsAsync(result);

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetPlot(1);
            var objResult = res.Result as ObjectResult;

            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.OK);
            var value = Assert.IsType<Domain.Plot>(objResult.Value);
            Assert.Equal(result.PlotId, value.PlotId);
            Assert.Equal(result.Description, value.Description);
            Assert.Equal(result.Choices.Count, value.Choices.Count);

        }

        [Fact]
        public async Task GetPlot_Failed()
        {
            _adventureServiceMock.Setup(opp => opp.GetPlot(It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetPlot(1);
            var ObjRes = res.Result as StatusCodeResult;

            Assert.NotNull(res);
            Assert.True(ObjRes.StatusCode == 500);

        }

        [Fact]
        public async Task GetPlot_NotFound()
        {
            _adventureServiceMock.Setup(adv => adv.GetAdventureDetails(It.IsAny<int>())).ReturnsAsync(null as Domain.Adventure);

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetAdventure(1);
            var objResult = res.Result as NotFoundResult;

            Assert.NotNull(res);
            Assert.True(objResult.StatusCode == (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetPlot_BadRequest()
        {
            var result = new Domain.Plot
            {
                PlotId = 1,
                Choices = new List<Domain.Plot>(),
                Action = "Yes",
                Description = "test"
            };
            _adventureServiceMock.Setup(adv => adv.GetPlot(It.IsAny<int>())).ReturnsAsync(result);

            var controller = new AdventureController(_adventureServiceMock.Object);
            var res = await controller.GetPlot(-1);
            var ObjRes = res.Result as BadRequestResult;

            Assert.NotNull(res);
            Assert.True(ObjRes.StatusCode == 400);

        }
    }
}
