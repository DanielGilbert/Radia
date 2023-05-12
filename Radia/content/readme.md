## Hi there!
Looks like you are running Radia with only the default settings. While you can do that, it really shines once you add your own content to it.

## Usage
Your best bet is to use the provided docker container. There is `appsettings.json` located in the root of the `/app` folder, which you will need to adopt to your own needs. Let's have a look:

``` json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AppConfiguration": {
    "FileProviderConfigurations": [
      {
        "AllowListing": true,
        "Settings": {
          "RootDirectory": "/app/content/"
        }
      },
      {
        "AllowListing": false,
        "Settings": {
          "RootDirectory": "/app/templates/default/views/"
        }
      },
      {
        "AllowListing": false,
        "Settings": {
          "RootDirectory": "/app/templates/default/"
        }
      }
    ],
    "WebsiteTitle": "Radia",
    "FooterCopyright": "&copy; &lt;Your name should go here&gt;, 1970 - {{CurrentYear}}",
    "DefaultPageHeader": "Welcome to Radia"
  }
}
```

This is the default `appsettings.json`, as it is in the docker container and in the repository.

## Properties

| Property                   | Description                                                                                               |
| -------------------------- | --------------------------------------------------------------------------------------------------------- |
| Logging                    | This allows you to define and set the logging level.                                                      |
| FileProviderConfigurations | This contains the main gist of Radia. Here you are able to reference all the different sources of content |
| WebsiteTitle               | This will set the title you can see on the browser window                                                 |
| FooterCopyright            | The Copyright information to display in your footer, if any.                                              |
| DefaultPageHeader          | You can set the page header of the default template. |


## File Providers
### Filesystem
This is the default file provider for Radia. It takes a directory from the file system, and will display it's contents on the root. So, if you want to have a directory called `bar` visible on the index page, and this directory is under `foo/bar`, then your RootDirectory in this case becomes `foo`. So you always refer to the parent directory of the directory you want to see in the index.

You use the `Filesystem File Provider` simply by adding `RootDirectory` to the `Settings` object, as you can see above.

``` json
{
  "FileProviderConfigurations": [
    {
      "AllowListing": true,
      "Settings": {
        "RootDirectory": "/app/content/"
      }
    }
  ]
}
```

### Git
Another nice aspect about Radia is that it can directly display the contents of a git repository. It does that by checking out the repository on the first run, and then fetching it every 10 minutes. In future versions, this might be configurable. As the repository is cloned locally, you need to have enough space available.

``` json
{
  "FileProviderConfigurations": [
    {
      "AllowListing": true,
      "Settings": {
        "Repository": "https://github.com/DanielGilbert/g5t.de.git",
        "Branch": "main"
      }
    }
  ]
}
```

### General Settings

| Property     | Description                                                                                                                               |
| ------------ | ----------------------------------------------------------------------------------------------------------------------------------------- |
| AllowListing | This boolean decides if the content of this directory will be listed on the index page. I do not recommend that for template directories or similar. |