using Bogus;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace XBoxLiveMock.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly List<Response> fakeResponse;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;

            var fakePagingInfo = new Faker<PagingInfo>()
            .RuleFor(p => p.ContinuationToken, f => f.Random.AlphaNumeric(10))
            .RuleFor(p => p.TotalItems, f => f.Random.Number(100, 1000));

            var fakeItems = new Faker<Items>()
                .RuleFor(i => i.Url, f => f.Internet.Url())
                .RuleFor(i => i.ItemType, f => f.PickRandom("Type1", "Type2", "Type3"))
                .RuleFor(i => i.TitleId, f => f.Random.AlphaNumeric(8))
                .RuleFor(i => i.Containers, f => f.Lorem.Word())
                .RuleFor(i => i.Obtained, f => f.Date.Past())
                .RuleFor(i => i.StartDate, f => f.Date.Past())
                .RuleFor(i => i.EndDate, f => f.Date.Future())
                .RuleFor(i => i.State, f => f.PickRandom("Active", "Inactive"));

            fakeResponse = new List<Response>
            {
                new Response
                {
                    PagingInfo = fakePagingInfo.Generate(),
                    Items = fakeItems.Generate()
                }
            };
        }

        [HttpGet("me/inventory")]
        public IActionResult Get() => Ok(fakeResponse);
    }
}
