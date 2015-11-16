using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace SelfHostedRestService.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet()]
        public HttpContent Index()
        {
            var page = @"<!DOCTYPE html>

<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
<head>
    <meta charset='utf-8' />
    <title>Alias Game</title>
</head>
<body>
    <button onclick='javascript:alert(\'123\')'>Push me!</button>
</body>
</html>";

            return new StringContent(page, System.Text.Encoding.UTF8, "text/html");
        }
    }
}
