---
page_type: sample
languages:
- csharp
products:
- azure
- azure-functions
- azure-media-services
name: "Azure Media Services v3 - Serverless Workflows with Azure Functions and Logic Apps"
description: "Projects that show how to integrate Azure Media Services with Azure Functions and Azure Logic Apps."
azureDeploy: https://raw.githubusercontent.com/Azure-Samples/media-services-v3-dotnet-core-functions-integration/master/azuredeploy.json
---

# Azure Media Services v3 - Serverless Workflows with Azure Functions & Logic Apps 
This repository contains projects that show how to integrate Azure Media Services with Azure Functions & Azure Logic Apps.
These Media Services Functions examples are based on AMS REST API v3 on Azure Functions v2. Most of the functions can also be used from Logic Apps.

This repository can be accessed directly using https://aka.ms/ams3functions.

## Prerequisites for a sample Logic Apps deployments

### 1. Create an Azure Media Services account

Create a Media Services account in your subscription if don't have it already ([follow this article](https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-portal-create-account)).

### 2. Create a Service Principal

Create a Service Principal and save the password. It will be needed in step #4. To do so, go to the API tab in the account ([follow this article](https://docs.microsoft.com/en-us/azure/media-services/media-services-portal-get-started-with-aad#service-principal-authentication)).

### 3. Make sure the AMS streaming endpoint is started

To enable streaming, go to the Azure portal, select the Azure Media Services account which has been created, and start the default streaming endpoint ([follow this article](https://docs.microsoft.com/en-us/azure/media-services/previous/media-services-portal-vod-get-started#start-the-streaming-endpoint)).

### 4. Deploy the Azure functions

If not already done : fork the repo, deploy Azure Functions and select the right project (IMPORTANT!).

Follow the guidelines in the [git tutorial](1-CONTRIBUTION-GUIDE/git-tutorial.md) for details on how to fork the project and use Git properly with this project.

Note : if you never provided your GitHub account in the Azure portal before, the continuous integration probably will probably fail and you won't see the functions. In that case, you need to setup it manually. Go to your azure functions deployment / Functions app settings / Configure continous integration. Select GitHub as a source and configure it to use your fork.

<a href="https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure-Samples%2Fmedia-services-v3-dotnet-core-functions-integration%2Fmaster%2Fazuredeploy.json" target="_blank"><img src="http://azuredeploy.net/deploybutton.png"/></a>
