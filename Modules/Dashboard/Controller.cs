using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.AspNetCore.Authorization;

namespace ArchtistStudio.Modules.Dashboard;


public class DashboardController(
    // IMapper mapper,
    // IDashboardRepository repository
    ) : MyController
{
    public IActionResult Overview()
    {
        return View();
    }

}