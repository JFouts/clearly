using Clearly.EventSubscription;
using Microsoft.AspNetCore.Mvc;

namespace Questionable.Startup.System
{
    [Produces("application/json")]
    [Route("system")]
    public class SystemController : Controller
    {
        private readonly ISubscriptionStatistics _statistics;

        public SystemController(ISubscriptionStatistics statistics)
        {
            _statistics = statistics;
        }

        [HttpGet("subscriptions/start")]
        public IActionResult GetSubscriptionStart()
        {
            return Ok(_statistics.StartedTime);
        }

        [HttpGet("subscriptions/count")]
        public IActionResult GetNumberOfSubscription()
        {
            return Ok(_statistics.NumberOfActiveSubscriptions);
        }

        [HttpGet("subscriptions/last")]
        public IActionResult GetLastProcessTime()
        {
            return Ok(_statistics.LastProcessesTime);
        }

        [HttpGet("subscriptions/events")]
        public IActionResult GetNumberOfEvents()
        {
            return Ok(_statistics.NumberOfEventsProcessed);
        }
    }
}