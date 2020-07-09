﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using CommentsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommentsAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private const string TableName = "comments";
        private readonly IAmazonDynamoDB _amazonDynamoDb;

        public CommentsController(IAmazonDynamoDB amazonDynammoDb)
        {
            _amazonDynamoDb = amazonDynammoDb;
        }
        
        [HttpGet]
        public async Task<ScanResponse> GetComments()
        {
            var request = new ScanRequest
            {
                    TableName = TableName
            };

            var response = await _amazonDynamoDb.ScanAsync(request);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {
                        "id",
                        new AttributeValue
                        {
                            S = id.ToString()
                        }
                    }
                }
            };

            var response = await _amazonDynamoDb.GetItemAsync(request);

            return response.Item["username"].S;
        }

        [HttpPost]
        public async Task Post([FromBody] Comments comment)
        {
            var item = new Dictionary<string, AttributeValue>()
            {
                {"id", new AttributeValue{S = comment.Id}},
                {"username", new AttributeValue{S = comment.Username}},
                {"comment", new AttributeValue{S = comment.Comment}},
                {"postId", new AttributeValue{S = comment.PostId}},
                {"date", new AttributeValue{S = comment.Date}}
            };

            PutItemRequest request = new PutItemRequest
            {
                TableName = TableName,
                Item = item
            };
            await _amazonDynamoDb.PutItemAsync(request);
        }

        [HttpPut("{id}")] 
        public void Put([FromBody] int id, string username)
        {
            
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
