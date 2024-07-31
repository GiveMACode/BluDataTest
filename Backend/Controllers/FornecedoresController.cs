using AutoMapper;
using Backend.Data;
using Backend.DTOs;
using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models.FornecedorModel;

namespace Backend.Controller;

// Controllers/FornecedoresController.cs
[Route("api/[controller]")]
[ApiController]
public class FornecedoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly FornecedorService _fornecedorService;

    public FornecedoresController(ApplicationDbContext context, IMapper mapper, FornecedorService fornecedorService)
    {
        _context = context;
        _mapper = mapper;
        _fornecedorService = fornecedorService;

    }

    // Implementar métodos CRUD para Fornecedores
    // GET: api/Fornecedores
    /// <summary>
/// Retrieves a list of Fornecedores based on the provided filters.
/// </summary>
/// <param name="nome">The name of the Fornecedor to filter by. If null or empty, no filter is applied.</param>
/// <param name="cpfCnpj">The CPF/CNPJ of the Fornecedor to filter by. If null or empty, no filter is applied.</param>
/// <param name="dataCadastro">The date of the Fornecedor's registration to filter by. If null, no filter is applied.</param>
/// <returns>An ActionResult containing a list of FornecedorDto objects that match the provided filters.</returns>
[HttpGet]
public async Task<ActionResult<IEnumerable<FornecedorDto>>> GetFornecedores([FromQuery] string nome, [FromQuery] string cpfCnpj, [FromQuery] DateTime? dataCadastro)
{
    var query = _context.Fornecedores.AsQueryable();

    if (!string.IsNullOrWhiteSpace(nome))
    {
        query = query.Where(f => f.Nome.Contains(nome));
    }

    if (!string.IsNullOrWhiteSpace(cpfCnpj))
    {
        query = query.Where(f => f.CPF_CNPJ == cpfCnpj);
    }

    if (dataCadastro.HasValue)
    {
        query = query.Where(f => f.DataHoraCadastro.Date == dataCadastro.Value.Date);
    }

    var fornecedores = await query.ToListAsync();
    return Ok(_mapper.Map<IEnumerable<FornecedorDto>>(fornecedores));
}

    // GET: api/Fornecedores/5
    /// <summary>
/// Retrieves a Fornecedor by its unique identifier.
/// </summary>
/// <param name="id">The unique identifier of the Fornecedor to retrieve.</param>
/// <returns>An ActionResult containing the FornecedorDto object if found, or NotFound if the Fornecedor does not exist.</returns>
[HttpGet("{id}")]
public async Task<ActionResult<FornecedorDto>> GetFornecedor(int id)
{
    var fornecedor = await _context.Fornecedores.FindAsync(id);

    if (fornecedor == null)
    {
        return NotFound();
    }

    return Ok(_mapper.Map<FornecedorDto>(fornecedor));
}

    // DELETE: api/Fornecedores/5
    /// <summary>
/// Deletes a Fornecedor by its unique identifier.
/// </summary>
/// <param name="id">The unique identifier of the Fornecedor to delete.</param>
/// <returns>
/// An IActionResult indicating the outcome of the operation.
/// If the Fornecedor is found and deleted successfully, returns NoContent.
/// If the Fornecedor is not found, returns NotFound.
/// </returns>
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteFornecedor(int id)
{
    var fornecedor = await _context.Fornecedores.FindAsync(id);
    if (fornecedor == null)
    {
        return NotFound();
    }

    _context.Fornecedores.Remove(fornecedor);
    await _context.SaveChangesAsync();

    return NoContent();
}

    /// <summary>
/// Checks if a Fornecedor with the specified unique identifier exists in the database.
/// </summary>
/// <param name="id">The unique identifier of the Fornecedor to check for existence.</param>
/// <returns>
/// A boolean value indicating whether a Fornecedor with the specified unique identifier exists in the database.
/// Returns true if a Fornecedor with the given id exists; otherwise, returns false.
/// </returns>
private bool FornecedorExists(int id)
{
    return _context.Fornecedores.Any(e => e.Id == id);
}

    // POST: api/Fornecedores
    /// <summary>
