using FileSageAPI.Entities.Url.DTOs;
using FileSageAPI.Entities.Url.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FileSageAPI.Entities.Url.Layers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlController: ControllerBase
    {
        private readonly IUrlService _service;

        public UrlController(IUrlService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();
            return Ok(response.Adapt<List<UrlGetDTO>>());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok(response.Adapt<UrlGetDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(List<UrlInsertDTO> dtos)
        {
            var response = await _service.Insert(dtos, true, true);
            return Ok(response.Adapt<List<UrlGetDTO>>());
        }

        [HttpPatch]
        public async Task<IActionResult> Update(List<UrlUpdateDTO> urls)
        {
            var response = await _service.Update(urls, true, true);
            return Ok(response.Adapt<List<UrlGetDTO>>());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(List<int> urls)
        {
            var response = await _service.Delete(urls, true, true);
            return Ok(response.Adapt<List<UrlGetDTO>>());
        }

    }
}
