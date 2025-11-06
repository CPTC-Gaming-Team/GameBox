using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using GameBox.Controllers;
using GameBox.Data;
using GameBox.Models;

namespace GameBox.Tests
{
    public class GameModelsControllerTests
    {
        private static ApplicationDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewWithAllGames()
        {
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateContext(dbName);
            context.GameModels.AddRange(
                new GameModel { Name = "Game A", Description = "A", Rating = 5 },
                new GameModel { Name = "Game B", Description = "B", Rating = 7 }
            );
            await context.SaveChangesAsync();

            var controller = new GameModelsController(context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<GameModel>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdNull()
        {
            await using var context = CreateContext(Guid.NewGuid().ToString());
            var controller = new GameModelsController(context);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenNotFound()
        {
            await using var context = CreateContext(Guid.NewGuid().ToString());
            var controller = new GameModelsController(context);

            var result = await controller.Details(12345);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsView_WhenFound()
        {
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateContext(dbName);
            var game = new GameModel { Name = "Found", Description = "desc", Rating = 4 };
            context.GameModels.Add(game);
            await context.SaveChangesAsync();

            var controller = new GameModelsController(context);

            var result = await controller.Details(game.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<GameModel>(viewResult.Model);
            Assert.Equal(game.Id, model.Id);
            Assert.Equal("Found", model.Name);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelValid()
        {
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateContext(dbName);
            var controller = new GameModelsController(context);

            var newGame = new GameModel { Name = "New", Description = "newdesc", Rating = 6 };

            var result = await controller.Create(newGame);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirect.ActionName);

            Assert.Equal(1, context.GameModels.Count());
            var stored = context.GameModels.First();
            Assert.Equal("New", stored.Name);
        }

        [Fact]
        public async Task Create_Post_ReturnsView_WhenModelInvalid()
        {
            await using var context = CreateContext(Guid.NewGuid().ToString());
            var controller = new GameModelsController(context);
            controller.ModelState.AddModelError("Name", "Required");

            var newGame = new GameModel { Name = "", Description = "d", Rating = 3 };

            var result = await controller.Create(newGame);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<GameModel>(viewResult.Model);
            Assert.Same(newGame, model);
            Assert.Empty(context.GameModels);
        }

        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenIdNull()
        {
            await using var context = CreateContext(Guid.NewGuid().ToString());
            var controller = new GameModelsController(context);

            var result = await controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenNotFound()
        {
            await using var context = CreateContext(Guid.NewGuid().ToString());
            var controller = new GameModelsController(context);

            var result = await controller.Edit(9999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsView_WhenFound()
        {
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateContext(dbName);
            var game = new GameModel { Name = "E", Description = "d", Rating = 2 };
            context.GameModels.Add(game);
            await context.SaveChangesAsync();

            var controller = new GameModelsController(context);

            var result = await controller.Edit(game.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<GameModel>(viewResult.Model);
            Assert.Equal(game.Id, model.Id);
        }

        [Fact]
        public async Task Delete_Get_ReturnsView_WhenFound()
        {
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateContext(dbName);
            var game = new GameModel { Name = "D", Description = "d", Rating = 1 };
            context.GameModels.Add(game);
            await context.SaveChangesAsync();

            var controller = new GameModelsController(context);

            var result = await controller.Delete(game.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<GameModel>(viewResult.Model);
            Assert.Equal(game.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_DeletesAndRedirects()
        {
            var dbName = Guid.NewGuid().ToString();
            await using var context = CreateContext(dbName);
            var game = new GameModel { Name = "ToDelete", Description = "d", Rating = 8 };
            context.GameModels.Add(game);
            await context.SaveChangesAsync();

            var controller = new GameModelsController(context);

            var result = await controller.DeleteConfirmed(game.Id);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirect.ActionName);
            Assert.Empty(context.GameModels);
        }
    }
}