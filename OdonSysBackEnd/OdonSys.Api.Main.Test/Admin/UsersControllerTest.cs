using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace OdonSys.Api.Main.Test.Admin
{
    internal class UsersControllerTest: TestBase
    {
        private readonly string _uri = "api/users";
        //private string _userId;

        [SetUp]
        public void SetUp()
        {
            //_userId = "c0d7d4c6-6089-41d4-be5d-d479516bd7e9";
        }

        [Test]
        public async Task GetAllShouldReturnOk()
        {
            var response = await _sut.GetAsync(_uri);
            Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
        }
    }
}
