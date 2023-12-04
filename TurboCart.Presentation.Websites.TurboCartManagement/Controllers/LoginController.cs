using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Presentation.Websites.TurboCartManagement.Models;

namespace TurboCart.Presentation.Websites.TurboCartManagement.Controllers;

[Route("Login")]
public class LoginController : BaseController
{
    public LoginController(IUserUseCase userUseCase) : base(userUseCase) {
    }

    [HttpGet]
    public IActionResult Index() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel loginViewModel) {
        if (!ModelState.IsValid) {
            return BadRequest("Brugernavn og kodeord er påkrævet");
        }

        Guid result = await _userUseCase.Authenticate(loginViewModel.Username, loginViewModel.Password);
        if (result == Guid.Empty) {
            return BadRequest("Brugernavn eller kodeord er fokert");
        }

        HttpContext.Session.SetString("_login", result.ToString());

        return Redirect("/");
    }

    [HttpGet("Logout")]
    public IActionResult Logout() {
        HttpContext.Session.Clear();
        return Redirect("/Login");
    }
}
