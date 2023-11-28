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
    public async Task<ActionResult<bool>> Authenticate(string username, string password)
    {
        var result = await _userUseCase.Authenticate(username, password);

        if (result ?? false)
            return Ok();

        return StatusCode(403);
    }

    [HttpPost("{username}/{password}")]
    public async Task<ActionResult<User>> PostUser(string username, string password, User user)
    {
        try
        {
            await _userUseCase.AddUser(username, password, user);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(user);
    }

    [HttpPut("{username}/{password}")]
    public async Task<ActionResult<User>> PutUser(string username, string password, User user)
    {
        try
        {
            await _userUseCase.UpdateUser(username, password, user);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(user);
    }

    [HttpDelete("{username}/{password}")]
    public async Task<ActionResult> DeleteUser(string username, string password)
    {
        try
        {
            await _userUseCase.DeleteUser(username, password);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }
}
