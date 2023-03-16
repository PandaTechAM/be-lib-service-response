using System.Text.Json.Serialization;

namespace PandaTech.ServiceResponse;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceResponseStatus
{
    Ok, //Get/Post/Put/Delete request succeeded.
    OkWithNoData, //Get/Post/Put/Delete request succeeded but no data returned
    Moved, //Requested resource assigned to new temp/perm URL
    BadRequest, //Client side error due to some reason
    Duplicate, //Duplicative data
    Unauthorized, //Client is not authenticated, unknown client
    Forbidden, //Client is known but access is restricted
    NotFound, //Endpoint is valid but the resource itself does not exist.
    Error //Other error
}