using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;

namespace GDStore.DAL.SQL.Context
{
    public class GDStoreContextInitializer : DropCreateDatabaseAlways<GDStoreContext>
    {
        protected override void Seed(GDStoreContext context)
        {
            if (context.Sleeves.Any())
            {
                return;
            }

            var firstLeftSleeve = new Sleeve{Length = 5, Side = Side.Left};
            var firstRightSleeve = new Sleeve { Length = 5, Side = Side.Right };
            var secondLeftSleeve = new Sleeve { Length = 6, Side = Side.Left };
            var secondRightSleeve = new Sleeve { Length = 5, Side = Side.Right };
            var thirdLeftSleeve = new Sleeve { Length = 7, Side = Side.Left };
            var thirdRightSleeve = new Sleeve { Length = 7, Side = Side.Right };

            context.Sleeves.Add(firstLeftSleeve);
            context.Sleeves.Add(firstRightSleeve);
            context.Sleeves.Add(secondLeftSleeve);
            context.Sleeves.Add(secondRightSleeve);
            context.Sleeves.Add(thirdLeftSleeve);
            context.Sleeves.Add(thirdRightSleeve);

            var firstLeftTrouserLeg = new TrouserLeg { Length = 5, Side = Side.Left };
            var firstRightTrouserLeg = new TrouserLeg { Length = 5, Side = Side.Right };
            var secondLeftTrouserLeg = new TrouserLeg { Length = 6, Side = Side.Left };
            var secondRightTrouserLeg = new TrouserLeg { Length = 5, Side = Side.Right };
            var thirdLeftTrouserLeg = new TrouserLeg { Length = 7, Side = Side.Left };
            var thirdRightTrouserLeg = new TrouserLeg { Length = 7, Side = Side.Right };

            context.TrouserLegs.Add(firstLeftTrouserLeg);
            context.TrouserLegs.Add(firstRightTrouserLeg);

            var firstSuit = new Suit
            {
                Name = "first suit",
                Sleeves = new List<Sleeve> { firstLeftSleeve, firstRightSleeve },
                TrouserLegs = new List<TrouserLeg> { firstLeftTrouserLeg, firstRightTrouserLeg }
            };

            var secondSuit = new Suit
            {
                Name = "second suit",
                Sleeves = new List<Sleeve> { secondLeftSleeve, secondRightSleeve },
                TrouserLegs = new List<TrouserLeg> { secondLeftTrouserLeg, secondRightTrouserLeg }
            };

            var thirdSuit = new Suit
            {
                Name = "third suit",
                Sleeves = new List<Sleeve> { thirdLeftSleeve, thirdRightSleeve },
                TrouserLegs = new List<TrouserLeg> { thirdLeftTrouserLeg, thirdRightTrouserLeg }
            };

            context.Suits.Add(firstSuit);
            context.Suits.Add(secondSuit);
            context.Suits.Add(thirdSuit);

            var firstCustomer = new Customer { Email = "first@hello.com", FirstName = "Elon", LastName = "Musk", Suits = new List<Suit> { firstSuit } };
            var secondCustomer = new Customer { Email = "second@hello.com", FirstName = "Brad", LastName = "Pitt", Suits = new List<Suit> { secondSuit } };
            var thirdCustomer = new Customer { Email = "third@hello.com", FirstName = "Tim", LastName = "Cook", Suits = new List<Suit> { thirdSuit } };

            context.Customer.Add(firstCustomer);
            context.Customer.Add(secondCustomer);
            context.Customer.Add(thirdCustomer);
            context.SaveChanges();
        }
    }
}