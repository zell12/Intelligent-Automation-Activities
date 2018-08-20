# INTELLIGENT CUSTOM ACTIVITIES FOR UIPATH  

 These are packages and custom activities infused with AI - Machine Learning models, cognitive services and all those good stuffs. Each are carefully implemented and structured to provide a whole new meaning of intelligence to your automation workflows.   
 All of these are available to the community via [UiPath community gallery.](https://gallery.uipath.com/)  
  

# Getting Started  

 This section will show how you can install and utilize the custom activities in your automation workflow.  
  

## Prerequisites  
  

 - [x] **[UiPath Studio - CommunityEdition](https://www.uipath.com/community)**  
  

## How To  
  

 ### Install the activity packages in UiPath Studio  
  

 1. Open UiPath Studio and go to **Manage Packages** or hit `CTRL + P`.  
  
 2. Select **Community** category and search for `Zell.CognitiveServices.Activities`.
  
	  ![enter image description here](https://lh3.googleusercontent.com/bwqv_E1MbaR8encAYhiyCtI1RIzZl_0_EhijVEzSfc7VPcvdGZQ0ulTv0U3McjW5R7mPvtnn4ImX)  
  
 3. Install the specified packages.  
  

### Use the activities  

 1. Find and collapse `Zell Power Up` on the Available categories of Activity Explorer and expand `Cognitive Services`.  
  
	  ![enter image description here](https://lh3.googleusercontent.com/QNo0aMQlbGrsFPWY_pe64IZ2Xu1NgeJp6CWQSbup4DRA3mOQe7wjNZgwxklUaoKL_PQ-J8ujD_vI)

 > If the category cannot be found upon installation, you may need to  
 > restart UiPath Studio for the new activities to be available.  
  

 2. Drag and drop the desired activities into the activity canvas and supply the necessary parameters.   
  
 3. Read on below for further details on each activity.  
  

# Packages and Activity Inclusions  
## Cognitive Services  

Easy-to-use and intelligent automation activity APIs and libraries built on top of Microsoft Azure cognitive service platforms.  
##  `Version 1.1 (August 22, 2018)` 

    ## Additions
    - Additional intelligent activity - Text Moderation

### Text Moderation
Helps you detect potential profanity in more than 100 languages and match text against your custom lists automatically. Content Moderator also checks for possible personally identifiable information (PII). This activity comes with a free text moderator api key for development purposes. If for extensive use, its recommended to generate your own subscription key in Azure.

> You can [sign up here](https://azure.microsoft.com/en-us/try/cognitive-services/)  for the  **free**  trial key.  A free trial key is limited **only to 1 call per second.**
> **Alternately**, you can buy access key from  [Azure Portal](https://portal.azure.com/). While buying access key you may want to consider which tier is appropriate for you. More information is [here](https://azure.microsoft.com/en-us/pricing/details/cognitive-services/search-api/web/).

**Activity Parameters**  

![enter image description here](https://lh3.googleusercontent.com/1IWNWINm1XFOvcBLOb1egu1LXr5f8wEEQOCewEsq2LVt_mFq9SJIdaS2cMyhyb55Ey2CEu-eJda0)

![enter image description here](https://lh3.googleusercontent.com/aFe2f9nMcWSqbosSHhW5qREd3pNKUw8OyV8f4mPUc4dEbqD2W40Rj6JfsfNJ5NEh9RZjYQnO4VYX)


##  `Version 1.0 (August 16, 2018)`  
    ## Initial Build
### Document Translator  
Translate one or more Office documents, plain text HTML or PDF documents to another language, in one go.  This activity comes with a free text translator api key for development purposes. If for extensive use, its recommended to generate your own subscription key in Azure.

![](https://lh3.googleusercontent.com/llBGwq3__4TGa2gf6lkTqykbPswe6JYZv7t7qku4RLS7GMozPZLHARUmGc1ACGMCYpOOoI6_5lgV)  
  

**Activity Parameters**  

![](https://lh3.googleusercontent.com/NlN21cOqynAHuiyie4SAWri_OqV5QZrOSf50jH-axZIsEtkHsrsNSsP8MGaMYfeEmkTKXi4rTSKa)  
  
![](https://lh3.googleusercontent.com/XjftF_oVHpv7yBbLWMcc7sENczkXxTU8zcAHaJ0Sulvd5OlQYXDDOELUt1LTEP_DP8-RW23VNnhY)  
  

### Text Translator  

Cognitive service for natural language machine translation supporting over 60+ languages and dialects.  This activity comes with a free text moderator api key for development purposes. If for extensive use, its recommended to generate your own subscription key in Azure.

![](https://lh3.googleusercontent.com/vAmbU7PMgWDhs9c_GL3w_HUUJBb_NvH2j2X2TzxUXVo6GkgkGQgPX2mljs2sV2Nhe5RmObIDayQo)  
  
**Activity Parameters**  
  
![](https://lh3.googleusercontent.com/lb7X2wMfUg5QG0Ec702wu7srpJwajO9JUFWT6BuDacCuNnDBibGLpHnTwAEIHIPQZE9MlmlD87tY)  
  
![](https://lh3.googleusercontent.com/xEXTdbQQu36-FCuupNkNsXB2nowRZ6jn3s9seNOrMQ0Eifl1CPLnHM7LM2zpy14OayCeM_bXN0qx)  
  

> **Generating your own Azure key for the translation API**
> 
>  ![Sign into Azure](https://www.microsoft.com/en-us/translator/business/wp-content/uploads/sites/8/2018/06/Sign_into_icon.png) **1. Sign into Azure**
>   -   Don't have an account?  [Sign-up for a Microsoft Azure account](https://azure.com/)
>   -   Already have an account?  [Sign-in](https://portal.azure.com/)
>   
>  ![Subscribe to Microsoft Translator](https://www.microsoft.com/en-us/translator/business/wp-content/uploads/sites/8/2018/06/Subscribe_icon.png) **2. Subscribe to Microsoft Translator**
>   -   After you sign into Azure, go to the  [Cognitive Services](https://portal.azure.com/#create/Microsoft.CognitiveServices) section
>   -   Under "API Type" select the Text or Speech API
>   -   You can only add one Translator API subscription at a time
>   -   In the Pricing Tier section, select the pricing tier that best fits your needs
>   -   Each subscription has a free tier. The free tier has the same features and functionalities as the paid plans and does not have an expiration date
>   -   Fill out the rest of the form, and select the Create button
>   -   All subscriptions go into effect immediately
>    
>  ![Authentication Key](https://www.microsoft.com/en-us/translator/business/wp-content/uploads/sites/8/2018/06/Auth_icon.png) **3. Authentication Key**
>   -   Retrieve your authentication key by going to the menu All Resources > click on your subscription
>   -   The Key value is used for authentication, keep the Key value confidential. You will need this when you develop your app  
    

# Development and Debugging  

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.  

## Prerequisities  
  

 - [x] [Visual Studio 2017 (at least Community Edition)](https://www.visualstudio.com/)  
 - [x] [Nuget Package explorer](https://github.com/NuGetPackageExplorer/NuGetPackageExplorer/releases)  
 - [x] Clone or download this repository  
  

## Debugging  
  

### Via Visual Studio  

> Under construction  

### Via UiPath Studio  

> Under construction  
  

## Updating local package  
  

 1. Rebuild the solution in Visual Studio.  
  

> Optionally, you can add a version indicator on the class description  
> e.g. *v1 - Description of class*  
  
    

 2. On Nuget package explore, replace the libraries (*.dll) and its dependencies (as necessary), edit package metadata **VERSION**.  
  

> You may also add version indicator on package description itself  
  
    

 3. On UiStudio, go to Activities Explorer > Manage Packages > Update > Select package to update.  
    
 4. Restart UiPath Studio, if prompted.  
  

# Built With  
  

-   .NET Framework 4.6.1  
-  [Azure Machine Learning Studio](https://studio.azureml.net/) -  A fully-managed cloud service that enables to experiment, build, train, deploy, and share predictive analytics solutions.  
-  [Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/) - Advanced intelligence APIs harnessing the power of Machine Learning (Learning Algorithm - Multiclass Neural Network with 3 hidden layers)  
-  [Microsoft Document Translator](https://github.com/MicrosoftTranslator/DocumentTranslator) - Microsoft Document Translator translates Microsoft Office, plain text, HTML, PDF files and SRT caption files, from and to any of the 60+ languages supported by the Microsoft Translator web service  
  

# License  

Copyright (c) Russel Alfeche. All rights reserved.  
  

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details  