using DemoApi.Database.Base;
using DemoApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DemoApi.APIFilter
{
    public class UnitOfWorkActionAttribute : ActionFilterAttribute
    {
        private IUnitOfWork _unitOfWork;

        private static System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadUncommitted;

        public UnitOfWorkActionAttribute(System.Data.IsolationLevel levelPerRequest = System.Data.IsolationLevel.ReadCommitted)
        {
            IsolationLevel = levelPerRequest;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _unitOfWork = CastleHelper.Resolver.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            _unitOfWork.SetIsolationLevel(IsolationLevel);
            _unitOfWork.ForceBeginTransaction();
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _unitOfWork = CastleHelper.Resolver.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            if (actionExecutedContext.Exception != null)
                _unitOfWork.RollbackTransaction();
            else
            {
                try
                {
                    _unitOfWork.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackTransaction();
                    actionExecutedContext.Exception = ex;
                }
            }
            base.OnActionExecuted(actionExecutedContext);
        }
        public override bool AllowMultiple => false;
    }
}