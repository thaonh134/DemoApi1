using DemoApi.Models.RelationShips;
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
        Task<List<ViewRelationShipModel>> GetAllRelationShip(string UserId);
    }
}
