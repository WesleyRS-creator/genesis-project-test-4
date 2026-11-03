using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class AdresseRepository : Repository<Adresse, int>, IAdresseRepository          {
    public AdresseRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
