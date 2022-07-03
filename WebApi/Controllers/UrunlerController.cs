using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")] // ~/api/Urunler
    [ApiController]
    public class UrunlerController : ControllerBase
    {
        private readonly IUrunService _urunService;
        public UrunlerController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        [HttpGet]
        public IActionResult Get() // ~/api/urunler
        {
            var model = _urunService.Query().ToList();
            return Ok(model); //200 http status code
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id) // ~/api/urunler/1
        {
            var model = _urunService.Query().SingleOrDefault(u => u.Id == id);
            if (model == null)
                return NotFound(); //404
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(UrunModel model) // ~/api/urunler
        {
            if (ModelState.IsValid)
            {
                var result = _urunService.Add(model);
                if (result.IsSuccessful)
                    return CreatedAtAction("Get", new { id = model.Id }, model); // return Ok(model) ; de diyebilirsin 
                ModelState.AddModelError("", result.Message);
            }
            return BadRequest(ModelState); // 400 client kaynaklı problem varsa bunu dönebilirsin
            /*return StatusCode(500); */// Internal server error sunucu hatası için

        }
        [HttpPut]
        public IActionResult Put(UrunModel model) // ~/api/urunler
        {
            if (ModelState.IsValid)
            {
                var result = _urunService.Update(model);
                if (result.IsSuccessful)
                    return NoContent(); //204
                ModelState.AddModelError("", result.Message);
            }
            return StatusCode(500, ModelState); // genelde badrequest dön , bunu örnek yaptık
        }
        [HttpDelete]
        public IActionResult Delete(int id) // ~/api/urunler/1
        {
            _urunService.Delete(id);
            return NoContent(); //204
        }
    }
}
