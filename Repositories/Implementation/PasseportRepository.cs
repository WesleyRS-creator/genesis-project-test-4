using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class PasseportRepository : Repository<Passeport, int>, IPasseportRepository          {
    public PasseportRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
