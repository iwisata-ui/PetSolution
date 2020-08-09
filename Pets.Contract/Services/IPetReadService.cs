using System.IO;
using System.Threading.Tasks;

namespace Pets.Contract.Services
{
	public interface IPetReadService
	{
		Task<Stream> GetDataAsync();
	}
}
