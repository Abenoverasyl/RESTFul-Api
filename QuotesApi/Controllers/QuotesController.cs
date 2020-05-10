using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Quotes
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;
            switch (sort)
            {
                case "desc":
                    quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quotesDbContext.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quotesDbContext.Quotes;
                    break;
            }
            return Ok(quotes);
        }


        [HttpGet("[action]")]
        public IActionResult PagingQuote(int? pageNumber, int? pageSize)
        {
            var quotes = _quotesDbContext.Quotes.OrderBy(q => q.Id);
            var curPageNumb = pageNumber ?? 1;
            var curPageSize = pageSize ?? 1;
            return Ok(quotes.Skip((curPageNumb - 1) * curPageSize).Take(curPageSize));
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult SeachQuote(string type)
        {
            var quotes = _quotesDbContext.Quotes.Where(q => q.Type.StartsWith(type));
            return Ok(quotes);
        }


        // GET api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No record found..");
            }
            return Ok(quote);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Quote quote)
        {
            var current = _quotesDbContext.Quotes.Find(id);
            if (current == null)
            {
                return NotFound("No record found..");
            }
            else
            {
                current.Title = quote.Title;
                current.Author = quote.Author;
                current.Description = quote.Description;
                current.Type = quote.Type;
                current.CreatedAt = quote.CreatedAt;
                _quotesDbContext.SaveChanges();
                return Ok("Updated Successfully!");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("Not Found!");
            }
            _quotesDbContext.Quotes.Remove(quote);
            _quotesDbContext.SaveChanges();
            return Ok("Quote Deleted!");
        }
    }
}
