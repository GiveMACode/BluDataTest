using AutoMapper;
using Backend.Data;
using Backend.DTOs;
using Backend.Models.EmpresaModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controller;

[Route("api/[controller]")]
[ApiController]
public class EmpresasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmpresasController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Implementar métodos CRUD para Empresas

 // GET: api/Empresas
    /// <summary>
/// Retrieves a list of all Empresas from the database.
/// </summary>
/// <returns>
/// An ActionResult containing a list of EmpresaDto objects.
/// If the operation is successful, the HTTP status code will be 200 (OK).
/// </returns>
[HttpGet]
public async Task<ActionResult<IEnumerable<EmpresaDto>>> GetEmpresas()
{
    // Retrieve all Empresas from the database using Entity Framework's ToListAsync method
    var empresas = await _context.Empresas.ToListAsync();

    // Map the retrieved Empresas to EmpresaDto objects using AutoMapper
    var empresasDto = _mapper.Map<IEnumerable<EmpresaDto>>(empresas);

    // Return the mapped EmpresasDto objects with a 200 (OK) status code
    return Ok(empresasDto);
}

    // GET: api/Empresas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpresaDto>> GetEmpresa(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);

        if (empresa == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<EmpresaDto>(empresa));
    }

    // POST: api/Empresas
    /// <summary>
/// Creates a new Empresa in the database.
/// </summary>
/// <param name="empresaDto">The Empresa data to be created, represented as an EmpresaDto object.</param>
/// <returns>
/// An ActionResult containing the newly created EmpresaDto object.
/// If the operation is successful, the HTTP status code will be 201 (Created).
/// The Location header will contain the URI of the newly created resource.
/// </returns>
[HttpPost]
public async Task<ActionResult<EmpresaDto>> PostEmpresa(EmpresaDto empresaDto)
{
    var empresa = _mapper.Map<EmpresaModel>(empresaDto);
    _context.Empresas.Add(empresa);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, _mapper.Map<EmpresaDto>(empresa));
}


    // PUT: api/Empresas/5
    /// <summary>
/// Updates an existing Empresa in the database.
/// </summary>
/// <param name="id">The unique identifier of the Empresa to be updated.</param>
/// <param name="empresaDto">The updated Empresa data, represented as an EmpresaDto object.</param>
/// <returns>
/// An IActionResult indicating the outcome of the operation.
/// If the operation is successful, the HTTP status code will be 204 (No Content).
/// If the provided id does not match the id in the EmpresaDto, the method will return a 400 (Bad Request).
/// If the Empresa with the specified id does not exist, the method will return a 404 (Not Found).
/// </returns>
[HttpPut("{id}")]
public async Task<IActionResult> PutEmpresa(int id, EmpresaDto empresaDto)
{
    if (id != empresaDto.Id)
    {
        return BadRequest();
    }

    var empresa = _mapper.Map<EmpresaModel>(empresaDto);
    _context.Entry(empresa).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!EmpresaExists(id))
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

    // DELETE: api/Empresas/5
    /// <summary>
/// Deletes an existing Empresa from the database.
/// </summary>
/// <param name="id">The unique identifier of the Empresa to be deleted.</param>
/// <returns>
/// An IActionResult indicating the outcome of the operation.
/// If the operation is successful, the HTTP status code will be 204 (No Content).
/// If the Empresa with the specified id does not exist, the method will return a 404 (Not Found).
/// </returns>
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteEmpresa(int id)
{
    var empresa = await _context.Empresas.FindAsync(id);
    if (empresa == null)
    {
        return NotFound();
    }

    _context.Empresas.Remove(empresa);
    await _context.SaveChangesAsync();

    return NoContent();
}

    private bool EmpresaExists(int id)
    {
        return _context.Empresas.Any(e => e.Id == id);
    }
}
