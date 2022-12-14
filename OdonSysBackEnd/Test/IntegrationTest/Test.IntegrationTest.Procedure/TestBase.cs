using Access.Sql;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.Extensions;

namespace AcceptanceTest.Host.Api
{
    [TestFixture(Category = "Acceptance")]
    internal class TestBase
    {
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
            //var jwt = await GetJwtAuthenticationAsync(_client);
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwt.TokenType, jwt.AccessToken);
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

        //private async Task<JwtAuthenticationModel> GetJwtAuthenticationAsync(HttpClient httpClient)
        //{
        //    var username = "admin";
        //    var password = "123456";
        //    var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        //    httpClient.DefaultRequestHeaders.Remove("authorization");
        //    httpClient.DefaultRequestHeaders.Add("authorization", $"Basic {encoded}");
        //    var response = await httpClient.PostAsync("api/login", null);
        //    var responseBody = JsonConvert.DeserializeObject<JwtAuthenticationModel>(await response.Content.ReadAsStringAsync());
        //    httpClient.DefaultRequestHeaders.Remove("authorization");
        //    return responseBody;
        //}

        private async Task LoadDataBaseConfigurations()
        {
            var rolId = Guid.NewGuid();
            var sqlStatement =
                @"
                    INSERT INTO Roles(UserCreated, UserUpdated, Name, Code, Active, Id) VALUES('system', 'system', 'SuperAdmin', 'superadmin', 1, '{0}');
                ";
            sqlStatement = string.Format(sqlStatement, rolId);

            var sqlStatementPermitions = "INSERT INTO Permissions (UserCreated, UserUpdated, RoleId, FunctionPoint) VALUES ('test', 'test', 1,'{0}');\n";
            var permitions = string.Empty;
            foreach (var item in (PermissionName[])Enum.GetValues(typeof(PermissionName)))
            {
                permitions = string.Concat(permitions, string.Format(sqlStatementPermitions, item.GetDescription()));
            }
            sqlStatement = string.Concat(sqlStatement, permitions, "SELECT * from Permissions");
            await _context.Permissions.FromSqlRaw(sqlStatement).ToListAsync();
            TeethIds = (await _context.Teeth.FromSqlRaw(Properties.Resources.BasicSql).ToListAsync()).Select(x => x.Id.ToString());
        }
    }
}
