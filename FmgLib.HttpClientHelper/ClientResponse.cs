using System.Net;
using System.Net.Http.Headers;

namespace FmgLib.HttpClientHelper;

public class ClientResponse
{
    public string? ResponseStr { get; set; }
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public HttpContentHeaders? Header { get; set; }
    public string? ErrorMessage { get; set; }
    public ClientContentType? ContentType
    {
        get
        {
            if (Header is null)
                return null;

            if ((bool)(Header?.ContentType?.MediaType?.Contains("json", StringComparison.InvariantCultureIgnoreCase)))
                return ClientContentType.Json;
            else if ((bool)(Header?.ContentType?.MediaType?.Contains("xml", StringComparison.InvariantCultureIgnoreCase)))
                return ClientContentType.Xml;
            else if ((bool)(Header?.ContentType?.MediaType?.Contains("html", StringComparison.InvariantCultureIgnoreCase)))
                return ClientContentType.Html;
            else
                return ClientContentType.Text;
        }
    }
}
