using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using System.Text;

namespace HtmlPartitionSync
{
    public static class PartitionJs
    {
        [FunctionName("PartitionJs")]

        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var kvPairs = req.GetQueryNameValuePairs();
            var paramUrl = kvPairs.FirstOrDefault(kv => kv.Key == "url").Value;
            var paramXPath = kvPairs.FirstOrDefault(kv => kv.Key == "xpath").Value;
            var url = HttpUtility.UrlDecode(paramUrl);
            var xpath = HttpUtility.UrlDecode(paramXPath);
            var htmlStr = "";

            using (var client = new HttpClient())
            {
                htmlStr = await client.GetStringAsync(url);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);
            var linkNodeList = doc.DocumentNode.SelectNodes("//link");
            var xpathNode = doc.DocumentNode.SelectSingleNode(xpath);

            var sb = new StringBuilder();

            foreach (var linkNode in linkNodeList)
            {
                var linkNodeWriteStr = $"document.write('{linkNode.OuterHtml.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("'", "\\'")}')";
                sb.AppendLine(linkNodeWriteStr);
            }

            var xpathNodeWriteStr = $"document.write('{xpathNode.OuterHtml.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("'", "\\'")}')";
            sb.AppendLine(xpathNodeWriteStr);
            var res = req.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(sb.ToString());
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("text/javascript");
            res.Content.Headers.ContentType.CharSet = "utf-8";
            return res;
        }
    }
}



//https://htmlpartitionsync.azurewebsites.net/api/PartitionJs?url=https%3A%2F%2Fgithub.com%2FHyundongHwang%2Fhhdps%2Fblob%2Fmaster%2FREADME.md&xpath=%252F%252Farticle

//https://github.com/HyundongHwang/hhdps

//https://github.com/HyundongHwang/hhdps/blob/master/README.md
//https%3A%2F%2Fgithub.com%2FHyundongHwang%2Fhhdps%2Fblob%2Fmaster%2FREADME.md

////article
//%252F%252Farticle

//http://localhost:7071/api/PartitionJs
//http://localhost:7071/api/PartitionJs?url=https%3A%2F%2Fgithub.com%2FHyundongHwang%2Fhhdps%2Fblob%2Fmaster%2FREADME.md&xpath=%252F%252Farticle
//https://www.w3schools.com/xml/xpath_syntax.asp
//https://www.codeproject.com/Articles/691119/Html-Agility-Pack-Massive-information-extraction-f



//GET https://gist.github.com/HyundongHwang/abddb7e919324a251eae.js HTTP/1.1
//Host: gist.github.com
//Connection: keep-alive
//Upgrade-Insecure-Requests: 1
//User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36
//Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
//Accept-Encoding: gzip, deflate, sdch, br
//Accept-Language: ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4,pt;q=0.2
//Cookie: _octo=GH1.1.1199769929.1492352428; logged_in=yes; dotcom_user=HyundongHwang; _gat=1; gist_user_session=bqsuAxcK8gyJjCMFgI-5dsWxL-B9gJ98n_mENExNYiU2EKQG; __Host-gist_user_session_same_site=bqsuAxcK8gyJjCMFgI-5dsWxL-B9gJ98n_mENExNYiU2EKQG; _ga=GA1.2.996522895.1492352428; tz=Asia%2FSeoul


