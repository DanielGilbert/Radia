# Radia
[![Publish Docker image](https://github.com/DanielGilbert/Radia/actions/workflows/docker.yml/badge.svg)](https://github.com/DanielGilbert/Radia/actions/workflows/docker.yml)
![Docker Pulls](https://img.shields.io/docker/pulls/herrgilbert/radia)

## Introduction
*Radia* is inspired by the old days of Internet, where you would slab your web directory into the wild. Just start the Docker, add a Github repo as source, and you are good to go. You can of course also just reference a local folder on the file system. It's totally up to you.

## History
*Radia* started with the simple name of *Directory Listing* (🥱) in 2019. It powered my [private website](https://g5t.de) ever since. I was tired of dealing with a full-blown CMS and didn't like the static page generators. I just want it to work, being lean and easy at the same time.

I also happen to like to build stuff. So there goes that.

Recently, I got told that I have "no sufficient knowledge of backend technology" and my job application got turned down. So I'm currently building up knowledge, knowing that I will never apply at that place again.

The name comes from the likes of [Radia Perlman](https://en.wikipedia.org/wiki/Radia_Perlman), who is most famous for the invention of the [Spanning Tree Protocol](https://en.wikipedia.org/wiki/Spanning_Tree_Protocol). With this, she laid the foundation of what we nowadays refer to as "The Internet". Also, I like the name.

## Features
- Works on folders and/or Git repositories
- Can serve from multiple file repositories. Want your template in a different repo than your content? No Problem!
- Renders Markdown files
- Respects hidden file attributes (for the git file provider, hidden files have to start with a ".")
- ~~Just~~ Kinda works.™

## Usage
Your best bet is to use the provided docker container. I included a `docker-compose` file, which should help you get things going. There is `appsettings.json` located in the root of the `/app` folder, which you will need to adopt to your own needs. Let's have a look:


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

### Meta Files
Meta files (designated by the name `.meta`) allow you to add a description to files or directories. The file is basically a [INI file](https://en.wikipedia.org/wiki/INI_file), which looks like this:
```ini
[games]
Description=Some of the games I developed lately.
```
The section name (in brackets) is the name of file or folder that is affected by the key(s) that follow in this sections. In this case, the only key available is `Description`. So a folder called `games` would now show the description given in this example. The `.meta` file needs to be placed in the parent folder. So, if you want to give the folder `/content/games/` a description, the `.meta` file must be placed at `/content/.meta`, and the section must be called `[games]`.
## Building
I really encourage you to use the existing docker image. If you really like to build Radia on your own, you can do so by running `dotnet build Radia.sln`
### Tests
For the time being, the tests can be run using Windows. A `tests.runsettings`is located at the root of the project repository. This is needed and should be included in all `dotnet test Radia.sln` calls.