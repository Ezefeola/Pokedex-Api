using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Web.Api.Endpoints.Auth;
public class AuthEndpointsConfiguration : EndpointsConfiguration
{
    public AuthEndpointsConfiguration()
    {
        WithPrefix("auth");
        WithTags("auth");
    }
}