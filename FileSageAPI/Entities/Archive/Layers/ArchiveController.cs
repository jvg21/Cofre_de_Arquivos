using FileSageAPI.Entities.Archive.DTOs;
using FileSageAPI.Entities.Archive.Interfaces;

using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FileSageAPI.Entities.Archive.Layers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchiveService _service;

        public ArchiveController(IArchiveService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();
            return Ok(response.Adapt<List<ArchiveGetDTO>>());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok(response.Adapt<ArchiveGetDTO>());
        }


        //[HttpDelete]
        //public async Task<IActionResult> Delete(List<int> archives)
        //{
        //    var response = await _service.Delete(archives, true, true);
        //    return Ok(response.Adapt<List<ArchiveGetDTO>>());
        //}
    }
}
