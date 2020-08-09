using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using Pets.Contract.Enums;
using Pets.Contract.Models;
using Pets.Contract.Services;
using Should;
using SpecsFor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pets.Backend.Tests.Services.DataService
{
	public class SeparateGenderList : SpecsFor<Backend.Services.DataService>
	{
		private List<OwnerGenderCats> _result;
		private static List<PetOwner> _petOwners;
		protected override void Given()
		{
			Given<web_service_returns_data_with_mix_genders_and_pets>();
			base.Given();
		}

		protected override void When()
		{
			_result = SUT.GetOwnerGenderCatsAsync().Result.ToList();
		}

		[Test]
		public void then_result_should_contain_separate_cat_list_for_male_and_female()
		{
			_result.ShouldNotBeNull();
			_result.Count.ShouldEqual(2);

			var maleCatList = _result.Single(s => s.OwnerGender == Gender.Male).CatNames.ToList();
			maleCatList.Count.ShouldEqual(3);
			maleCatList.ShouldContain("Garfield");
			maleCatList.ShouldContain("Tom");
			maleCatList.ShouldContain("Max");

			var femaleCatList = _result.Single(s => s.OwnerGender == Gender.Female).CatNames.ToList();
			femaleCatList.Count.ShouldEqual(1);
			femaleCatList.ShouldContain("Garfield");
		}
		private class web_service_returns_data_with_mix_genders_and_pets : IContext<Backend.Services.DataService>
		{
			public void Initialize(ISpecs<Backend.Services.DataService> state)
			{
				_petOwners = Builder<PetOwner>.CreateListOfSize(4).Build().ToList();

				_petOwners[0].Gender = Gender.Male;
				_petOwners[0].Pets = new List<Pet>
				{
					new Pet {Name = "Garfield", Type = PetType.Cat},
					new Pet {Name = "Fido", Type = PetType.Dog}
				};


				_petOwners[1].Gender = Gender.Female;
				_petOwners[1].Pets = new List<Pet>
				{
					new Pet {Name = "Garfield", Type = PetType.Cat}
				};


				_petOwners[2].Gender = Gender.Male;
				_petOwners[2].Pets = null;


				_petOwners[3].Gender = Gender.Male;
				_petOwners[3].Pets = new List<Pet>
				{
					new Pet {Name = "Tom", Type = PetType.Cat},
					new Pet {Name = "Max", Type = PetType.Cat},
					new Pet {Name = "Sam", Type = PetType.Dog}
				};


				state.GetMockFor<IPetReadService>()
					.Setup(service => service.GetDataAsync())
					.ReturnsAsync(new MemoryStream());

				state.GetMockFor<IDataDeserializeService>()
					.Setup(service => service.GetPetOwners(It.IsAny<Stream>()))
					.Returns(_petOwners);
			}
		}
	}
}
