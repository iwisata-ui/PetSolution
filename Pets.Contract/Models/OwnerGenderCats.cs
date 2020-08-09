using Pets.Contract.Enums;
using System.Collections.Generic;

namespace Pets.Contract.Models
{
	public class OwnerGenderCats
	{
		public Gender OwnerGender { get; set; }
		public IEnumerable<string> CatNames { get; set; } = new List<string>();
	}
}
