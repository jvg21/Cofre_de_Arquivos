
using FileSageAPI.Entities.Author.DTOs;
using FileSageAPI.Entities.Author.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FileSageAPI.Entities.Author.Layers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorController(IAuthorService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();
            return Ok(response.Adapt<List<ArchiveAuthorGetDTO>>());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok(response.Adapt<ArchiveAuthorGetDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(List<AuthorInsertDTO> dtos)
        {
            var response = await _service.InsertAuthor(dtos, true, true);
            return Ok(response.Adapt<List<ArchiveAuthorGetDTO>>());
        }

             [HttpPatch]
        public async Task<IActionResult> Update(List<AuthorUpdateDTO> dtos)
        {
            var response = await _service.UpdateAuthor(dtos, true, true);
            return Ok(response.Adapt<List<ArchiveAuthorGetDTO>>());
        }
    }
}
