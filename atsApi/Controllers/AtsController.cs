using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using atsApi.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace atsApi.Controllers
{
    public class AtsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var table = GetUserLogTable();
            TableQuery<UserLogEntity> query = new TableQuery<UserLogEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "1"));

            return Ok(table.ExecuteQuery(query));
        }

        public IHttpActionResult Post([FromBody] User entity)
        {
            UserLogEntity newLogEntity = new UserLogEntity(entity.Username, entity.Id.ToString());
            var table = GetUserLogTable();
            TableOperation insertOperation = TableOperation.Insert(newLogEntity);

            // Execute the insert operation.
            table.Execute(insertOperation);

            return Created(string.Empty, newLogEntity);
        }

        private CloudTable GetUserLogTable()
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               ConfigurationManager.ConnectionStrings["Ats"].ConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference(ConfigurationManager.AppSettings["userLogTableName"]);
            table.CreateIfNotExists();

            return table;
        }

    }
}
