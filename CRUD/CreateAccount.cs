using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Math;
using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace CRUD
{
    public static class CreateAccount
    {
        [FunctionName("CreateAccount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //var service = (IOrganizationService)new ServiceClient($@"AuthType=ClientSecret;url={"https://org2a7db823.crm5.dynamics.com/"};ClientId={"19d53033-0a67-479b-b1c1-7a1e724fd398"};ClientSecret={"DQJ7Q~XtcmTXtMwPq8SzSSpqvLujFeQckax5E"}");
            var service = Connect.GetOrganizationServiceClientSecret(
                "19d53033-0a67-479b-b1c1-7a1e724fd398",
                "DQJ7Q~XtcmTXtMwPq8SzSSpqvLujFeQckax5E",
                 "https://org2a7db823.crm5.dynamics.com/");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<AccountRequestCreate>(requestBody);

            if (!string.IsNullOrEmpty(data.name) && !string.IsNullOrWhiteSpace(data.name))
            {
                Entity newAccount = new Entity(App.Custom.Account.EntityName);
                newAccount[App.Custom.Account.PrimaryName] = data.name;
                var createacc = service.Create(newAccount);
                //log.LogInformation("Account created with ID: " + createacc);
                AccountResponseCreate accountid = new AccountResponseCreate();
                accountid.accountId = createacc.ToString();

                string json = JsonConvert.SerializeObject(accountid);

                return new OkObjectResult(json);
            }
            else
            {
                log.LogInformation("No account created.");
            }
                              
            return new OkResult();
        }
    }

    public class AccountResponseCreate
    {
        public string accountId { get; set; }
    }

    public class AccountRequestCreate
    {
        public string name { get; set; }
    }


}
