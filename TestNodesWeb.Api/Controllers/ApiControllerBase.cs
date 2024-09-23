using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestNodesWeb.Api.Configuration.Filters;

namespace TestNodesWeb.Controllers
{
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    public abstract class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }
    }
}