using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CRUD
{
    public static class RetrieveAccount
    {
        [FunctionName("RetrieveAccount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var service = Connect.GetOrganizationServiceClientSecret(
                "19d53033-0a67-479b-b1c1-7a1e724fd398",
                "DQJ7Q~XtcmTXtMwPq8SzSSpqvLujFeQckax5E",
                 "https://org2a7db823.crm5.dynamics.com/");

            var accountRecords = AccountMethod.retrieveAccountData(service);

            JArray accountArray = new JArray();
            //var accountMessage = "";
            for (int i = 1; i <= accountRecords.Count; i++)
            {
                JObject accountObjectTemp = new JObject(
                    new JProperty(App.Custom.Account.PrimaryName, accountRecords[i - 1].Attributes[App.Custom.Account.PrimaryName].ToString()),
                    new JProperty(App.Custom.Account.PrimaryKey, accountRecords[i - 1].Attributes[App.Custom.Account.PrimaryKey].ToString()));


                accountArray.Add(accountObjectTemp);

            }

            JObject accountObject = new JObject();
            accountObject["AccountArray"] = accountArray;

            string json = accountObject.ToString();

            //string responseMessage = string.IsNullOrEmpty(accountRecords.ToString())
            //    ? "This HTTP triggered function executed successfully."
            //    : accountMessage;          

            return new OkObjectResult(json);
        }
    }

}
