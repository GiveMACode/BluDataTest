namespace Backend.Models.FornecedorModel;
using Models.EmpresaModel;
using Models.TelefoneModel;
public class FornecedorModel
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public EmpresaModel? Empresa { get; set; }
    public string Nome { get; set; }
    public string? CPF_CNPJ { get; set; }
    public DateTime DataHoraCadastro { get; set; }
    public string? RG { get; set; }
    public DateTime? DataNascimento { get; set; }
    public List<TelefoneModel>? Telefones { get; set; }
}