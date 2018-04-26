using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using PCWCodeExamplesCSharpEmail.Models;

/*

    Email validation with C#
    Simple demo which passes email address to the API on form submit and shows a message based on response.

    Including showing alternative suggestions for common typos such as gamil.com instead of gmail.com

    Full email validation API documentation:-
    https://developers.alliescomputing.com/postcoder-web-api/email-validation
    
*/

namespace PCWCodeExamplesCSharpEmail.Controllers
{
	public class EmailLookupController : ApiController
    {
		[HttpGet]
		[Route("PCWCodeExamples/EmailLookup")]
		public string EmailLookup()
		{
			// The end '/' is required to stop the period in the email address breaking our URL
			return "Pass an email address by appending /you@domain.com/";
		}

		[HttpGet]
		[Route("PCWCodeExamples/EmailLookup/{email}")]
		public async Task<EmailReturn> EmailLookup(string email)
		{
			// Replace with your API key, test key will always return true regardless of email address
			string apiKey = "PCW45-12345-12345-1234X";

			// Grab the input text and trim any whitespace
			email = email.Trim();

			// URL encode our input string
			email = HttpUtility.UrlEncode(email);

			// Create empty containers for our output
			EmailLookup emailResp = new EmailLookup();
			EmailReturn output = new EmailReturn();

			if (String.IsNullOrEmpty(email))
			{
				// Respond without calling API if no input supplied
				output.error_message = "No input supplied";
			}
			else
			{
				// Create the URL to API including API key and encoded email address
				string emailUrl = $"https://ws.postcoder.com/pcw/{apiKey}/emailaddress/{email}";

				// Create a disposable HTTP client
				using (HttpClient client = new HttpClient())
				{
					// Specify "application/json" in content-type header to request json return values
					client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					
					// Execute our get request
					using (HttpResponseMessage resp = await client.GetAsync(emailUrl))
					{
						// Triggered if API does not return 200 HTTP code
						// More info - https://developers.alliescomputing.com/postcoder-web-api/error-handling

						// Here we will output a basic message with HTTP code
						if (!resp.IsSuccessStatusCode)
						{
							output.error_message = $"An error occurred - {resp.StatusCode.ToString()}";
						}
						else
						{
							// Store JSON response in our EmailLookup object
							emailResp = JsonConvert.DeserializeObject<EmailLookup>(await resp.Content.ReadAsStringAsync());

							// Store the results of our lookup in our return wrapper
							output.email = emailResp;

							// Do something based on whether or not the email address is valid
							if (emailResp.valid)
							{
								// Something good
							}
							else
							{
								// Something bad
							}
						}

						// Note: If "score" is less than 100, it may be a generic sales@ mailbox, disposable email address or a catch all server
						// More info - https://www.alliescomputing.com/support/validating-email-addresses

						// Full list of "state" responses - https://developers.alliescomputing.com/postcoder-web-api/email-validation
					}
				}
			}
			
			return output;
		}
    }
}