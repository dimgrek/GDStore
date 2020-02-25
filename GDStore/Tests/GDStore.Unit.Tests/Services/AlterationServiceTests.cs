using System;
using System.Collections.Generic;
using FakeItEasy;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Services.CommandBus;
using GDStore.Alterations.Services.Services;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using Moq;
using Xunit;

namespace GDStore.Unit.Tests.Services
{
    public class AlterationServiceTests
    {
        private readonly AlterationService sut;
        private readonly ISuitRepository suitRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IAlterationRepository alterationRepository;
        private readonly INotificationCommandBus notificationCommandBus;

        public AlterationServiceTests()
        {
            suitRepository = A.Fake<ISuitRepository>();
            customerRepository = A.Fake<ICustomerRepository>();
            alterationRepository = A.Fake<IAlterationRepository>();
            notificationCommandBus = A.Fake<INotificationCommandBus>();

            sut = new AlterationService(suitRepository, customerRepository, alterationRepository, notificationCommandBus);
        }

        [Fact]
        public async void AddAlteration()
        {
            //arrange
            var command = new AddAlterationCommand {Item = Item.Sleeve};

            var suit = new Suit
            {
                Sleeves = new List<Sleeve>
                {
                    new Sleeve {Side = Side.Right, Id = Guid.NewGuid()},
                    new Sleeve {Side = Side.Left, Id = Guid.NewGuid()}
                }
            };

            var customer = new Customer();
            A.CallTo(() => suitRepository.GetAllByCustomerId(It.IsAny<Guid>())).Returns(new List<Suit>());
            A.CallTo(() => customerRepository .GetByIdAsync(It.IsAny<Guid>())).Returns(customer);

            //act 
            await sut.AddAlteration(command);

            //assert
            A.CallTo(() => customerRepository.SaveChangesAsync()).MustHaveHappened();
        }
    }
}