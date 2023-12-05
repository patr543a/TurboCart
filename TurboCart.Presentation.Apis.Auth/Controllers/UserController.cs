using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Presentation.Apis.Auth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserUseCase _userUseCase)
    : ControllerBase
{
    [HttpPost("Auth/{username}/{password}")]
    public async Task<ActionResult<Guid>> Authenticate(string username, string password)
    {
        var result = await _userUseCase.Authenticate(username, password);

        if (result != Guid.Empty)
            return Ok(result);

        return StatusCode(403, Guid.Empty);
    }

    [HttpPost("Auth/{guid}")]
    public async Task<ActionResult<bool>> Authenticate(Guid guid)
    {
        var result = await _userUseCase.Authenticate(guid);

        if (result ?? false)
            return Ok(true);

        return StatusCode(403, false);
    }

    [HttpPost("{guid}")]
    public async Task<ActionResult<User>> PostUser(Guid guid, User user)
    {
        try
        {
            await _userUseCase.AddUser(guid, user);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(user);
    }

    [HttpPut("{guid}")]
    public async Task<ActionResult<User>> PutUser(Guid guid, User user)
    {
        try
        {
            await _userUseCase.UpdateUser(guid, user);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(user);
    }

    [HttpDelete("{guid}")]
    public async Task<ActionResult> DeleteUser(Guid guid)
    {
        try
        {
            await _userUseCase.DeleteUser(guid);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }
}
