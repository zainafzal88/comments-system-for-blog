using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
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
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
