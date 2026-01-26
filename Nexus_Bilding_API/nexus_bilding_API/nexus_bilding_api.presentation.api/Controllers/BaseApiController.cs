using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.Wrappers;
using nexus_bilding_api.core.domain.Base;

namespace nexus_bilding_api.presentation.api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            if (result.Value == null) return Ok(new Response<T>(result.Value, "Operation Successful")); // Or string.Empty msg
            return Ok(new Response<T>(result.Value, "Operation Successful"));
        }

        return BadRequest(new Response<T>(result.Error));
    }
}
