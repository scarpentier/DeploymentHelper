# DeploymentHelper
Replaces strings in your app configuration file(s) with data read from an Excel file.

It makes deployment of applications easier by avoiding the modification of multiple (and often complex) configuration files.

## Usage
DeploymentHelper.exe ExcelFile Environment InputFile OutputFile

## Exemple

    DeploymentHelper.exe Deployment.xlsx Dev Input.xml app.config
    
**Deployment.xlsx**

Key | Dev | QA | Preprod | Prod
--- | --- | --- | --- | ---
Debug | TRUE | FALSE | FALSE | FALSE
SiteUrl | localhost | someappqa | someapppp | someapp
Pi.Server | pidev | piqa | pipreprod | pi
Pi.User | pitest1 | pitest1 | someapp | someapp
Pi.Password | admin | admin | t2j8tBd | 768Wwb9

**Input.xml**

    <?xml version="1.0" encoding="utf-8" ?> 
    <configuration>
      <appSettings>
        <add key="Debug" value="{Debug}"/>
        <add key="SiteUrl" value="http://{Server.Name}"/>
      </appSettings>
      <connectionStrings>
        <add name="PIOLEDB" connectionString="Provider=PIOLEDB;Data Source={Pi.Server};User Id={Pi.User};Password={Pi.Password}"/>
      </connectionStrings>
    </configuration>
    
**Output.xml**

	<?xml version="1.0" encoding="utf-8" ?> 
	<configuration>
	  <appSettings>
	    <add key="Debug" value="TRUE"/>
	    <add key="SiteUrl" value="http://"/>
	  </appSettings>
	  <connectionStrings>
	    <add name="PIOLEDB" connectionString="Provider=PIOLEDB;Data Source=pidev;User Id=pitest1;Password=admin"/>
	  </connectionStrings>
	</configuration>

## Credits
* Uses the most excellent [LinqToExcel](https://github.com/paulyoder/LinqToExcel) library