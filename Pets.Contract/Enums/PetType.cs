using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pets.Contract.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PetType
	{
		Dog,
		Cat,
		Fish
	}
}
