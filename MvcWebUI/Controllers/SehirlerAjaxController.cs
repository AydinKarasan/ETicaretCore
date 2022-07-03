using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    [Route("[controller]")] // SehirlerAjax  conroller içinde kendisi otomatik bulabiliyor
    public class SehirlerAjaxController : Controller
    {
        private readonly ISehirService _sehirService;
        public SehirlerAjaxController (ISehirService sehirService)
        {
            _sehirService = sehirService;
        }

        //[Route("CitiesGet/{ulkeId}")] // temel route yapısını değiştirdik önceden id = 1 olarak geliyordu şimdi ulkeId =1 olarak route çağırıyor
        [Route("SehirlerGet/{ulkeId?}")]
        public IActionResult SehirlerGet(int? ulkeId) //SehirlerAjax/SehirlerGet/1
        {
            var sehirler = _sehirService.Query().Where(s => s.UlkeId == ulkeId).ToList();
            return Json(sehirler);
        }
    }
}
