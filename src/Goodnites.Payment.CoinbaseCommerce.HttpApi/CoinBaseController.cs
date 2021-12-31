using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Volo.Abp;

namespace Goodnites.Payment.CoinbaseCommerce
{
    [RemoteService(Name = "GoodnitesCoinBase")]
    [Route("/api/coinbase")]
    public class CoinBaseController : CoinbaseCommerceController
    {
        private readonly ICoinBaseApiService _coinBaseApiService;
        private readonly ILogger<CoinBaseController> _logger;

        public CoinBaseController(ICoinBaseApiService coinBaseApiService, ILogger<CoinBaseController> logger)
        {
            _coinBaseApiService = coinBaseApiService;
            _logger = logger;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("webhook")]
        public async Task<IActionResult> CoinBaseWebHookAsync()
        {

#if !DEBUG
                   See : https://commerce.coinbase.com/docs/api/#webhooks
            if (Request.HttpContext.Connection.RemoteIpAddress == null)
            {
                return BadRequest();
            }
            
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (ip.Contains("54.175.255.")==false)
            {
                return BadRequest();
            }

            var lastBlock = int.Parse(ip.Substring(ip.LastIndexOf("."), ip.Length));
            if (!(lastBlock is >= 192 and <= 223))
            {
                return BadRequest();
            }
#endif

            var webHookSignature = Request.Headers[HeaderNames.WebhookSignature];

            using var reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true);

            var bodyStr = await reader.ReadToEndAsync();

            try
            {
                await _coinBaseApiService.WebHookAsync(bodyStr, webHookSignature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok();
        }
    }
}