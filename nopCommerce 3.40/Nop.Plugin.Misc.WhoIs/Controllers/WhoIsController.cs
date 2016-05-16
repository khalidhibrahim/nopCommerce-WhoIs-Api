using Newtonsoft.Json;
using Nop.Core.Data;
using Nop.Plugin.Misc.WhoIs.Domain;
using Nop.Web.Framework.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.WhoIs.Controllers
{
    public class WhoIsController : BaseController
    {
        #region Fields

        private readonly IRepository<WhoIsRecord> _WhoIsRepository;

        #endregion

        #region Ctor

        public WhoIsController(IRepository<WhoIsRecord> WhoIsRepository)
        {
            _WhoIsRepository = WhoIsRepository;
        }

        #endregion

        [HttpPost]
        public ActionResult GetWhoIs(string ipaddress)
        {
            WhoIsRecord cache = _WhoIsRepository.Table.SingleOrDefault(x => x.IpAddress == ipaddress);

            if (cache == null)
            {
                string HtmlResult = string.Empty;
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        HtmlResult = wc.DownloadString(string.Format("http://ipinfo.io/{0}/json", ipaddress));
                    }
                }
                catch
                {
                    //probably The remote server returned an error: (429) Too Many Requests.
                }

                if (HtmlResult != string.Empty)
                {
                    dynamic dynObj = JsonConvert.DeserializeObject(HtmlResult);

                    cache = new WhoIsRecord()
                    {
                        IpAddress = dynObj.ip,
                        City = dynObj.city,
                        Hostname = dynObj.hostname == "No Hostname" ? string.Empty : dynObj.hostname,
                        Region = dynObj.region,
                        CountryCode = dynObj.country,
                        CompanyName = dynObj.org,
                        TimeStamp = DateTime.UtcNow
                    };

                    _WhoIsRepository.Insert(cache);

                    return Json(new
                    {
                        City = cache.City,
                        Hostname = cache.Hostname,
                        Region = cache.Region,
                        CountryCode = cache.CountryCode,
                        CompanyName = cache.CompanyName
                    });
                }

                return ErrorOccured("Error.");
            }
            else
            {              
                return Json(new
                {
                    City = cache.City,
                    Hostname = cache.Hostname,
                    Region = cache.Region,
                    CountryCode = cache.CountryCode,
                    CompanyName = cache.CompanyName
                });
            }
        }

        public ActionResult ErrorOccured(string errorMessage)
        {
            return Json(new
            {
                success = false,
                errorMessage = errorMessage
            });
        }

    }
}