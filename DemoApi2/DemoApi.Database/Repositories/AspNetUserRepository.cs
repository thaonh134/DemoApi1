using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Repositories
{
    public class AspNetUserRepository : BaseRepositoryDateModify<AspNetUser, test15_api_everEntities>, IAspNetUserRepository
    {
        public AspNetUserRepository(test15_api_everEntities dbContext) : base(dbContext)
        {
        }
    }
}
