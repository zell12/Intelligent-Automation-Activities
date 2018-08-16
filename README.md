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

2. Select **Community** category and search for `Zell.CognitiveServices.Activities` and `Zell.MachineLearningModels.Activities`.
![enter image description here](https://lh3.googleusercontent.com/B_50yv3r2itMB4D1EjngnnMtzjGapG_SE6_LJ-9VOWCHQXAVW2OXRWYEEA5QoM6nPQUAH2KPkkLO)

4. Install the specified packages.

### Use the activities
1. Find and collapse `Zell Power Up`  on the Available categories of Activity Explorer.
![enter image description here](https://lh3.googleusercontent.com/ABabFFqYqKLzWTmDZHq2CojO-2ke51HjSkWqwHAGkZ9eklNTd7M-giqqMXRzJdyGfIgUwCy2c7Dw)

> 	If the category cannot be found upon installation, you may need to
> restart UiPath Studio for the new activities to be available.

2. Drag and drop the desired activities into the activity canvas and supply the necessary parameters. 

3. Read on below for further details on each activity.

# Packages and Activity Inclusions
## `Version 1.0 (August 16, 2018)`
## Cognitive Services
Easy-to-use and intelligent automation activity APIs and libraries built on top of Microsoft Azure cognitive service platforms.
### Document Translator

Translate one or more Office documents, plain text HTML or PDF documents to another language, in one go.
![enter image description here](https://lh3.googleusercontent.com/llBGwq3__4TGa2gf6lkTqykbPswe6JYZv7t7qku4RLS7GMozPZLHARUmGc1ACGMCYpOOoI6_5lgV)

**Activity Parameters**
|Name|Type|Required?|Remarks|
|----|----|----|----|
|File/s|  IN  |Y|via File Multiselect Dialog|
|To   |  IN  |Y|Language ISO code e.g. 'fil', 'en', 'ro'|
| API Key |IN|N|Defaults to a FREE key if not supplied -  up to 2M chars - see **Generating your own Azure key...** section|
| Translated Files |OUT|N/A|List of translated files delimited by comma|
![enter image description here](https://lh3.googleusercontent.com/XjftF_oVHpv7yBbLWMcc7sENczkXxTU8zcAHaJ0Sulvd5OlQYXDDOELUt1LTEP_DP8-RW23VNnhY)

### Text Translator
Cognitive service for natural language machine translation supporting over 60+ languages and dialects.
![enter image description here](https://lh3.googleusercontent.com/vAmbU7PMgWDhs9c_GL3w_HUUJBb_NvH2j2X2TzxUXVo6GkgkGQgPX2mljs2sV2Nhe5RmObIDayQo)

**Activity Parameters**
|Name|Type|Required?|Remarks|
|----|----|----|----|
|Text|  IN  |Y|Text to translate|
|To   |  IN  |Y|Language ISO code e.g. 'fil', 'en', 'ro'|
| API Key |IN|N|Defaults to a FREE key if not supplied -  up to 2M chars - see **Generating your own Azure key...** section|
| Translated Text|OUT|N/A|Text translated to target language|
| Detected Source Language|OUT|N/A|Source language detected|
![enter image description here](https://lh3.googleusercontent.com/xEXTdbQQu36-FCuupNkNsXB2nowRZ6jn3s9seNOrMQ0Eifl1CPLnHM7LM2zpy14OayCeM_bXN0qx)

> **Generating your own Azure key for the translation API**
> 
>    ![Sign into Azure](https://www.microsoft.com/en-us/translator/business/wp-content/uploads/sites/8/2018/06/Sign_into_icon.png)  **1. Sign into Azure**
>   -   Don't have an account?  [Sign-up for a Microsoft Azure account](https://azure.com/)
>   -   Already have an account?  [Sign-in](https://portal.azure.com/)
>   
>  ![Subscribe to Microsoft Translator](https://www.microsoft.com/en-us/translator/business/wp-content/uploads/sites/8/2018/06/Subscribe_icon.png)    **2. Subscribe to Microsoft Translator**
>   -   After you sign into Azure, go to the  [Cognitive Services](https://portal.azure.com/#create/Microsoft.CognitiveServices) section
>    -   Under "API Type" select the Text or Speech API
>    -   You can only add one Translator API subscription at a time
>    -   In the Pricing Tier section, select the pricing tier that best fits your needs
>    -   Each subscription has a free tier. The free tier has the same features and functionalities as the paid plans and does not have an expiration date
>    -   Fill out the rest of the form, and select the Create button
>    -   All subscriptions go into effect immediately
>    
>    ![Authentication Key](https://www.microsoft.com/en-us/translator/business/wp-content/uploads/sites/8/2018/06/Auth_icon.png)   **3. Authentication Key**
>    -   Retrieve your authentication key by going to the menu All Resources > click on your subscription
>    -   The Key value is used for authentication, keep the Key value confidential. You will need this when you develop your app

    
  

## Machine Learning Models
Activities integrated into a powerful cloud-based predictive analytics service that makes it possible to quickly create and deploy predictive models as analytics solutions.

### Email ticket classifier 
Email classification experiment to assign an email to one or more class(es) of predefined set of classes or work queues.
![enter image description here](https://lh3.googleusercontent.com/6WM3mKrDX683x5R8lPahVuIe2jKuI5X4lxUtnPM1T80A6wGneOj-ZisfIhr7600OndF0Q2hFeRO2)

**Activity Parameters**
|Name|Type|Required?|Remarks|
|----|----|----|----|
|Email Subject|  IN  |Y|Subject of the email to be classified|
|Email Description   |  IN  |Y|Brief description of the email to be classified|
| Case Type |OUT|N/A|Predicted case type classification|
| Case Subject|OUT|N/A|Predicted case subject classification|
| Case QUeue|OUT|N/A|Predicted queue name classification|
![enter image description here](https://lh3.googleusercontent.com/B6iqVZovaDHc68dpKG5OeLecs34vDArXmJHW8k2uNrc8VYBfeQccq1KG3cAXiQ01TpyK2ho8Tp98)

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
-   [Azure Machine Learning Studio](https://studio.azureml.net/) -  A fully-managed cloud service that enables to experiment, build, train, deploy, and share predictive analytics solutions.
-   [Azure Cognitive Services](https://azure.microsoft.com/en-us/services/cognitive-services/) - Advanced intelligence APIs harnessing the power of Machine Learning
-   [Microsoft Document Translator](https://github.com/MicrosoftTranslator/DocumentTranslator) - Microsoft Document Translator translates Microsoft Office, plain text, HTML, PDF files and SRT caption files, from and to any of the 60+ languages supported by the Microsoft Translator web service

# License
Copyright (c) Russel Alfeche. All rights reserved.

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details