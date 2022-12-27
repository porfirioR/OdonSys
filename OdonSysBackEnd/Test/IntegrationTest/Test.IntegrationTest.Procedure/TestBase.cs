using Access.Sql;
using Contract.Admin.Auth;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.Extensions;

namespace AcceptanceTest.Host.Api
{
    [TestFixture(Category = "Acceptance")]
    internal class TestBase
    {
        private static string _testPassword = "123456";
        private static string _testUser = "admin";
        protected HostApiFactory _factory;
        protected HttpClient _client;
        protected DataContext _context;
        public IEnumerable<string> TeethIds;


        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var dbContextFactory = new ApplicationDbContextFactory();
            _context = dbContextFactory.CreateDbContext(Array.Empty<string>());

            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await LoadDataBaseConfigurations();
            _factory = new HostApiFactory();
            _client = _factory.CreateClient();
            await RegisterTestClient(_client);
            var jwt = await GetJwtAuthenticationAsync(_client);
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
            var sqlStatement = @"INSERT INTO Roles(UserCreated, UserUpdated, Name, Code, Active, Id, DateCreated, DateModified) VALUES('system', 'system', 'SuperAdmin', 'superadmin', 1, '{0}', '{1}', '{2}');";
            sqlStatement = string.Format(sqlStatement, rolId, date, date.ToString(), date, date.ToString());
            var sqlStatementDoctor = @"INSERT INTO Roles(UserCreated, UserUpdated, Name, Code, Active, Id, DateCreated, DateModified) VALUES('system', 'system', 'doctor', 'doctor', 1, '{0}', '{1}', '{2}');";
            sqlStatementDoctor = string.Format(sqlStatementDoctor, doctorRolId, date, date.ToString(), date, date.ToString());
            sqlStatement = string.Concat(sqlStatement, sqlStatementDoctor);
            var insertPermissions = "INSERT INTO Permissions (UserCreated, UserUpdated, Active, RoleId, Name, Id, DateCreated, DateModified) VALUES ('system', 'system', 1, '{0}', '{1}', '{2}', '{3}', '{4}');\n";
            var permitions = string.Empty;
            foreach (var permissionItem in (PermissionName[])Enum.GetValues(typeof(PermissionName)))
            {
                permitions = string.Concat(permitions, string.Format(insertPermissions, rolId, permissionItem.GetDescription(), Guid.NewGuid(), date.ToString(), date, date.ToString()));
            }
            sqlStatement = string.Concat(sqlStatement, permitions, "SELECT * from Permissions;");
            var permissions = await _context.Permissions.FromSqlRaw(sqlStatement).ToListAsync();


            TeethIds = (await _context.Teeth.FromSqlRaw(Properties.Resources.BasicSql).ToListAsync()).Select(x => x.Id.ToString());
        }

        private Task RegisterTestClient(HttpClient client)
        {
            throw new NotImplementedException();
        }

        private static async Task<AuthModel> GetJwtAuthenticationAsync(HttpClient httpClient)
        {
            var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_testUser}:{_testPassword}"));
            httpClient.DefaultRequestHeaders.Remove("authorization");
            httpClient.DefaultRequestHeaders.Add("authorization", $"Basic {encoded}");
            var response = await httpClient.PostAsync("api/authentication/login", null);
            var responseBody = JsonConvert.DeserializeObject<AuthModel>(await response.Content.ReadAsStringAsync());
            httpClient.DefaultRequestHeaders.Remove("authorization");
            return responseBody;
        }
    }
}
