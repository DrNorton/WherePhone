using System;
using NUnit.Framework;
using WherePhone.Api.Executer;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;


namespace WherePhone.Api.Tests
{
  
    [TestFixture()]
    public class ApiServiceTest
    {
        [Test()]
        public async void GetPhones()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetPhoneList();
            Assert.Greater(result.Count, 0);
        }
        [Test()]
        public async void GetUsers()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetUsers();
            Assert.Greater(result.Count, 0);
        }

        [Test()]
        public async void GetPhone()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetPhone("331f65f6-fbc6-40bc-b16f-35b49840441b");
            Assert.IsNotNull(result);
        }


        [Test()]
        public async void RegisterAndUnregisterPhone()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.RegisterPhone(new Device() { ImageUrl = "", Name = "Создано тестом", Platform = "test", Guid = "c1489e16-235f-4c45-a082-706d33360021" });
            DeletePhone();
        }

       
        public async void DeletePhone()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.DeletePhone(new Device() { Guid = "c1489e16-235f-4c45-a082-706d33360021" });
            Assert.IsNull(result);
        }



        [Test()]
        [ExpectedException(typeof(ApiException))]
        public async void GetPhoneWhichNotExist()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);

            var result = await facade.GetPhone("teasattat");
            Assert.IsNull(result);
        }

        [Test()]
        public async void GetCurrentBorrow()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetCurrentBorrow("331f65f6-fbc6-40bc-b16f-35b49840441b");
            Assert.IsNotNull(result);
        }

        [Test()]
        public async void GetAllBorrowsByDeviceId()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetBorrows("331f65f6-fbc6-40bc-b16f-35b49840441b");
            Assert.IsNotNull(result);
        }

  
    }
}
