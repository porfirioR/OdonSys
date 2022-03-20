using Access.Sql;
using DT.Host.Api.Acceptance.Test;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
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
        public const int ExistingDocumentId = 1;
        public const int ExistingTicketId = 2;
        public const int ExistingNotificationId = 3;
        public const int ExistingTransactionCategoryId = 1;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var dbContextFactory = new ApplicationDbContextFactory();
            _context = dbContextFactory.CreateDbContext(new string[] { });

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
            //        INSERT INTO Roles (UserCreated, UserUpdated, Name, Code, Active) VALUES ('test', 'test', 'SuperAdmin', 'superadmin', 1);
            //        INSERT INTO CustomFields (UserCreated, UserUpdated, FieldName, ""Values"", Code, DocumentColumn, FieldType, DocumentTypes, ""Order"", Active) VALUES ('test', 'test', 'Status', 'New||Processing||Complete||Pay', 'status', 'CustomFieldString1', 6, '1', 2, 1);
            //        INSERT INTO CustomFields (UserCreated, UserUpdated, FieldName, ""Values"", Code, DocumentColumn, FieldType, DocumentTypes, ""Order"", Active) VALUES ('test', 'test', 'Document Type', 'Account Application||Annual List of Officers||Bill||Check||Deposit - Check||Deposit - Wire||ID||Fee invoice', 'document-type', 'CustomFieldString2', 6, '1', 1, 1);
            //        INSERT INTO CustomFields (UserCreated, UserUpdated, FieldName, Code, DocumentColumn, FieldType, DocumentTypes, ""Order"", Active) VALUES ('test', 'test', 'Account No', 'account-no', 'CustomFieldString3', 1, '1,2,3', 3, 1);
            //        INSERT INTO CustomFields (UserCreated, UserUpdated, FieldName, ""Values"", Code, DocumentColumn, FieldType, DocumentTypes, ""Order"", Active) VALUES ('test', 'test', 'Status', 'Closed||New||Open||Pending||Pending-Client||Pending-External||Pending-Internal||Working', 'ticket-status', 'CustomFieldString4', 6, 2, 3, 1);
            //        INSERT INTO Documents (UserCreated, UserUpdated, Type, Container, Format, OriginalFileName, SystemFileName, Active, FileSizeBytes, HasVersions, Deleted, CustomFieldString1, CustomFieldString2, CustomFieldString3, CustomFieldString4, CustomFieldString5, CustomFieldString7, CustomFieldString8, CustomFieldString9, CustomFieldBool1, CustomFieldBool2, CustomFieldDecimal6, DepartmentId, AssignedTo, Email, Subject, Description, [To], [From]) VALUES ('test', 'test', 1, 'Documents', '.pdf', 'test.pdf', 'test.pdf', 1, 620260, 0, 0, 'New', 'Account Application', '5780283', 'Sms', 'T Rowe Price', '2020', '12345678', '2110 - Rental Lease Income',0, 0,1000.00000000,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
            //        INSERT INTO Documents (UserCreated, UserUpdated, Type, Active, FileSizeBytes, HasVersions, Deleted, CustomFieldString1, CustomFieldString2, CustomFieldString3, CustomFieldString4, CustomFieldString5, CustomFieldString7, CustomFieldString8, CustomFieldString9, CustomFieldBool1, CustomFieldBool2, CustomFieldDecimal6, DepartmentId, AssignedTo, Email, Subject, Description, [To], [From]) VALUES ('test', 'test', 2, 1, 620260, 0, 0, 'New', 'Account Application', '5780283', 'Sms', 'T Rowe Price', '2020', '12345678', '2110 - Rental Lease Income',0, 0,1000.00000000,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
            //        INSERT INTO Documents (UserCreated, UserUpdated, Type, Active, FileSizeBytes, HasVersions, Deleted, CustomFieldString1, CustomFieldString2, CustomFieldString3, CustomFieldString4, CustomFieldString5, CustomFieldString7, CustomFieldString8, CustomFieldString9, CustomFieldBool1, CustomFieldBool2, CustomFieldDecimal6, DepartmentId, AssignedTo, Email, Subject, Description, [To], [From], Container) VALUES ('test', 'test', 3, 1, 620260, 0, 0, 'New', 'Account Application', '5780283', 'Sms', 'T Rowe Price', '2020', '12345678', '2110 - Rental Lease Income',0, 0,1000.00000000,NULL,NULL,NULL,NULL,NULL,NULL,NULL, 'Documents');
            //        INSERT INTO Departments (UserCreated, UserUpdated, Name, Code, Active) VALUES ('test', 'test','Department', 'department', 1);
            //        INSERT INTO EmailTemplate (UserCreated, UserUpdated, Code, Name, Content, Model, Subject, Title, EmailSubType) VALUES ('test', 'test', 'test-ticket-template','test ticket template','<p>Account Number : @Model.Ticket.AccountNumber</p><p>Id : @Model.Ticket.Id</p><p>Display Id : @Model.Ticket.DisplayId</p><p>Url : @Model.Ticket.Url</p><p>Subject : @Model.Ticket.Subject</p><p>User Name : @Model.Author.DisplayName</p>','DT.Common.Models.Template.TicketModel','Test Ticket Template','Test Ticket Template',3);
            //        INSERT INTO TicketingSupportEmail (UserCreated, UserUpdated, UserName, Password, DepartmentId, Code, Active) VALUES ('test', 'test', 'support-dev@digitaltrust.com', 'ThOdRoCH?!O$i2p-crE#', 1, 'test', 1);
            //        INSERT INTO AccountReplication (UserCreated, UserUpdated, AccountId, AccountNumber, LegalName, DisplayName, IsActive, AccountCategoryId, Code) VALUES ('test', 'test', 1, 'abc', 'test', 'test', 1, 1, 'test');
            //        INSERT INTO SlaDocumentStatus (UserCreated, UserUpdated, Name, SlaStatus, IsReplyStatus, NewTicketStatus) VALUES ('test', 'test', 'New', 1, 0, 1);
            //        INSERT INTO DocumentLists (UserCreated, UserUpdated, Name, Code, Columns, WhereStatement, OrderByStatement, Counter, Active, DocumentType, RulesWhereStatement) VALUES ('test', 'test', 'test-list', 'test-list', 'CustomFieldString1, Id','CustomFieldString1 = ''New''', 'Id Asc', 0, 1, 1, '');
            //        INSERT INTO AccountReplicationTransactionCategory (UserCreated, UserUpdated, TransactionCategoryId, Description) VALUES ('test', 'test', 1, 'test');
            //        INSERT INTO AccountReplicationCategory (UserCreated, UserUpdated, Description, AccountCategoryId) VALUES ('test', 'test', 'description', 1);
            //        INSERT INTO AccountRelationshipsReplication (UserCreated, UserUpdated, AccountRelationshipTypeName, AccountReplicationContactInfoId, AccountReplicationId) VALUES ('test', 'test', 'name', 1, 1);
            //        INSERT INTO AccountCustomFieldsReplication (UserCreated, UserUpdated, FieldType, LabelName, Value, AccountReplicationId) VALUES ('test', 'test', 6, 'label', 'value', 1);
            //        INSERT INTO AccountReplicationContactInfo (FirstName, MiddleName, LastName, DateOfBirth, EmailAddress, TaxId, IdentityCode, PostalAddressLine1, PostalAddressLine2, PostalAddressLine3, City, PostalCode, StateProvince, AccountReplicationContactInfoId, UserCreated, UserUpdated) VALUES ('firstname', 'middlename', 'lastname', NULL, 'email@email.com', 'tax', 'identity', 'adress1', 'adress2', 'adress3', 'city', '1234', 'province', 2, 'test', 'test');
            //        INSERT INTO Cache (Id, Value, ExpiresAtTime) VALUES ('DT.Access.Document.Data.Contract.DocumentList.IDocumentListDataAccess.GetAllAsync', convert(varbinary(max), '[{{""Id"":1,""Name"":""test-list"",""Code"":""test-list"",""ColumnNameList"":[""CustomFieldString1"","" Id""],""DocumentType"":1,""WhereStatement"":""CustomFieldString1 = New"",""WhereStatementParameters"":null,""OrderByStatement"":""Id Asc"",""Counter"":0,""Active"":true,""RulesWhereStatement"":null,""DateUpdated"":""2022-01-19T21:17:48.4966667""}}]'), '2022-01-19 21:48:02.5723957');
            //        INSERT INTO RoleDocumentLists (UserCreated, UserUpdated, RoleId, DocumentListId) VALUES ('test', 'test', 1, 1);
            //        INSERT INTO DocumentCopyJobs (UserCreated, UserUpdated, DocumentId, Status, Active) VALUES ('digitaltrust', 'test', 1, 3, 1);
            //        INSERT INTO Stamps (Name, Color, Code, StampType, Active, VisibilityRuleset, WhereStatement, DocumentType, CommentRequired, UserCreated, UserUpdated) VALUES ('test-stamp', '#3555b6', 'test-stamp', 1, 1, '{{""Condition"":0,""Rules"":[{{""field"":""CustomFieldString1"",""operator"":"" = "",""value"":""New""}}]}}', 'x.CustomFieldString1 == ''New'' ', 1, 0, 'test', 'test');
            //        INSERT INTO RoleStamps (RoleId, StampId, UserCreated, UserUpdated) VALUES (1, 1, 'test', 'test');
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
            await Task.Delay(1);
        }
    }
}
