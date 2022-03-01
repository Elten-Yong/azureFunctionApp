using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


namespace functionApp
{
    public class Authentication
    {
        public string clientId { get; set; }
        public string organizationUri { get; set; }
        public string clientSecret { get; set; }
    }

    class CRUD
    {

        static void Main(string[] args)
        {

            var config = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("AppConfig.json").Build();

            var section = config.GetSection(nameof(Authentication));
            var authentication = section.Get<Authentication>();

            var service = Connect.GetOrganizationServiceClientSecret(
                  authentication.clientId,
                  authentication.clientSecret,
                  authentication.organizationUri);
            //Value: "DQJ7Q~XtcmTXtMwPq8SzSSpqvLujFeQckax5E"

            //    secre: "977c25bc-a7fc-4f48-a87f-d764c9c084b0"
            WhoAmIRequest request = new WhoAmIRequest();

            WhoAmIResponse response = (WhoAmIResponse)service.Execute(request);

            Console.WriteLine("Your UserId is {0}", response.UserId);

            while (true)
            {
                Console.WriteLine("\n1. Create");
                Console.WriteLine("2. Retrieve");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Exit");

                var chooseNoMenu = 0;

                while (true)
                {
                    try
                    {
                        Console.Write("\nPlease enter the number to choose the selection: ");
                        chooseNoMenu = Convert.ToInt32(Console.ReadLine());
                        if (chooseNoMenu < 1 || chooseNoMenu > 5)
                            throw new Exception();

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please enter the correct number!");
                    }
                }

                switch (chooseNoMenu)
                {
                    case 1:
                        Console.Write("Please enter your account name: ");

                        var accountNameCreate = accountNameValidation();

                        Entity newAccount = new Entity("account");
                        newAccount["name"] = accountNameCreate;
                        var createacc = service.Create(newAccount);
                        Console.WriteLine("Account created with ID: " + createacc);
                        break;

                    case 2:

                        retrieveAccountData(service);
                        Console.WriteLine("\n[End of Listing]");

                        break;

                    case 3:
                        int noToChoose = retrieveAccountData(service);

                        Guid accountIdUpdate = retreiveChoosenRecordId(selectAccount(noToChoose), service);

                        Console.Write("Please enter your new account name: ");
                        var accountNameUpdate = accountNameValidation();

                        var accountUpdate = new Entity("account", accountIdUpdate)
                        {
                            ["name"] = accountNameUpdate
                        };

                        service.Update(accountUpdate);
                        Console.WriteLine("Account update Successfully. ");
                        break;

                    case 4:
                        noToChoose = retrieveAccountData(service);

                        Guid accountIdDelete = retreiveChoosenRecordId(selectAccount(noToChoose), service);
                        service.Delete("account", accountIdDelete);
                        Console.WriteLine("Entity record(s) have been deleted.");
                        break;

                    default:
                        Console.WriteLine("Exit");
                        break;
                }

                if (chooseNoMenu != 5)
                {
                    Console.WriteLine("Do you want to continue with the program? (press y to continue other key to exit.)");
                    var exit = "";

                    try
                    {
                        exit = Console.ReadLine();
                    }
                    catch (Exception)
                    {

                    }
                    if (exit.ToLower() != "y")
                        break;
                }
                else
                    break;


            }

            Console.ReadLine();
        }

        private static object ConfigurationBuilder()
        {
            throw new NotImplementedException();
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

        private static int retrieveAccountData(IOrganizationService service)
        {
            QueryExpression query = new QueryExpression("account")
            {
                Distinct = true,
                ColumnSet = new ColumnSet("name"),
            };

            DataCollection<Entity> accountEntityCollection = service.RetrieveMultiple(query).Entities;

            // Display the results.
            for (int i = 1; i <= accountEntityCollection.Count; i++)
            {
                Console.WriteLine("\n{0}. Account name: {1} ", i, accountEntityCollection[i - 1].Attributes["name"]);
            }

            return accountEntityCollection.Count;


        }

        private static Guid retreiveChoosenRecordId(int choosenNo, IOrganizationService service)
        {

            QueryExpression query = new QueryExpression("account")
            {
                Distinct = true,
                ColumnSet = new ColumnSet("accountid"),
            };

            DataCollection<Entity> accountEntityCollection = service.RetrieveMultiple(query).Entities;

            return (Guid)accountEntityCollection[choosenNo - 1].Attributes["accountid"];
        }

        private static string accountNameValidation()
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

        private static int selectAccount(int noToChoose)
        {
            int choosenNoForSelection = 0;
            while (true)
            {
                try
                {
                    Console.Write("\nChoose the number account: ");
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
