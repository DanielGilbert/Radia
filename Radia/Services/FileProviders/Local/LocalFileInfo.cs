﻿using Microsoft.Extensions.FileProviders;

namespace Radia.Services.FileProviders.Local
{
    public class LocalFileInfo : IFileInfo
    {
        public bool Exists => throw new NotImplementedException();

        public bool IsDirectory => throw new NotImplementedException();

        public DateTimeOffset LastModified => throw new NotImplementedException();

        public long Length => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string? PhysicalPath => throw new NotImplementedException();

        public Stream CreateReadStream()
        {
            throw new NotImplementedException();
        }
    }
}