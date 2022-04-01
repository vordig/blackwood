using Microsoft.AspNetCore.Mvc;

namespace Blackwood.Web.Contracts.Requests;

public class GreetingsWebRequest
{
    [FromQuery(Name = "firstName")] public string FirstName { get; set; }
    [FromQuery(Name = "lastName")] public string LastName { get; set; }
}