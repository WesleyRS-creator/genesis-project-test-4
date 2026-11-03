using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class VProjetsEtEquipeRepository : Repository<VProjetsEtEquipe, int>, IVProjetsEtEquipeRepository          {
    public VProjetsEtEquipeRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
