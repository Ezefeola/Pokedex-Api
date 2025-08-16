namespace Web.Api.Endpoints.Abstractions;
public interface IEndpoint <TGroup> where TGroup : IEndpointGroup
{
    RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app);
}