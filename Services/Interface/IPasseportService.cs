namespace TestMVCSharpC.Services.Interface;

using Models;
using Models.Dto;
using Generic;
using Services.Generic;

public interface IPasseportService : IService<Passeport, int, PasseportDto> 
{
}
