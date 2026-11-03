using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class ProjetRepository : Repository<Projet, int>, IProjetRepository          {
    public ProjetRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
