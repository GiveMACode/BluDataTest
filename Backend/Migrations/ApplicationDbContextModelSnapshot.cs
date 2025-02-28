﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Backend.Audit.FornecedorAudit.FornecedorAudit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ação")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("TEXT");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Usuario")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("FornecedoresAudits");
                });

            modelBuilder.Entity("Backend.Models.EmpresaModel.EmpresaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CNPJ")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("TEXT");

                    b.Property<string>("UF")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("Backend.Models.FornecedorModel.FornecedorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF_CNPJ")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataHoraCadastro")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RG")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("Backend.Models.ProdutoModel.ProdutoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Backend.Models.TelefoneModel.TelefoneModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Numero")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("FornecedorModelProdutoModel", b =>
                {
                    b.Property<int>("FornecedoresId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FornecedoresId", "ProdutosId");

                    b.HasIndex("ProdutosId");

                    b.ToTable("FornecedorModelProdutoModel");
                });

            modelBuilder.Entity("Backend.Audit.FornecedorAudit.FornecedorAudit", b =>
                {
                    b.HasOne("Backend.Models.FornecedorModel.FornecedorModel", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("Backend.Models.FornecedorModel.FornecedorModel", b =>
                {
                    b.HasOne("Backend.Models.EmpresaModel.EmpresaModel", "Empresa")
                        .WithMany("Fornecedores")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Backend.Models.TelefoneModel.TelefoneModel", b =>
                {
                    b.HasOne("Backend.Models.FornecedorModel.FornecedorModel", "Fornecedor")
                        .WithMany("Telefones")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("FornecedorModelProdutoModel", b =>
                {
                    b.HasOne("Backend.Models.FornecedorModel.FornecedorModel", null)
                        .WithMany()
                        .HasForeignKey("FornecedoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.ProdutoModel.ProdutoModel", null)
                        .WithMany()
                        .HasForeignKey("ProdutosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Models.EmpresaModel.EmpresaModel", b =>
                {
                    b.Navigation("Fornecedores");
                });

            modelBuilder.Entity("Backend.Models.FornecedorModel.FornecedorModel", b =>
                {
                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}
