using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return _quotesDbContext.Quotes;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Quote Get(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            return quote;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
