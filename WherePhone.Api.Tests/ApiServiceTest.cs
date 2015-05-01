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
            var result = await facade.GetPhone("TEST2");
            Assert.IsNotNull(result);
        }


        [Test()]
        public async void AddPhone()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.AddPhone(new Phone() { Image = "", Name = "Создано тестом", Platform = "test", Udid = Guid.NewGuid().ToString() });
            Assert.IsNotNull(result);
        }

        [Test()]
        public async void DeletePhone()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.DeletePhone(new Phone() { Udid = "c1489e16-235f-4c45-a082-706d33360021" });
            Assert.IsNotNull(result);
        }



        [Test()]
        public async void GetPhoneWhichNotExist()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetPhone("OLOLO");
            Assert.IsNull(result);
        }

        [Test()]
        public async void GetBorrowList()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetBorrowTickets();
            Assert.Greater(result.Count, 0);
        }

        [Test()]
        public async void GetBorrow()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetBorrow("TEST2");
            Assert.IsNotNull(result);
        }

        [Test()]
        public async void GetBorrowWhichNotExist()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetBorrow("OLOLOL");
            Assert.IsNull(result);
        }
    }
}
