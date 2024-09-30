using System.ComponentModel.DataAnnotations;

namespace Gcook.ViewModels;

 public class RegistroVM
 {
      [Display(Name = "Nome completo", Prompt = "Informe seu Nome Completo")]
      [Required(ErrorMessage = "por favor, informe seu Nome")]
      [StringLength(60, ErrorMessage = "O Nome deve possuir no máximo 60 caracteres")]
      public string Nome { get; set; }

      [DataType(DataType.Date)]
      [Display(Name = "Data de nascimento", Prompt = "informe sua Data de Nascimento")]
      [Required(ErrorMessage = "por favor, informe sua Data de nascimento")]
      public DateTime? DataNascimento { get; set; } = null;

      [Display(Prompt = "informe seu Email")]
      [Required(ErrorMessage = "por favor, informe seu email")]
      [EmailAddress(ErrorMessage = "por favor, informe um email valido!")]
      [StringLength(100, ErrorMessage = "o email deve possuir no maximo 100 caracteres")]
      public string Email { get; set; }   
      
      [DataType(DataType.Password)]
      [Display(Name = "Senha de Acesso", Prompt = "informe uma Senha para Acesso")]
      [Required(ErrorMessage = "por favor, informe sua senha de acesso")]
      [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve possuir no minimo 6 e no maximo 60 caracteres")]
      public string Senha { get; set; }

      [DataType(DataType.Password)] 
      [Display(Name = "Confirmar Senha de Acesso", Prompt = "Confirme sua Senha de Acesso")]
      [Compare("senha", ErrorMessage = "As Senhas não Conferem.")]
      public string ConfirmacaoSenha { get; set; }

      public IFormFile Foto { get; set; }

      public bool Termos { get; set; }

      public bool Enviado { get; set; }
}
