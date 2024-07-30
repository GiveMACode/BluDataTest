namespace Backend.Models.TelefoneModel;
using Models.FornecedorModel;

public class TelefoneModel
{
    public int Id { get; set; }
    public string? Numero { get; set; }
    public int FornecedorId { get; set; }
    public FornecedorModel? Fornecedor { get; set; }
}
