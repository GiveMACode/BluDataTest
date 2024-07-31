using Backend.Models.FornecedorModel;

namespace Backend.Audit.FornecedorAudit;

public class FornecedorAudit
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public string? Ação { get; set; }
    public DateTime DataHora { get; set; }
    public string? Usuario { get; set; }
    public FornecedorModel? Fornecedor { get; set; }
}
