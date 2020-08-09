using Pets.Contract.Enums;

namespace Pets.Contract.Models
{
	public class Pet
	{
		public string Name { get; set; }
		public PetType Type { get; set; }
	}
}
