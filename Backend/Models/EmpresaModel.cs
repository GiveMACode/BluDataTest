namespace Backend.Models.EmpresaModel;
using Backend.Models.FornecedorModel;
public class EmpresaModel
{
    public int Id { get; set; }
    public string? UF { get; set; }
    public string? NomeFantasia { get; set; }
    public string? CNPJ { get; set; }
    public List<FornecedorModel>? Fornecedores { get; set; }
}