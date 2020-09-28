using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
namespace graphQL_test.Controllers
{

    [ApiController]
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public GraphqlErrors _errors;
        public GraphQLController(ISchema schema, IDocumentExecuter executer, GraphqlErrors errors)
        {
            _schema = schema;
            _executer = executer;
            _errors = errors;
        }
        [HttpPost]
        public async Task<object> Post([FromBody] System.Text.Json.JsonElement _query)
        {
            string rawJson = _query.ToString();
            var query = JsonConvert.DeserializeObject<GraphQLQueryDTO>(rawJson);
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables?.ToInputs();
            });

            if (result.Errors?.Count > 0)
            {
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    _errors.Errors.Add(result.Errors[i].Message);
                }
                return _errors;
            }

            return Ok(result);
        }
    }
}