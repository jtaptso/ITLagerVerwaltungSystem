using Microsoft.AspNetCore.Mvc;
using ITLagerVerwaltungSystem.Core.Services;
using ITLagerVerwaltungSystem.Core.DTOs;

namespace ITLagerVerwaltungSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        // Inject IMaterialService via constructor
        private readonly IMaterialService _materialService;
        public MaterialsController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // GET: api/materials
        [HttpGet]
        public IActionResult GetAllMaterials()
        {
            var materials = _materialService.GetAllMaterials();
            return Ok(materials);
        }

        // GET: api/materials/{id}
        [HttpGet("{id}")]
        public IActionResult GetMaterial(int id)
        {
            var material = _materialService.GetMaterialById(id);
            if (material == null) return NotFound();
            return Ok(material);
        }


        // POST: api/materials (JSON only)
        [HttpPost]
        public IActionResult CreateMaterial([FromBody] MaterialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var created = _materialService.CreateMaterial(dto);
            return CreatedAtAction(nameof(GetMaterial), new { id = created.Id }, created);
        }

        // POST: api/materials/upload (multipart/form-data)
        [HttpPost("upload")]
        public async Task<IActionResult> CreateMaterialWithFiles([FromForm] MaterialDto dto, [FromForm] List<IFormFile> files)
        {
            var picturePaths = new List<string>();
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    picturePaths.Add($"/uploads/{fileName}");
                }
            }
            dto.PicturePaths = picturePaths;
            var created = _materialService.CreateMaterial(dto);
            return CreatedAtAction(nameof(GetMaterial), new { id = created.Id }, created);
        }

        // PUT: api/materials/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateMaterial(int id, [FromBody] MaterialDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = _materialService.UpdateMaterial(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/materials/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteMaterial(int id)
        {
            var deleted = _materialService.DeleteMaterial(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // PATCH: api/materials/{id}/status
        [HttpPatch("{id}/status")]
        public IActionResult UpdateMaterialStatus(int id, [FromBody] MaterialStatusDto dto)
        {
            var updated = _materialService.UpdateMaterialStatus(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
}
