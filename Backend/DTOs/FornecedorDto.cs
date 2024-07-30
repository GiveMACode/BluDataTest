namespace Backend.DTOs;

public class FornecedorDto
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string? Nome { get; set; }
    public string? CPF_CNPJ { get; set; }
    public DateTime? DataHoraCadastro { get; set; }
    public string? RG { get; set; }
    public DateTime? DataNascimento { get; set; }
}