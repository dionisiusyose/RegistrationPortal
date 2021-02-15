﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User_Management.Bases;
using User_Management.Models;
using User_Management.Repositories.RepositoryDatas;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionsController : BaseIntController<Religion, ReligionRepository>
    {
        public ReligionsController(ReligionRepository repository) : base(repository)
        {
        }
    }
}