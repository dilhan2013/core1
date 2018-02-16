﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EF;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly BloggingContext _context;

        public ValuesController(BloggingContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Blog>> Get()
        {

            return await _context.Blogs.ToListAsync();

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            var id = (await _context.Blogs.MaxAsync(a => a.BlogId));
            var blog = new Blog
            {
                BlogId = ++id,
                Url = "www.test.com.au " + Guid.NewGuid().ToString(),
                Rating = 5
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
