
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchtistStudio.Core;

[Authorize]
public class MyController : Controller
{

    [NonAction]
    protected IActionResult ItemNotFound()
    {
        return BadRequest("Item not found");
    }

    [NonAction]
    protected IActionResult Existed(string name)
    {
        return BadRequest($"{name} is existed");
    }
}