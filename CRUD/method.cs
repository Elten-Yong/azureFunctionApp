using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CRUD
{
    public class Authentication
    {
        public string clientId { get; set; }
        public string organizationUri { get; set; }
        public string clientSecret { get; set; }
    }

    public class Connect
    {
        public static IOrganizationService GetOrganizationServiceClientSecret(string clientId, string clientSecret, string organizationUri)
        {
            try
            {
                var conn = new ServiceClient($@"AuthType=ClientSecret;url={organizationUri};ClientId={clientId};ClientSecret={clientSecret}");

                //return conn.OrganizationWebProxyClient != null ? conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
                return null;
            }
        }
    }

     class AccountMethod{
        public static DataCollection<Entity> retrieveAccountData(IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("account")
            {
                Distinct = true,
                ColumnSet = new ColumnSet("accountid","name"),
            };

            DataCollection<Entity> accountEntityCollection = service.RetrieveMultiple(query).Entities;

            // Display the results.
            

            return accountEntityCollection;


        }

        public static Guid retreiveChoosenRecordId(int choosenNo, IOrganizationService service)
        {

            QueryExpression query = new QueryExpression("account")
            {
                Distinct = true,
                ColumnSet = new ColumnSet("accountid", "name"),
            };

            DataCollection<Entity> accountEntityCollection = service.RetrieveMultiple(query).Entities;

            return (Guid)accountEntityCollection[choosenNo - 1].Attributes["accountid"];
        }

        public static string accountNameValidation()
        {
            var accountName = "";
            while (true)
            {
                try
                {
                    accountName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(accountName))
                        throw new Exception();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a correct name.");
                }

            }

            return accountName;


        }

        public static int selectAccount(int noToChoose)
        {
            int choosenNoForSelection = 0;
            while (true)
            {
                try
                {
                    Console.Write("\nChoose the account no: ");
                    choosenNoForSelection = int.Parse(Console.ReadLine());
                    if (choosenNoForSelection < 1 || choosenNoForSelection > noToChoose)
                        throw new Exception();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nWrong no choosen.");
                }
            }

            return choosenNoForSelection;
        }
    }

}
