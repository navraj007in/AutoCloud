using System;
using System.Text;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System.Threading.Tasks;

using AccessTier = Azure.ResourceManager.Storage.Models.AccessTier;
using Sku = Azure.ResourceManager.Storage.Models.Sku;

namespace AutoCloud
{
    class Program
    {
         const string ResourceRegion = "West US";

        static async Task Main(string[] args)
        {
            string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
            Console.WriteLine("Subscription ID:"+subscriptionId);
            var credential = new DefaultAzureCredential();

            var resourcesManagementClient = new ResourcesManagementClient(subscriptionId, credential);

            Console.WriteLine("Subscription : "+subscriptionId);

            Console.WriteLine("Welcome to AutoCloud.! Manage All your Cloud Infrastructure from Here.");
            Console.WriteLine("1. List Your Resources.");
            Console.WriteLine("2. Create a New Resource.");
            Console.WriteLine("3. Modify an Existing Resource");
            Console.WriteLine("4. Delete a Resource");
            Console.WriteLine("5. Configure your Cloud Account");
            Console.WriteLine("6. Exit");

            int action =0;
            action =  Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(action);
            switch (action)
            {
                case 1:
                    Console.WriteLine("Listing your resources");
                    await CreateResourceGroupAsync(resourcesManagementClient);
                    break;
                default:
                    break;
            }
        }

        private static async Task<string> CreateResourceGroupAsync(ResourcesManagementClient resourcesManagementClient)
        {
            string resourceGroupName = RandomName("rg", 20);
            Console.WriteLine($"Creating resource group {resourceGroupName}...");
            await resourcesManagementClient.ResourceGroups.CreateOrUpdateAsync(resourceGroupName, new ResourceGroup(ResourceRegion));
            Console.WriteLine("Done!");

            return resourceGroupName;
        }

        /// <summary>
        /// Generates a random name.
        /// </summary>
        /// <param name="prefix">The text to include at the beginning of the name.</param>
        /// <param name="maxLen">The total length of the name.</param>
        /// <returns></returns>
        static string RandomName(string prefix, int maxLen)
        {
            var random = new Random();
            var sb = new StringBuilder(prefix);
            for (int i = 0; i < (maxLen - prefix.Length); i++)
                sb.Append(random.Next(10).ToString());
            return sb.ToString();
        }
    }

    
}
