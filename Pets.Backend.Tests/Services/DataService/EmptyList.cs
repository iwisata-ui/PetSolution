using Moq;
using NUnit.Framework;
using Pets.Contract.Models;
using Pets.Contract.Services;
using Should;
using SpecsFor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pets.Backend.Tests.Services.DataService
{
	public class EmptyList : SpecsFor<Backend.Services.DataService>
	{
		private List<OwnerGenderCats> _result;
		private static List<PetOwner> _petOwners;
		protected override void Given()
		{
			Given<web_service_returns_empty_data>();
			base.Given();
		}

		protected override void When()
		{
			_result = SUT.GetOwnerGenderCatsAsync().Result.ToList();
		}

		[Test]
		public void then_result_should_be_empty()
		{
			_result.ShouldNotBeNull();
			_result.Count.ShouldEqual(0);
		}
		private class web_service_returns_empty_data : IContext<Backend.Services.DataService>
		{
			public void Initialize(ISpecs<Backend.Services.DataService> state)
			{
				_petOwners = new List<PetOwner>();

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
