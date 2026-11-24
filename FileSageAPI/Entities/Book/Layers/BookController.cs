using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Book.DTOs;
using FileSageAPI.Entities.Book.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FileSageAPI.Entities.Book.Layers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();
            return Ok(response.Adapt<List<ArchiveBookGetDTO>>());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok(response.Adapt<ArchiveBookGetDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(List<BookInsertDTO> dtos)
        {
            var response = await _service.InsertBook(dtos, true, true);
            return Ok(response.Adapt<List<ArchiveBookGetDTO>>());
        }

        [HttpPatch]
        public async Task<IActionResult> Update(List<BookUpdateDTO> dtos)
        {
            var response = await _service.UpdateBook(dtos, true, true);
            return Ok(response.Adapt<List<ArchiveBookGetDTO>>());
        }

    }
}
