using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;

namespace ArchtistStudio.Modules.Error;


public class ErrorController : MyController
{
        public IActionResult Error()
    {
        return View();
    }
}