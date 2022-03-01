using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;

namespace CRUD
{
    public static class UpdateAccount
    {
        [FunctionName("UpdateAccount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var service = Connect.GetOrganizationServiceClientSecret(
                "19d53033-0a67-479b-b1c1-7a1e724fd398",
                "DQJ7Q~XtcmTXtMwPq8SzSSpqvLujFeQckax5E",
                 "https://org2a7db823.crm5.dynamics.com/");

          
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<AccountRequestUpdate>(requestBody);


            if (!string.IsNullOrEmpty(data.accountid) && !string.IsNullOrWhiteSpace(data.accountid) && !string.IsNullOrEmpty(data.name) && !string.IsNullOrWhiteSpace(data.name))
            {
                if (System.Guid.TryParse(data.accountid, out Guid accountid))
                {
                    var accountUpdate = new Entity(App.Custom.Account.EntityName, accountid)
                    {
                        [App.Custom.Account.PrimaryName] = data.name
                    };

                    service.Update(accountUpdate);

                    log.LogInformation("Account update successfully.");
                }
                else
                {
                    log.LogInformation("Wrong account id.");
                }
            }
            else
            {
                log.LogInformation("Wrong name or id.");
            }





            return new OkResult();
        }
    }

    public class AccountRequestUpdate
    {
        public string accountid { get; set; }

        public string name { get; set; }
    }
}
