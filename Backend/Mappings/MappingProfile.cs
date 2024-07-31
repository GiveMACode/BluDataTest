using AutoMapper;
using Backend.DTOs;
using Backend.Models.EmpresaModel;
using Backend.Models.FornecedorModel;
using Backend.Models.TelefoneModel;

namespace Backend.Mappings;

public class MappingProfile : Profile
{
public MappingProfile()
    {
        CreateMap<FornecedorModel, FornecedorDto>().ReverseMap();
        CreateMap<EmpresaModel, EmpresaDto>();
        CreateMap<FornecedorModel, FornecedorDto>();
        CreateMap<TelefoneModel, TelefoneDto>();
    
    }
}
