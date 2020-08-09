using System;

namespace Pets.Contract.Exceptions
{
	public class WebServiceReadException : Exception
	{
		public WebServiceReadException(Exception ex) : base("Web service read exception", ex)
		{
			
		}
	}
}
