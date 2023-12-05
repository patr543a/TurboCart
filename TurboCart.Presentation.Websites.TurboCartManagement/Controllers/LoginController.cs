using Microsoft.AspNetCore.Identity;
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
        var model = new LoginViewModel() {
            Username = "",
            Password = ""
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel loginViewModel) {
        if (!ModelState.IsValid) {
            loginViewModel.Password = "";
            loginViewModel.ErrorMessage = "Brugernavn og kodeord er påkrævet";
            return View(loginViewModel);
        }

        Guid result = await _userUseCase.Authenticate(loginViewModel.Username, loginViewModel.Password);
        if (result == Guid.Empty) {
            loginViewModel.Password = "";
            loginViewModel.ErrorMessage = "Brugernavn eller kodeord er fokert";
            return View(loginViewModel);
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
