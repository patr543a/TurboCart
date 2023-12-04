using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;

namespace TurboCart.Presentation.Websites.TurboCartManagement.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IUserUseCase _userUseCase;

        public BaseController(IUserUseCase userUseCase) {
            _userUseCase = userUseCase;
        }

        public async Task<bool> IsLoggedIn() {
            return true; //FIXME
            if (Guid.TryParse(HttpContext.Session.GetString("_login"), out Guid guid)) {
                return await _userUseCase.Authenticate(guid) ?? false;
            }
            return false;
        }
    }
}
