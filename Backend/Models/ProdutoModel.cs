namespace Backend.Models.ProdutoModel;
using Backend.Models.FornecedorModel;
public class ProdutoModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public ICollection<FornecedorModel>? Fornecedores { get; set; }
    
}


