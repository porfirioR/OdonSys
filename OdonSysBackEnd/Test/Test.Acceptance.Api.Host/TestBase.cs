using Access.Sql;
using Contract.Administration.Authentication;
using Host.Api.Contract.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Utilities.Enums;
using Utilities.Extensions;

namespace AcceptanceTest.Host.Api
{
    [TestFixture(Category = "Acceptance")]
    internal class TestBase
    {
        private string _testPassword = string.Empty;
        private string _testUser = string.Empty;
        private string _email = string.Empty;
        protected HostApiFactory _factory;
        protected HttpClient _client;
        protected DataContext _context;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("Resources/auth.json", optional: true, reloadOnChange: false);
            var configurationRoot = config.Build();
            _testPassword = configurationRoot.GetSection("Password").Value;
            _testUser = configurationRoot.GetSection("User").Value;
            _email = configurationRoot.GetSection("Email").Value;

            var dbContextFactory = new ApplicationDbContextFactory();
            _context = dbContextFactory.CreateDbContext(Array.Empty<string>());

            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await LoadDataBaseConfigurations();
            _factory = new HostApiFactory();
            _client = _factory.CreateClient();
            var jwt = await RegisterTestClient(_client);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwt.Scheme, jwt.Token);
            _context.Dispose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        protected HttpClient GetUnauthorizedClient()
        {
            return _factory.CreateClient();
        }

        private async Task LoadDataBaseConfigurations()
        {
            var rolId = Guid.NewGuid();
            var doctorRolId = Guid.NewGuid();
            var date = DateTime.Now;
            var adminRoleSqlStatement = @"INSERT INTO Roles(UserCreated, UserUpdated, Name, Code, Active, Id, DateCreated, DateModified) VALUES('system', 'system', 'Super Admin', 'superadmin', 1, '{0}', '{1}', '{2}');";
            adminRoleSqlStatement = string.Format(adminRoleSqlStatement, rolId, date, date.ToString(), date, date.ToString());
            var doctorRoleSqlStatement = @"INSERT INTO Roles(UserCreated, UserUpdated, Name, Code, Active, Id, DateCreated, DateModified) VALUES('system', 'system', 'Doctor', 'doctor', 1, '{0}', '{1}', '{2}');";
            doctorRoleSqlStatement = string.Format(doctorRoleSqlStatement, doctorRolId, date, date.ToString(), date, date.ToString());
            adminRoleSqlStatement = string.Concat(adminRoleSqlStatement, doctorRoleSqlStatement);
            var insertPermissions = "INSERT INTO Permissions (UserCreated, UserUpdated, Active, RoleId, Name, Id, DateCreated, DateModified) VALUES ('system', 'system', 1, '{0}', '{1}', '{2}', '{3}', '{4}');\n";
            var permissions = string.Empty;
            foreach (var permissionItem in (PermissionName[])Enum.GetValues(typeof(PermissionName)))
            {
                permissions = string.Concat(permissions, string.Format(insertPermissions, rolId, permissionItem.GetDescription(), Guid.NewGuid(), date.ToString(), date, date.ToString()));
            }
            adminRoleSqlStatement = string.Concat(adminRoleSqlStatement, permissions, "SELECT * from Permissions;");
            _ = await _context.Permissions.FromSqlRaw(adminRoleSqlStatement).ToListAsync();

            _context.Database.ExecuteSqlRaw(Properties.Resources.BasicSql);
        }

        private async Task<AuthenticationModel> RegisterTestClient(HttpClient client)
        {
            var testUser = new RegisterUserApiRequest()
            {
                Name = _testUser,
                MiddleName = Guid.NewGuid().ToString()[..25],
                Surname = _testUser,
                SecondSurname = Guid.NewGuid().ToString()[..25],
                Document = "1111111",
                Password = _testPassword,
                Phone = "0991123456",
                Email = _email,
                Country = Country.Paraguay
            };
            var response = await client.PostAsJsonAsync("api/authentication/register", testUser);
            var content = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<AuthenticationModel>(content);
            return responseBody;
        }
    }
}
