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

2. Select **Community** category and search for `Zell.MachineLearningModels.Activities`.

	  ![enter image description here](https://lh3.googleusercontent.com/Dqvi2dbCnbOO3eU2Ul4FxkWoIqoAptOIcKqL81GPsfvnonX-WhsJv5NyA5sbxlu69Fv2Ns-thoMv)

4. Install the specified packages.

### Use the activities
1. Find and collapse `Zell Power Up`  on the Available categories of Activity Explorer and expand `Machine Learning`.

	![enter image description here](https://lh3.googleusercontent.com/zKQtzA9YR6Irot0Ymsa3uH1Fwv34qniM4SVY4Yj5HttjNHwqlxtuyBX8pdlYjoPhIkGRmc1QvgGM)

> 	If the category cannot be found upon installation, you may need to
> restart UiPath Studio for the new activities to be available.

2. Drag and drop the desired activities into the activity canvas and supply the necessary parameters. 

3. Read on below for further details on each activity.

# Packages and Activity Inclusions
## Machine Learning Models
Activities integrated into a powerful cloud-based predictive analytics service that makes it possible to quickly create and deploy predictive models as analytics solutions.
## `Version 1.1 (August 22, 2018)`

    ## Additions
    - Additional ML model - Credit Grant Assessor

### Credit Grant Assessor
This activity can be used to easily incorporate credit risk assessment into your automation. Credit Risk models play a key role in the assessment of two main risk drivers. 1)Willingness to pay and 2) Ability to pay. Credit scoring algorithms, which make a guess at the probability of default, are the method banks use to determine whether or not a loan should be granted.

![enter image description here](https://lh3.googleusercontent.com/UmvVXCtia-Uu9lrCSmczidcA2F8rxl1BIuWwIhOIXg0BJPMzSQx4A8FktAMj4PDlt6r_0qTKF2I8)

**Activity Parameters**

![enter image description here](https://lh3.googleusercontent.com/Xz9SIhQdRkOfLO0JB1Rsn2X5ag8WpGiTetRO7E84FqcGXeCGC6zFdUK2yCa46DmtXIpnisoToUu7)

## `Version 1.0 (August 16, 2018)`

    ## Initial Build

### Email ticket classifier 
Email classification experiment to assign an email to one or more class(es) of predefined set of classes or work queues.
![enter image description here](https://lh3.googleusercontent.com/6WM3mKrDX683x5R8lPahVuIe2jKuI5X4lxUtnPM1T80A6wGneOj-ZisfIhr7600OndF0Q2hFeRO2)

**Activity Parameters**

![enter image description here](https://lh3.googleusercontent.com/8ODQ5H_dd6JOe7e4IhWZuzIszZqoIqBABfUOYMXGypF3iVu11qAQGPViw44rAe55GBRI9-5yTH1V)

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
-   Learning Algorithm - Multi-class neural network

# License
Copyright (c) Russel Alfeche. All rights reserved.

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details