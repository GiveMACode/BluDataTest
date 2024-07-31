using Backend.Data;
using Backend.Models.FornecedorModel;
using Backend.Audit;
using Backend.Audit.FornecedorAudit;

namespace Backend.Services;

// Services/FornecedorService.cs
public class FornecedorService
{
    private readonly ApplicationDbContext _context;

    public FornecedorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidarFornecedor(FornecedorModel fornecedor)
    {
        var empresa = await _context.Empresas.FindAsync(fornecedor.EmpresaId);

        if (empresa == null)
        {
            throw new Exception("Empresa não encontrada.");
        }

        if (empresa.UF == "PR" && fornecedor.DataNascimento.HasValue &&
            (DateTime.Now.Year - fornecedor.DataNascimento.Value.Year) < 18)
        {
            return false; // Não permitir cadastro de menores de idade no Paraná
        }

        return true;
    }

    public async Task AuditarFornecedor(FornecedorModel fornecedor, string acao, string usuario)
        {
        

            var audit = new FornecedorAudit
            {
                FornecedorId = fornecedor.Id,
                Ação = acao,
                DataHora = DateTime.UtcNow,
                Usuario = usuario
            };

            _context.FornecedoresAudits.Add(audit);
            await _context.SaveChangesAsync();
        }

}
