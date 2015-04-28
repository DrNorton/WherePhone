using System;
using NUnit.Framework;
using WherePhone.Api.Executer;
using WherePhone.Api.Facade;


namespace WherePhone.Api.Tests
{
  
    [TestFixture()]
    public class ApiServiceTest
    {
        [Test()]
        public async void GetPosts()
        {
            var apiSettings = new FakeApiSettings();
            var facade = new ApiFacade(new ApiExecuter(apiSettings), apiSettings);
            var result = await facade.GetPhoneList();
            Assert.Greater(result.Count, 0);
        }

     


    }
}
