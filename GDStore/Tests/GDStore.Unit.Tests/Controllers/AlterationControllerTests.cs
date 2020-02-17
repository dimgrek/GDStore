using System;
using System.Threading.Tasks;
using GDStore.WebApi.Controllers;
using GDStore.WebApi.Models;
using GDStore.WebApi.Services;
using Moq;
using TestStack.FluentMVCTesting;
using Xunit;

namespace GDStore.Unit.Tests.Controllers
{
    public class AlterationControllerTests
    {
        private readonly Mock<IAlterationService> alterationService;
        private readonly AlterationController alterationController;

        public AlterationControllerTests()
        {
            alterationService = new Mock<IAlterationService>();
            alterationController = new AlterationController(alterationService.Object);
        }

        [Fact]
        public void CreateAlteration_Redirect_AlterationsByCustomerId()
        {
            //arrange
            var model = new AlterationModel{CustomerId = Guid.NewGuid()};
            alterationService.Setup(x => x.AddAlteration(It.IsAny<AlterationModel>())).Returns(Task.CompletedTask);
            
            //act

            //assert
            alterationController.WithCallTo(x => x.Create(model))
                .ShouldRedirectTo(x => x.AlterationsByCustomerId(model.CustomerId));
        }
    }
}
