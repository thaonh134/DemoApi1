using DemoApi.Common.Enums;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Database.Repositories
{
    public class RelationShipRepository : BaseRepositoryDateModify<RelationShip, test15_api_everEntities>, IRelationShipRepository
    {
        public RelationShipRepository(test15_api_everEntities dbContext) : base(dbContext)
        {

        }
       
    }
}
