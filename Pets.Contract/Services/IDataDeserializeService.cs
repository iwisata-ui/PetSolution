using Pets.Contract.Models;
using System.Collections.Generic;
using System.IO;

namespace Pets.Contract.Services
{
	public interface IDataDeserializeService
	{
		IEnumerable<PetOwner> GetPetOwners(Stream stream);
	}
}
