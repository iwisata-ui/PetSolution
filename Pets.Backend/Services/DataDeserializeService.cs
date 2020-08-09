using Newtonsoft.Json;
using Pets.Contract.Models;
using Pets.Contract.Services;
using System.Collections.Generic;
using System.IO;

namespace Pets.Backend.Services
{
	public class DataDeserializeService : IDataDeserializeService
	{
		public IEnumerable<PetOwner> GetPetOwners(Stream stream)
		{
			using (StreamReader sr = new StreamReader(stream))
			{
				var serializer = new JsonSerializer();
				using (JsonReader reader = new JsonTextReader(sr))
				{
					var petOwners = serializer.Deserialize<IEnumerable<PetOwner>>(reader);
					return petOwners;
				}
			}
		}
	}
}
