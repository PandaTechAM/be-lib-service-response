using System.Text.Json.Serialization;

namespace PandaTech.ServiceResponse;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceResponseStatus
{
    Ok = 200, //Get/Post/Put/Delete request succeeded.
    Moved = 302, //Requested resource assigned to new temp/perm URL
    BadRequest = 400, //Client side error due to some reason
    Unauthorized = 401, //Client is not authenticated, unknown client
    Forbidden = 403, //Client is known but access is restricted
    NotFound = 404, //Endpoint is valid but the resource itself does not exist.
    Error = 500, //Other error
    Conflict = 409 //Other error
}