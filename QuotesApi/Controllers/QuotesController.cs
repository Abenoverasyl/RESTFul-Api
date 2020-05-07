using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Models;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        static List<Quote> _quotes = new List<Quote>()
        {
            new Quote{Id = 0, Author="Aben Era", Description="This is project", Title="Project one"},
            new Quote{Id = 1, Author="Aben Era2", Description="This is project2", Title="Project one2"},
            new Quote{Id = 2, Author="Aben Era3", Description="This is project3", Title="Project one3"}
        };

        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return _quotes;
        }

        [HttpPost]
        public void Post([FromBody]Quote quote)
        {
            _quotes.Add(quote);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Quote quote)
        {
            _quotes[id] = quote;
        }

        [HttpDelete("{id}")]
        public IEnumerable<Quote> Delete(int id)
        {
            _quotes.RemoveAt(id);
            return _quotes;
        }
    }
}