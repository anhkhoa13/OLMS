﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLMS.API.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly ISender _sender;
    public StudentController(ISender sender)
    {
        _sender = sender;
    }
}
    
