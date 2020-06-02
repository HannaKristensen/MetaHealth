# MetaHealth
MetaHealth is a health web application to help people with different diagnoses.
This project will help people manage or help with there mental illness.
The people working on this project are four Senoir Western Oregon University students,
<br/> 
Hanna Kristensen <br /> 
Brittany Miller 
https://github.com/millabilla/MetaHealth <br /> 
Abi Johnson
https://github.com/abijohnson/MetaHealth <br /> 
Jasmit Singh
https://github.com/ja5mit/MetaHealth <br /> 

# How to Set Up HelpALong
1. Download Repository
2. DownLoad NuGet packages

NuGet Packages to run HelpAlong

|Name| Version Number|
|------|-------------|
| Google.Apis.Auth | 1.43 |
| Google.Apis.Calendar.v3 | 1.43 |
| Google.Apis.Core | 1.44 |
| Google.Apis | 1.43.0 |
| EntityFramework | 6.4 |
| Microsoft.AspNet.Identity.Core | 2.2.3 |
| Microsoft.AspNet.Identity.EntityFramework | 2.2.3 |
| Microsoft.AspNet.Identity.Owin | 2.2.3 |
| Microsoft.AspNet.Mvc | 5.2.7 |
| Microsoft.AspNet.Razor | 3.2.7 |
| Microsoft.AspNet.WebApiCore | 5.2.7 |
| Antlr | 3.5.0.2 |
| bootstrap | 3.4.1 |
| jQuery | 3.3.1 |
| jQuery.Validation | 1.17.0 |


3. Create a config file called AppSettings.config and create a google api key. The file will look like this
```
<appSettings>
 	 <add key="ApiID" value="YOUR ID" />
 	 <add key="ApiSecret" value="YOUR SECRET" />
</appSettings>
```
In the Web.config file you will need to add th file to AppSetting like this:
```  
<appSettings file=”AppSettings.config”>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  </appSettings>
```

Under the Connection String you will need to add your database connection string and change the context in all of the files in the DAL folder

4. Then you are all ready to go!

# E-R Diagram
![E-R Diagram](/Team%20Info/E-R-Diagram.PNG)

