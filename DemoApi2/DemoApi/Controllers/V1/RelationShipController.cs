using DemoApi.APIFilter;
using DemoApi.Common.Extension;
using DemoApi.Common.Pagination;
using DemoApi.HttpActionResult;
using DemoApi.Models.RelationShips;
using DemoApi.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DemoApi.Controllers.V1
{
    [RoutePrefix("api/v1/relationship")]
    [DemoAuthorizeAttribute]
    public class RelationShipController : DemoApiBaseController
    {
        private IRelationShipService _relationShipService;

        public RelationShipController(IRelationShipService relationShipService)
        {
            _relationShipService = relationShipService;
        }

        [Route("addrelationship")]
        [HttpPost]
        [UnitOfWorkAction]
        public async Task<IHttpActionResult> AddRelationShip(HttpRequestMessage requestMessage, AddRelationShipModel model)
        {
            int lastId = await _relationShipService.AddRelationShip(model);
            return new TCSuccessHttpActionResult(requestMessage, lastId);
        }
        [Route("updaterelationship")]
        [HttpPost]
        [UnitOfWorkAction]
        public async Task<IHttpActionResult> UpdateRelationShip(HttpRequestMessage requestMessage, UpdateRelationShipModel model)
        {
            int lastId = await _relationShipService.UpdateRelationShip(model);
            return new TCSuccessHttpActionResult(requestMessage, lastId);
        }


        [Route("checkfriend")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckingRelationShip(HttpRequestMessage requestMessage,
           string UserIdOne,
           string UserIdTwo)
        {
            return new TCSuccessHttpActionResult(requestMessage, await _relationShipService.CheckingRelationShip(UserIdOne, UserIdTwo));
        }
        [Route("getallrelationship")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRelationShip(HttpRequestMessage requestMessage,
           string UserId="")
        {
            return new TCSuccessHttpActionResult(requestMessage, await _relationShipService.GetAllRelationShipData(UserId));
        }
        [Route("getalluserinrelation")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUserInRelation(HttpRequestMessage requestMessage,
           string userId = ""
            , string userName = ""
            , int? pageNumber = null,
            int? pageSize = null)
        {
            PaginationRequest pagedDataRequest = new PaginationRequest(pageNumber.DefaultZeroIfNull(), pageSize);
            return new TCSuccessHttpActionResult(requestMessage, await _relationShipService.GetAllUserInRelation(pagedDataRequest,userId, userName));
        }
    }
}