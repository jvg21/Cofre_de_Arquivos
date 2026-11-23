
using FileSageAPI.Entities.Tag.DTOs;
using FileSageAPI.Entities.Tag.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FileSageAPI.Entities.Tag.Layers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();
            return Ok(response.Adapt<List<TagGetDTO>>());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok(response.Adapt<TagGetDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(List<TagInsertDTO> dtos)
        {
            var response = await _service.Insert(dtos, true,true);
            return Ok(response.Adapt<List<TagGetDTO>>());
        }

        [HttpPatch]
        public async Task<IActionResult> Update(List<TagUpdateDTO> dtos)
        {
            var response = await _service.Update(dtos,true, true);
            return Ok(response.Adapt<List<TagGetDTO>>());
        }


        //[HttpDelete]
        //public async Task<IActionResult> Delete(List<int> ids)
        //{
        //    var response = await _service.Delete(ids, true);
        //    return Ok(response);
        //}
    }
}
