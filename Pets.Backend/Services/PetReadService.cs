using Pets.Contract.Exceptions;
using Pets.Contract.Services;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pets.Backend.Services
{
	public class PetReadService: IPetReadService
	{
		public HttpClient HttpClient { get; set; }

		public async Task<Stream> GetDataAsync()
		{
			try
			{
				var baseAddress = ConfigurationManager.AppSettings[Constants.AppSettingKeys.BaseAddress];
				var uri = ConfigurationManager.AppSettings[Constants.AppSettingKeys.ResourceUri];
				var url = $"{baseAddress}{uri}";

				var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
				                 && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

				if (!isValidUrl)
				{
					var errorMessage = $"Invalid URL: {url}";
					throw new Exception(errorMessage);
				}

				var response = await HttpClient.GetAsync(url).ConfigureAwait(false);

				if (!response.IsSuccessStatusCode) throw new Exception("Failed to get data from web service");

				var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

				return content;
			}
			catch (Exception ex)
			{
				throw new WebServiceReadException(ex);
			}
		}
    }
}
