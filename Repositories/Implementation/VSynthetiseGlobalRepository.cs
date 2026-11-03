using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class VSynthetiseGlobalRepository : Repository<VSynthetiseGlobal, int>, IVSynthetiseGlobalRepository          {
    public VSynthetiseGlobalRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
