using AutoMapper;
using DemoApi.Common.Enums;
using DemoApi.Database.Base;
using DemoApi.Database.DatabaseContext;
using DemoApi.Database.Repositories.Interfaces;
using DemoApi.Models.RelationShips;
using DemoApi.Services.Common;
using DemoApi.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Services.Services
{
    public class RelationShipService : BaseService, IRelationShipService
    {
        private IRelationShipRepository _relationShiprepositoty;
        private IUnitOfWork _unitOfWork;
        public RelationShipService(IUnitOfWork unitOfWork, IRelationShipRepository relationShipRepository)
        {
            _unitOfWork = unitOfWork;
            _relationShiprepositoty = relationShipRepository;
        }
        

        public  async Task<int> AddRelationShip(AddRelationShipModel model)
        {
            if (string.Compare(model.User_One_Id, model.User_Two_Id) > 0)
            {
                var x = model.User_One_Id;
                var y = model.User_Two_Id;
                model.User_One_Id = y;
                model.User_Two_Id = x;
            }

            var itemEntry = Mapper.Map<RelationShip>(model);
            itemEntry.Status = (int)RelationShipStatus.Pending;

            _relationShiprepositoty.Create(itemEntry);
            return await _unitOfWork.SaveChangesAsync();
        }
        public async Task<int> UpdateRelationShip(UpdateRelationShipModel model)
        {
            if (string.Compare(model.User_One_Id, model.User_Two_Id) > 0)
            {
                var x = model.User_One_Id;
                var y = model.User_Two_Id;
                model.User_One_Id = y;
                model.User_Two_Id = x;
            }

            var itemEntry = await _relationShiprepositoty.GetAsync(x => x.User_One_Id == model.User_One_Id && x.User_Two_Id == model.User_Two_Id);
            itemEntry = Mapper.Map(model, itemEntry);
           
            _relationShiprepositoty.Update(itemEntry);
            return await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> CheckingRelationShip(string UserOne, string UserTwo)
        {
            var itemEntry = await _relationShiprepositoty.GetAsync(x =>(
            (
            (x.User_One_Id == UserOne && x.User_Two_Id==UserTwo)
            ||(x.User_One_Id == UserTwo && x.User_Two_Id == UserOne)
            )
             && x.Status == (int)RelationShipStatus.Accepted
           ));
            if (itemEntry == null) return false;
            else return true;
        }

        public async Task<List<ViewRelationShipModel>> GetAllRelationShip(string UserId)
        {
            var items =  await _relationShiprepositoty.GetAsync(x => (
            (x.User_One_Id == UserId || x.User_Two_Id == UserId)&& x.Status == (int)RelationShipStatus.Accepted
            ));
            if (items == null) return new List<ViewRelationShipModel>();
            return Mapper.Map<List<ViewRelationShipModel>>(items);
        }


    }
}
