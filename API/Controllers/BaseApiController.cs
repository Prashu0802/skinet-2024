﻿using API.RequestHelper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
        ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntities
        {
            var Item = await repo.ListAsync(spec);
            int count=await repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex, pageSize,count, Item);

            return Ok(pagination);
        }
    }
}
