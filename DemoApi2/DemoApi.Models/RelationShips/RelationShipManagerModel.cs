using DemoApi.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Models.RelationShips
{
    class RelationShipManagerModel
    {
    }

    public class AddRelationShipModel
    {
        public string User_One_Id { get; set; }
        public string User_Two_Id { get; set; }
        public string Action_User_Id { get; set; }
       

    }
    public class UpdateRelationShipModel
    {
        public string User_One_Id { get; set; }
        public string User_Two_Id { get; set; }
        public RelationShipStatus Status { get; set; }
        public string Action_User_Id { get; set; }

    }
    public class ViewRelationShipModel
    {
        public int Id { get; set; }
        public string User_One_Id { get; set; }
        public string User_Two_Id { get; set; }
        public RelationShipStatus ActiveStatus { get; set; }
        public string Action_User_Id { get; set; }
    }
}
