using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AzureTwitter.Storage.Interfaces.Repositories;
using AzureTwitter.Storage.Models;

namespace AzureTwitter.Api.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        private readonly ITweetsRepository _tweetsRepository;

        public TweetsController(ITweetsRepository tweetsRepository)
        {
            _tweetsRepository = tweetsRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TweetModel> Get()
        {
            return _tweetsRepository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TweetModel Get(string id)
        {
            return _tweetsRepository.Get(id);
        }
    }
}
