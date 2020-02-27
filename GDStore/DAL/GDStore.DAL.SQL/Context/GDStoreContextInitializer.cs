using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;

namespace GDStore.DAL.SQL.Context
{
    public class GDStoreContextInitializer : DropCreateDatabaseIfModelChanges<GDStoreContext>
    {
        protected override void Seed(GDStoreContext context)
        {
            if (context.Sleeves.Any())
            {
                return;
            }

            var firstLeftSleeve = new Sleeve{ Id = Guid.NewGuid(),Length = 5, Side = Side.Left};
            var firstRightSleeve = new Sleeve { Id = Guid.NewGuid(), Length = 5, Side = Side.Right };
            var secondLeftSleeve = new Sleeve { Id = Guid.NewGuid(), Length = 6, Side = Side.Left };
            var secondRightSleeve = new Sleeve { Id = Guid.NewGuid(), Length = 5, Side = Side.Right };
            var thirdLeftSleeve = new Sleeve { Id = Guid.NewGuid(), Length = 7, Side = Side.Left };
            var thirdRightSleeve = new Sleeve { Id = Guid.NewGuid(), Length = 7, Side = Side.Right };

            context.Sleeves.Add(firstLeftSleeve);
            context.Sleeves.Add(firstRightSleeve);
            context.Sleeves.Add(secondLeftSleeve);
            context.Sleeves.Add(secondRightSleeve);
            context.Sleeves.Add(thirdLeftSleeve);
            context.Sleeves.Add(thirdRightSleeve);

            var firstLeftTrouserLeg = new TrouserLeg { Id = Guid.NewGuid(), Length = 5, Side = Side.Left };
            var firstRightTrouserLeg = new TrouserLeg { Id = Guid.NewGuid(), Length = 5, Side = Side.Right };
            var secondLeftTrouserLeg = new TrouserLeg { Id = Guid.NewGuid(), Length = 6, Side = Side.Left };
            var secondRightTrouserLeg = new TrouserLeg { Id = Guid.NewGuid(), Length = 5, Side = Side.Right };
            var thirdLeftTrouserLeg = new TrouserLeg { Id = Guid.NewGuid(), Length = 7, Side = Side.Left };
            var thirdRightTrouserLeg = new TrouserLeg { Id = Guid.NewGuid(), Length = 7, Side = Side.Right };

            context.TrouserLegs.Add(firstLeftTrouserLeg);
            context.TrouserLegs.Add(firstRightTrouserLeg);

            var firstSuit = new Suit
            {
                Id = Guid.NewGuid(),
                Name = "first suit",
                LeftSleeve = firstLeftSleeve,
                RightSleeve = firstRightSleeve,
                LeftTrouserLeg = firstLeftTrouserLeg,
                RightTrouserLeg = firstRightTrouserLeg
            };

            var secondSuit = new Suit
            {
                Id = Guid.NewGuid(),
                Name = "second suit",
                LeftSleeve = secondLeftSleeve,
                RightSleeve = secondRightSleeve,
                LeftTrouserLeg = secondLeftTrouserLeg,
                RightTrouserLeg = secondRightTrouserLeg
            };

            var thirdSuit = new Suit
            {
                Id = Guid.NewGuid(),
                Name = "third suit",
                LeftSleeve = thirdLeftSleeve,
                RightSleeve = thirdRightSleeve,
                LeftTrouserLeg = thirdLeftTrouserLeg,
                RightTrouserLeg = thirdRightTrouserLeg
            };

            context.Suits.Add(firstSuit);
            context.Suits.Add(secondSuit);
            context.Suits.Add(thirdSuit);

            var firstCustomer = new Customer { Id = Guid.NewGuid(), Email = "first@hello.com", FirstName = "Elon", LastName = "Musk", Suits = new List<Suit> { firstSuit } };
            var secondCustomer = new Customer { Id = Guid.NewGuid(), Email = "second@hello.com", FirstName = "Brad", LastName = "Pitt", Suits = new List<Suit> { secondSuit } };
            var thirdCustomer = new Customer { Id = Guid.NewGuid(), Email = "third@hello.com", FirstName = "Tim", LastName = "Cook", Suits = new List<Suit> { thirdSuit } };

            context.Customer.Add(firstCustomer);
            context.Customer.Add(secondCustomer);
            context.Customer.Add(thirdCustomer);
            context.SaveChanges();
        }
    }
}