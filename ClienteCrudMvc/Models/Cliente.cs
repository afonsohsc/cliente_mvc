using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClienteCrudMvc.Models
{
	[Table("Clientes")]
	public class Cliente
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "O nome deve ser preenchido.")]
		[MaxLength(300)]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O CPF deve ser preenchido.")]
		[MaxLength(15)]
		public string CPF { get; set; }

		[Required(ErrorMessage = "A data de nascimento nome deve ser preenchido.")]
		[DisplayName("Data de Nascimento")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime DataNascimento { get; set; }

		[Required(ErrorMessage = "O genero deve ser preenchido.")]
		[MaxLength(1)]
		public string Genero { get; set; }

		public virtual List<Telefone> Telefones { get; set; }
	}
}