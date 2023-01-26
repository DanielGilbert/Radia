﻿using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders.Git
{
    public class GitFileProvider : IRadiaFileProvider
    {
        public FileProviderEnum FileProviderEnum => FileProviderEnum.Git;

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
