using Pets.Contract.Enums;
using Pets.Contract.Models;
using Pets.Contract.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pets.Backend.Services
{
	public class DataService : IDataService
	{
		private readonly IPetReadService _petReadService;
		private readonly IDataDeserializeService _dataDeserializeService;

		public DataService(IPetReadService petReadService, IDataDeserializeService dataDeserializeService)
		{
			_petReadService = petReadService;
			_dataDeserializeService = dataDeserializeService;
		}
		public async Task<IEnumerable<OwnerGenderCats>> GetOwnerGenderCatsAsync()
		{
			var dataStream = await _petReadService.GetDataAsync();
			var petOwners = _dataDeserializeService.GetPetOwners(dataStream);

			return petOwners
				.Where(p => p.Pets != null && p.Pets.Any())
				.GroupBy(p => p.Gender)
				.Select(g => new OwnerGenderCats
				{
					OwnerGender = g.Key,
					CatNames = g.SelectMany(o =>
						o.Pets.Where(p => p.Type == PetType.Cat).Select(p => p.Name).OrderBy(n => n))
				});
        }
	}
}
