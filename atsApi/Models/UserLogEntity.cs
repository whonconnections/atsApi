using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace atsApi.Models
{
    public class UserLogEntity : TableEntity
    {
        public UserLogEntity(string username, string userId)
        {
            this.PartitionKey = "1";
            this.Username = username;
            this.Id = userId;
        }

        public UserLogEntity() { }

        public string Username { get; set; }

        public string Id { get; set; }
    }
}