/// Creates a new Fornecedor in the database.
/// </summary>
/// <param name="fornecedorDto">The Fornecedor data to be created. This should be a valid FornecedorDto object.</param>
/// <returns>
/// An ActionResult containing the created FornecedorDto object if the operation is successful.
/// If the Fornecedor data does not meet the validation criteria, returns a BadRequest with an appropriate error message.
/// If the Fornecedor is successfully created, returns a CreatedAtAction result with the URI of the newly created Fornecedor.
/// </returns>
[HttpPost]
public async Task<ActionResult<FornecedorDto>> PostFornecedor(FornecedorDto fornecedorDto)
{
    var fornecedor = _mapper.Map<FornecedorModel>(fornecedorDto);

    if (!await _fornecedorService.ValidarFornecedor(fornecedor))
    {
        return BadRequest("Fornecedor não atende aos critérios de validação.");
    }

    fornecedor.DataHoraCadastro = DateTime.UtcNow;
    _context.Fornecedores.Add(fornecedor);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.Id }, _mapper.Map<FornecedorDto>(fornecedor));
}

    // PUT: api/Fornecedores/5
    /// <summary>
/// Updates an existing Fornecedor in the database.
/// </summary>
/// <param name="id">The unique identifier of the Fornecedor to update.</param>
/// <param name="fornecedorDto">The updated Fornecedor data. This should be a valid FornecedorDto object.</param>
/// <returns>
/// An IActionResult indicating the outcome of the operation.
/// If the provided id does not match the id in the FornecedorDto, returns BadRequest.
/// If the Fornecedor data does not meet the validation criteria, returns BadRequest with an appropriate error message.
/// If the Fornecedor is successfully updated, returns NoContent.
/// If a concurrency issue occurs (i.e., the Fornecedor has been modified by another user), returns NotFound.
/// </returns>
[HttpPut("{id}")]
public async Task<IActionResult> PutFornecedor(int id, FornecedorDto fornecedorDto)
{
    if (id != fornecedorDto.Id)
    {
        return BadRequest();
    }

    var fornecedor = _mapper.Map<FornecedorModel>(fornecedorDto);

    if (!await _fornecedorService.ValidarFornecedor(fornecedor))
    {
        return BadRequest("Fornecedor não atende aos critérios de validação.");
    }

    _context.Entry(fornecedor).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!FornecedorExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return NoContent();
}

// Exemplo no método POST
    /// <summary>
/// Creates a new Fornecedor in the database.
/// </summary>
/// <param name="fornecedorDto">The Fornecedor data to be created. This should be a valid FornecedorDto object.</param>
/// <returns>
/// An ActionResult containing the created FornecedorDto object if the operation is successful.
/// If the Fornecedor data does not meet the validation criteria, returns a BadRequest with an appropriate error message.
/// If the Fornecedor is successfully created, returns a CreatedAtAction result with the URI of the newly created Fornecedor.
/// </returns>
[HttpPost("Fornecedores/Auditado")]
public async Task<ActionResult<FornecedorDto>> PostFornecedorAuditado(FornecedorDto fornecedorDto)
{
    var fornecedor = _mapper.Map<FornecedorModel>(fornecedorDto);

    if (!await _fornecedorService.ValidarFornecedor(fornecedor))
    {
        return BadRequest("Fornecedor não atende aos critérios de validação.");
    }

    fornecedor.DataHoraCadastro = DateTime.UtcNow;
    _context.Fornecedores.Add(fornecedor);
    await _context.SaveChangesAsync();

    // Auditar criação de fornecedor
    await _fornecedorService.AuditarFornecedor(fornecedor, "Criação", "usuário_atual");

    return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.Id }, _mapper.Map<FornecedorDto>(fornecedor));
}

}
    

    