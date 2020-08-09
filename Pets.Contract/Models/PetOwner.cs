using Pets.Contract.Enums;
using System.Collections.Generic;

namespace Pets.Contract.Models
{
	public class PetOwner
	{
		public string Name { get; set; }
		public Gender Gender { get; set; }
		public int Age { get; set; }
		public IEnumerable<Pet> Pets { get; set; }
	}
}
