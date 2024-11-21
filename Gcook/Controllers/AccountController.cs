using Microsoft.AspNetCore.Mvc;
using GCook.ViewModels;
using GCook.Services;

namespace GCook.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUsuarioService _usuarioService;

    public AccountController(
        ILogger<AccountController> logger,
        IUsuarioService usuarioService
    )
    {
        _logger = logger;
        _usuarioService = usuarioService;
    }
[HttpGet]
public IActionResult Login(string returnUrl)
{
    // Verificando se returnUrl é uma URL local válida antes de atribuir
    LoginVM login = new()
    {
        UrlRetorno = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.Content("~/")
    };
    return View(login);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginVM login)
{
    if (ModelState.IsValid)
    {
        var result = await _usuarioService.LoginUsuario(login);
        if (result.Succeeded)
        {
            // Verificando se UrlRetorno é uma URL local válida antes do redirecionamento
            if (Url.IsLocalUrl(login.UrlRetorno))
            {
                return LocalRedirect(login.UrlRetorno);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        if (result.IsLockedOut)
        {
            return RedirectToAction("Lockout");
        }

        if (result.IsNotAllowed)
        {
            ModelState.AddModelError(string.Empty, "Sua conta não está confirmada, verifique seu email");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Usuário e/ou Senha Inválidos");
        }
    }

    return View(login);
}


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _usuarioService.LogoffUsuario();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Registro()
    {
        RegistroVM register = new();
        return View(register);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registro(RegistroVM register)
    {
        register.Enviado = false;
        if (ModelState.IsValid)
        {
            var result = await _usuarioService.RegistrarUsuario(register);
            if (result != null)
            {
                foreach (var error in result)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                register.Enviado = result == null;
            }
        }
        return View(register);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmarEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToAction("Index", "Home");
        }
        await _usuarioService.ConfirmarEmail(userId, code);
        return View(true);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}