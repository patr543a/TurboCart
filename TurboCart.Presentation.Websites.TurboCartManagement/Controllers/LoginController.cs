using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Presentation.Websites.TurboCartManagement.Models;

namespace TurboCart.Presentation.Websites.TurboCartManagement.Controllers;

[Route("Login")]
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserUseCase _userUseCase;

    public LoginController(ILogger<LoginController> logger, IUserUseCase userUseCase) {
        _logger = logger;
        _userUseCase = userUseCase;
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

        bool? result = await _userUseCase.Authenticate(loginViewModel.Username, loginViewModel.Password);
        if (result == null) {
            return BadRequest("Fik ugyldigt svar tilbage fra serveren");
        }
        if (result == false) {
            return BadRequest("Brugernavn eller kodeord er fokert");
        }

        //TODO: log the user in

        return Redirect("/");
    }
}
