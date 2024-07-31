namespace Backend.Data;

using Backend.Audit.FornecedorAudit;
using Backend.Models.ProdutoModel;
using Microsoft.EntityFrameworkCore;
using Models.EmpresaModel;
using Models.FornecedorModel;
using Models.TelefoneModel;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<EmpresaModel> Empresas { get; set; }
    public DbSet<FornecedorModel> Fornecedores { get; set; }
    public DbSet<TelefoneModel> Telefones { get; set; }
    public DbSet<FornecedorAudit> FornecedoresAudits { get; set; }
    public DbSet<ProdutoModel> Produtos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmpresaModel>()
            .HasMany(e => e.Fornecedores)
            .WithOne(f => f.Empresa)
            .HasForeignKey(f => f.EmpresaId);

        modelBuilder.Entity<FornecedorModel>()
            .HasMany(f => f.Telefones)
            .WithOne(t => t.Fornecedor)
            .HasForeignKey(t => t.FornecedorId);

        modelBuilder.Entity<FornecedorModel >()
            .HasMany(fa => fa.Produtos)
            .WithMany(fa => fa.Fornecedores);
    }
}
