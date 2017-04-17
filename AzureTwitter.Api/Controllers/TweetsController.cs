using System.Collections.Generic;
using AzureTwitter.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using AzureTwitter.Storage.Interfaces.Repositories;
using AzureTwitter.Storage.Models;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace AzureTwitter.Api.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        private readonly ITweetsRepository _tweetsRepository;
        private readonly IConnectionManager _connectionManager;

        public TweetsController(ITweetsRepository tweetsRepository, IConnectionManager connectionManager)
        {
            _tweetsRepository = tweetsRepository;
            _connectionManager = connectionManager;
        }

        [HttpGet]
        public IEnumerable<TweetModel> Get()
        {
            return _tweetsRepository.Get();
        }

        [HttpGet("{id}")]
        public TweetModel Get(string id)
        {
            return _tweetsRepository.Get(id);
        }

        [HttpPost]
        public void Post()
        {
            _connectionManager.GetHubContext<TweetHub>().Clients.All.newTweet(new TweetModel
            {
                User = "User 1",
                Content = "Content 1",
                Id = "1"
            });
        }
    }
}
