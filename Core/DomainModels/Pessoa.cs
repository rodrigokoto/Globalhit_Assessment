using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.BaseModel;

namespace Core.Models
{
    [Table("Pessoa")] 
	public class Pessoa : IdentityBase
	{

		[Required] 
		[StringLength(100)] 
		public string Nome { get; set; } = null!;

		[Required]
		[StringLength(100)]
		public string Nome_Complemento { get; set; } = null!;

		[StringLength(20)]
		public string? Rg { get; set; }

		[StringLength(15)]
		public string? Cpf { get; set; }

		[StringLength(100)]
		public string? Email { get; set; }

		[StringLength(15)]
		public string? Telefone { get; set; }

		public int? Id_Anexo_Sys { get; set; }

		public DateTime? Data_Alteracao { get; set; }

		public DateTime? Data_Cadastro { get; set; }
	}
}
