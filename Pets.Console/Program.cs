using Autofac;
using Pets.Console.Code.IoC;
using Pets.Contract.Services;

namespace Pets.Console
{
	public class Program
	{
		public static IDataService DataService => IoCBootstrapper.Bootstrap().Resolve<IDataService>();
		public static void Main(string[] args)
		{
			var ownerGenderCatList = DataService.GetOwnerGenderCatsAsync().Result;
			foreach (var ownerGenderCats in ownerGenderCatList)
			{
				System.Console.WriteLine();
				System.Console.WriteLine(ownerGenderCats.OwnerGender);
				System.Console.WriteLine("-------------");

				foreach (var catName in ownerGenderCats.CatNames)
				{
					System.Console.WriteLine(catName);
				}

				System.Console.WriteLine();
			}
			System.Console.ReadLine();
		}
	}
}
