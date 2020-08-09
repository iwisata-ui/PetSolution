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
	public class DataWithDogsAndFishButNoCats : SpecsFor<Backend.Services.DataService>
	{
		private List<OwnerGenderCats> _result;
		private static List<PetOwner> _petOwners;
		protected override void Given()
		{
			Given<web_service_returns_data_with_only_dogs_and_fish>();
			base.Given();
		}

		protected override void When()
		{
			_result = SUT.GetOwnerGenderCatsAsync().Result.ToList();
		}

		[Test]
		public void then_result_should_contain_separate_cat_list_for_male_and_female_both_with_empty_cat_list()
		{
			_result.ShouldNotBeNull();
			_result.Count.ShouldEqual(2);

			var maleCatList = _result.Single(s => s.OwnerGender == Gender.Male).CatNames.ToList();
			maleCatList.ShouldBeEmpty();

			var femaleCatList = _result.Single(s => s.OwnerGender == Gender.Female).CatNames.ToList();
			femaleCatList.ShouldBeEmpty();
		}

		private class web_service_returns_data_with_only_dogs_and_fish : IContext<Backend.Services.DataService>
		{
			public void Initialize(ISpecs<Backend.Services.DataService> state)
			{
				_petOwners = Builder<PetOwner>.CreateListOfSize(4).Build().ToList();

				_petOwners[0].Gender = Gender.Male;
				_petOwners[0].Pets = new List<Pet>
				{
					new Pet {Type = PetType.Fish},
					new Pet {Type = PetType.Dog}
				};


				_petOwners[1].Gender = Gender.Female;
				_petOwners[1].Pets = new List<Pet>
				{
					new Pet {Type = PetType.Fish}
				};


				_petOwners[2].Gender = Gender.Male;
				_petOwners[2].Pets = null;


				_petOwners[3].Gender = Gender.Male;
				_petOwners[3].Pets = new List<Pet>
				{
					new Pet {Type = PetType.Dog},
					new Pet {Type = PetType.Dog},
					new Pet {Type = PetType.Fish}
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
