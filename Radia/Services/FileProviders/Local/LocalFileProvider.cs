﻿using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Radia.Exceptions;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileProvider : IRadiaFileProvider
    {
        private readonly IFileProvider fileProvider;
        private readonly string rootPath;

        public FileProviderEnum FileProviderEnum { get; }

        public char PathDelimiter => Path.DirectorySeparatorChar;

        public LocalFileProvider(IFileProviderConfiguration fileProviderConfiguration, IFileProvider? frameworkFileProvider = null)
        {
            if (fileProviderConfiguration.FileProvider == FileProviderEnum.Local)
            {
                this.rootPath = fileProviderConfiguration.Settings["RootDirectory"];
                if (frameworkFileProvider == null)
                {
                    this.fileProvider = new PhysicalFileProvider(this.rootPath);
                }
                else
                {
                    this.fileProvider = frameworkFileProvider;
                }

                this.FileProviderEnum = FileProviderEnum.Local;
            }
            else
            {
                throw new InvalidFileProviderException();
            }
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return this.fileProvider.GetDirectoryContents(subpath);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return this.fileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return this.fileProvider.Watch(filter);
        }
    }
}