//HTTP/1.1 200 OK
//Server: GitHub.com
//Date: Wed, 17 May 2017 15:22:10 GMT
//Content-Type: text/javascript; charset=utf-8
//Status: 200 OK
//Cache-Control: no-cache
//Vary: X-PJAX
//X-UA-Compatible: IE=Edge,chrome=1
//Set-Cookie: gist_user_session=bqsuAxcK8gyJjCMFgI-5dsWxL-B9gJ98n_mENExNYiU2EKQG; path=/; expires=Wed, 31 May 2017 15:22:10 -0000; secure; HttpOnly
//Set-Cookie: __Host-gist_user_session_same_site=bqsuAxcK8gyJjCMFgI-5dsWxL-B9gJ98n_mENExNYiU2EKQG; path=/; expires=Wed, 31 May 2017 15:22:10 -0000; secure; HttpOnly; SameSite=Strict
//X-Request-Id: 154379e1fc81881b7f3b8cb41663cf9a
//X-Runtime: 0.038644
//Content-Security-Policy: default-src 'none'; base-uri 'self'; block-all-mixed-content; child-src render.githubusercontent.com; connect-src 'self' uploads.github.com status.github.com collector.githubapp.com api.github.com www.google-analytics.com github-cloud.s3.amazonaws.com github-production-repository-file-5c1aeb.s3.amazonaws.com github-production-user-asset-79cafe.s3.amazonaws.com wss://live.github.com; font-src assets-cdn.github.com; form-action 'self' github.com gist.github.com; frame-ancestors 'none'; img-src 'self' data: assets-cdn.github.com identicons.github.com collector.githubapp.com github-cloud.s3.amazonaws.com *.githubusercontent.com; media-src 'none'; script-src assets-cdn.github.com; style-src 'unsafe-inline' assets-cdn.github.com
//Strict-Transport-Security: max-age=31536000; includeSubdomains; preload
//Public-Key-Pins: max-age=5184000; pin-sha256="WoiWRyIOVNa9ihaBciRSC7XHjliYS9VwUGOIud4PB18="; pin-sha256="RRM1dGqnDFsCJXBTHky16vi1obOlCgFFn/yOhI/y+ho="; pin-sha256="k2v657xBsOVe1PQRwOsHsw3bsGT2VzIqz5K+59sNQws="; pin-sha256="K87oWBWM9UZfyddvDfoxL+8lpNyoUB2ptGtn0fv6G2Q="; pin-sha256="IQBnNBEiFuhj+8x6X8XLgh01V9Ic5/V3IRQLNFFc7v4="; pin-sha256="iie1VXtL7HzAMF+/PVPR9xzT80kQxdZeJ+zduCB3uj0="; pin-sha256="LvRiGEjRqfzurezaWuj8Wie2gyHMrW5Q06LspMnox7A="; includeSubDomains
//X-Content-Type-Options: nosniff
//X-Frame-Options: deny
//X-XSS-Protection: 1; mode=block
//Vary: Accept-Encoding
//X-Served-By: 9660edcd10921168c9f5df4333b06e59
//X-GitHub-Request-Id: 242D:32C2:A0CBE0:FD01D6:591C6AA2
//Content-Length: 1319

//document.write('<link rel="stylesheet" href="https://assets-cdn.github.com/assets/gist-embed-9f0a4ad9c85ca776e669003688baa9d55f9db315562ce4d231d37dab2714c70a.css">')
//document.write('<div id=\"gist27470833\" class=\"gist\">\n    <div class=\"gist-file\">\n      <div class=\"gist-data\">\n        <div class=\"js-gist-file-update-container js-task-list-container file-box\">\n  <div id=\"file-mytest-md\" class=\"file\">\n    \n  <div id=\"readme\" class=\"readme blob instapaper_body\">\n    <article class=\"markdown-body entry-content\" itemprop=\"text\"><p>�׽�Ʈ<\/p>\n<ul>\n<li>\n<p>asdf<\/p>\n<\/li>\n<li>\n<p>zxcv<\/p>\n<\/li>\n<\/ul>\n<hr>\n<div class=\"highlight highlight-source-java\"><pre><span class=\"pl-k\">int<\/span> var <span class=\"pl-k\">=<\/span> <span class=\"pl-c1\">123<\/span>;<\/pre><\/div>\n<\/article>\n  <\/div>\n\n  <\/div>\n  \n<\/div>\n\n      <\/div>\n      <div class=\"gist-meta\">\n        <a href=\"https://gist.github.com/HyundongHwang/abddb7e919324a251eae/raw/3bdeb38349bcca559bf4fc810166de79ba12994a/Mytest.md\" style=\"float:right\">view raw<\/a>\n        <a href=\"https://gist.github.com/HyundongHwang/abddb7e919324a251eae#file-mytest-md\">Mytest.md<\/a>\n        hosted with &#10084; by <a href=\"https://github.com\">GitHub<\/a>\n      <\/div>\n    <\/div>\n<\/div>\n')

