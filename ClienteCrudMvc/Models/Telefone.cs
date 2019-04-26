using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClienteCrudMvc.Models
{
	[Table("Telefones")]
	public class Telefone
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(5)]
		public string DDD { get; set; }

		[MaxLength(10)]
		public string Numero { get; set; }

		[ForeignKey(nameof(Cliente))]
		public int IdCliente { get; set; }

		public virtual Cliente Cliente { get; set; }
	}
}