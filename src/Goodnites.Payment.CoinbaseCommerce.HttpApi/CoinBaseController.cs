using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Volo.Abp;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [RemoteService(Name = "GoodnitesCoinBase")]
    [Microsoft.AspNetCore.Components.Route("/api/coinbase")]
    public class CoinBaseController : CoinbaseCommerceController
    {
        private readonly ICoinBaseApiService _coinBaseApiService;

        public CoinBaseController(ICoinBaseApiService coinBaseApiService)
        {
            _coinBaseApiService = coinBaseApiService;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("webhook")]
        public async Task<IActionResult> CoinBaseWebHookAsync([FromBody] Webhook webhook)
        {
            var webHookSignature = Request.Headers[HeaderNames.WebhookSignature];

            await _coinBaseApiService.WebHookAsync(webhook, webHookSignature);
            
            return Ok();
        }
    }
}