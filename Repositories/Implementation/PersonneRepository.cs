using TestMVCSharpC.Data;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Generic;
using TestMVCSharpC.Repositories.Interface;

namespace TestMVCSharpC.Repositories.Implementation;

public class PersonneRepository : Repository<Personne, int>, IPersonneRepository          {
    public PersonneRepository(TestMVCSharpCContext context) : base(context)
    {
    }
}
