using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class ParticiperRepository : Repository<Participer, int>, IParticiperRepository          {
    public ParticiperRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
