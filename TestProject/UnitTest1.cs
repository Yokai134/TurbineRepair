using TurbineRepair;
using TurbineRepair.ViewModel;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestAuthetication()
        {
            AutheticationVM autheticationVM = new AutheticationVM();
            autheticationVM.LoginApp = "ivanov134";
            autheticationVM.PasswordApp = "Qwerty123";
            bool authUser = autheticationVM.VerificationData(autheticationVM.LoginApp, autheticationVM.PasswordApp);
            Assert.IsTrue(authUser);

        }
    }
}