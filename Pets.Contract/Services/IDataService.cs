using Pets.Contract.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pets.Contract.Services
{
	public interface IDataService
	{
		Task<IEnumerable<OwnerGenderCats>> GetOwnerGenderCatsAsync();
	}
}
