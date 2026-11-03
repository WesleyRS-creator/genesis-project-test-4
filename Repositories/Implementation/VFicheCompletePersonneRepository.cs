using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class VFicheCompletePersonneRepository : Repository<VFicheCompletePersonne, int>, IVFicheCompletePersonneRepository          {
    public VFicheCompletePersonneRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
