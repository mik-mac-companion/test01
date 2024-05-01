using Microsoft.AspNetCore.Mvc;
using XboxLiveMock.Models;
using Bogus;

namespace XboxLiveMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        Faker<PagingInfo> fakePagingInfo = new Faker<PagingInfo>()
            .RuleFor(p => p.ContinuationToken, f => f.Random.AlphaNumeric(10))
            .RuleFor(p => p.TotalItems, f => f.Random.Number(100, 1000));

        Faker<Items> fakeItems = new Faker<Items>()
            .RuleFor(i => i.Url, f => f.Internet.Url())
            .RuleFor(i => i.ItemType, f => f.PickRandom("Type1", "Type2", "Type3"))
            .RuleFor(i => i.TitleId, f => f.Random.AlphaNumeric(8))
            .RuleFor(i => i.Containers, f => f.Lorem.Word())
            .RuleFor(i => i.Obtained, f => f.Date.Past())
            .RuleFor(i => i.StartDate, f => f.Date.Past())
            .RuleFor(i => i.EndDate, f => f.Date.Future())
            .RuleFor(i => i.State, f => f.PickRandom("Active", "Inactive"));

        List<Response> fakeResponse = new();

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;

            fakeResponse.Add(new()
            {
                PagingInfo = fakePagingInfo.Generate(),
                Items = fakeItems.Generate()
            });
        }

        [HttpGet(Name = "me/inventory")]
        public IEnumerable<Response> Get() => fakeResponse;
    }
}
