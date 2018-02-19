using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
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

        [HttpGet("MigrateDb")]
        public string MigrateDb()
        {
            _context.Database.Migrate();

            return $"{_context.Database.ProviderName} applied";
        }

        [HttpGet("minio")]
        public async Task<List<string>> TestMinio()
        {
            var minio = new MinioClient("http://127.0.0.1:9000",
                "W9J5XIE4M3T0YVSR52V7",
                "xmrPyNhU2VolKQTrxYAKk+te4peBJZzSW1M7WxjU"
            );

            try
            {
                // Create bucket if it doesn't exist.
                bool found = await minio.BucketExistsAsync("mybucket");
                if (found)
                {
                    Console.Out.WriteLine("mybucket already exists");
                }
                else
                {
                    // Create bucket 'my-bucketname'.
                    await minio.MakeBucketAsync("mybucket");
                    Console.Out.WriteLine("mybucket is created successfully");
                }
            }
            catch (MinioException e)
            {
                Console.Out.WriteLine("Error occurred: " + e);
            }

            var getListBucketsTask = minio.ListBucketsAsync();
            var list = new List<string>();

            foreach (Bucket bucket in getListBucketsTask.Result.Buckets)
            {
                list.Add(bucket.Name + " " + bucket.CreationDate);
            }

            return list;
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
