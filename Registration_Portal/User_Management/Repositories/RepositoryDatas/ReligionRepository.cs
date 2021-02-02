using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Context;
using User_Management.Models;
using User_Management.Repositories.GeneralRepositories;

namespace User_Management.Repositories.RepositoryDatas
{
    public class ReligionRepository : RepositoryInt<Religion, MyContext>
    {
        public ReligionRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
