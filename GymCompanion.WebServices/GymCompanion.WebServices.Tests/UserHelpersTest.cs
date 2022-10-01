
using GymCompanion.WebServices.Models;

namespace GymCompanion.WebServices.Tests
{
    [TestClass]
    public class UserHelpersTest
    {
        [TestMethod]
        public void TestCorrectRegister()
        {
            User user = new User()
            {
                Username = "SteliosPantelopoulos",
                Password = "P@ssw0rd",
                Email = "steliosPantelopoulos@test.com",
                Firstname = "Ste;ios",
                Lastname = "Testlast",
                Country = "Testland",
                Birthday = new DateTime(2000, 10, 30),
                RegistrationDate = new DateTime(2022, 9, 30)
            };

            
        }
    }
}