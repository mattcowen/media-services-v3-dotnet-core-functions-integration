//
// Azure Media Services REST API v3 Functions
//
// CreateEmptyAsset - This function creates an empty asset.
//
/*
```c#
Input:
    {
        // [Required] The name of the asset
        "assetNamePrefix": "TestAssetName",

        // [Required] The name of attached storage account where to create the asset
        "assetStorageAccount":  "storage01",
    }
Output:
    {
        // The name of the asset created
        "assetName": "TestAssetName-180c777b-cd3c-4e02-b362-39b8d94d7a85",

        // The identifier of the asset created
        "assetId": "nb:cid:UUID:68adb036-43b7-45e6-81bd-8cf32013c810",

        // The name of the destination container name for the asset created
        "destinationContainer": "destinationContainer": "asset-4a5f429c-686c-4f6f-ae86-4078a4e6139e"
    }

```
*/
//
//

using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using advanced_vod_functions_v3.SharedLibs;


namespace advanced_vod_functions_v3
{
    public static class CreateEmptyAsset
    {
        [FunctionName("CreateEmptyAsset")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"AMS v3 Function - CreateEmptyAsset was triggered!");

            string requestBody = new StreamReader(req.Body).ReadToEnd();

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if (data.assetNamePrefix == null)
            {
                return new BadRequestObjectResult("Please pass assetNamePrefix in the input object");
            }

            string assetStorageAccount = null;

            if (data.assetStorageAccount == null)
            {
                return new BadRequestObjectResult("Please pass assetStorageAccount in the input object");
            }

            assetStorageAccount = data.assetStorageAccount;

            string assetName = data.assetNamePrefix;

            int guidDelimiter = assetName.IndexOf('.');

            string uniqueAssetNameGuid = assetName.Substring(0, guidDelimiter);

            log.LogInformation(uniqueAssetNameGuid);

            assetName = "Input-" + assetName;

            Guid assetGuid = Guid.Parse(uniqueAssetNameGuid);

            log.LogInformation(assetGuid.ToString());

            MediaServicesConfigWrapper amsconfig = new MediaServicesConfigWrapper();

            Asset asset = null;

            try
            {
                IAzureMediaServicesClient client = MediaServicesHelper.CreateMediaServicesClientAsync(amsconfig);

                Asset assetParams = new Asset(null, assetName, null, assetGuid, DateTime.Now, DateTime.Now, null, assetName, null, assetStorageAccount, AssetStorageEncryptionFormat.None);
                
                asset = client.Assets.CreateOrUpdate(amsconfig.ResourceGroup, amsconfig.AccountName, assetName, assetParams);
                
                //asset = client.Assets.CreateOrUpdate(amsconfig.ResourceGroup, amsconfig.AccountName, assetName, new Asset());
            }
            catch (ApiErrorException e)
            {
                log.LogError($"ERROR: AMS API call failed with error code: {e.Body.Error.Code} and message: {e.Body.Error.Message}");
                return new BadRequestObjectResult("AMS API call error: " + e.Message + "\nError Code: " + e.Body.Error.Code + "\nMessage: " + e.Body.Error.Message);
            }
            catch (Exception e)
            {
                log.LogError($"ERROR: Exception with message: {e.Message}");
                return new BadRequestObjectResult("Error: " + e.Message);
            }

            // compatible with AMS V2 API
            string assetId = "nb:cid:UUID:" + asset.AssetId;


            string destinationContainer = "-asset-" + asset.AssetId;

            return (ActionResult)new OkObjectResult(new
            {
                assetName = assetName,
                assetId = assetId,
                destinationContainer = destinationContainer
            });
        }
    }
}
