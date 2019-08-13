using DemoApi.Common.Pagination;
using DemoApi.Database.DatabaseContext;
using DemoApi.Models.RelationShips;
using DemoApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Services.Services.Interface
{
    public interface IRelationShipService
    {
        Task<int> AddRelationShip(AddRelationShipModel model);
        Task<int> UpdateRelationShip(UpdateRelationShipModel model);
        Task<bool> CheckingRelationShip(string UserOne,string UserTwo);
        Task<List<ViewRelationShipModel>> GetAllRelationShipData(string UserId);
        Task<PaginationAndDataResult<ViewUserModel>> GetAllUserInRelation(PaginationRequest pageDataRequest, string UserId,string UserName);

    }
}
