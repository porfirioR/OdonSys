using Access.Sql;
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
            //var sqlStatement =
            //    @"
            //        CREATE FULLTEXT CATALOG AccountReplicationCatalog;
            //        CREATE FULLTEXT INDEX ON AccountReplication
            //        (
            //            AccountNumber
            //            Language 1033,
            //            LegalName
            //            Language 1033,
            //            DisplayName
            //            Language 1033
            //        )
            //        KEY INDEX [PK_AccountReplication] ON AccountReplicationCatalog;
            //        INSERT INTO StampCustomFieldSetters (CustomFieldId, StampId, Value, StampValidation) VALUES (1, 1, 'New', 2);
            //        ";

            //var sqlStatementRoleFunctionPoint = "INSERT INTO RoleFunctionPoints (UserCreated, UserUpdated, RoleId, FunctionPoint) VALUES ('test', 'test', 1,'{0}');\n";
            //var allRolesFunctionPoints = string.Empty;
            //foreach (var item in (FunctionPointType[])Enum.GetValues(typeof(FunctionPointType)))
            //{
            //    allRolesFunctionPoints = string.Concat(allRolesFunctionPoints, string.Format(sqlStatementRoleFunctionPoint, item.GetDescription()));
            //}
            //sqlStatement = string.Concat(sqlStatement, allRolesFunctionPoints, "SELECT * from RoleFunctionPoints");
            //await _context.RoleFunctionPoints.FromSqlRaw(sqlStatement).ToListAsync();
            TeethIds = (await _context.Teeth.FromSqlRaw(Properties.Resources.BasicSql).ToListAsync()).Select(x => x.Id.ToString());
        }
    }
}
