using System.ComponentModel.DataAnnotations;

namespace Gcook.ViewModels;

public class LoginVM
{
    [Display(Name = "email ou nome de Usuario", Prompt = "Informe seu Email ou nome de Usuário" )]
    [Required(ErrorMessage = "por favor, informe sey email ou nome de usuário")]
    public string Email { get; set; }

    [Display(Name = "Senha de Acesso", Prompt = "********")]
    [Required(ErrorMessage = "Por favor, informe sua senha")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Display(Name = "Manter Conectado?")]
    public bool Lembrar { get; set; } = false;

    public string UrlRetorno { get; set; }
}
