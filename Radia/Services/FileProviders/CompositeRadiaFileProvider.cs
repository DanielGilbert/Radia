﻿using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Composite;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Primitives;

namespace Radia.Services.FileProviders
{
    public class CompositeRadiaFileProvider : IRadiaFileProvider
    {
        private readonly IList<IRadiaFileProvider> radiaFileProviders;

        public CompositeRadiaFileProvider(IList<IRadiaFileProvider> radiaFileProviders)
        {
            ArgumentNullException.ThrowIfNull(radiaFileProviders);
            this.radiaFileProviders = radiaFileProviders;
        }

        public bool AllowListing => true;
        public IRadiaDirectoryContents GetDirectoryContents(string subpath)
        {
            var directoryContents = new CompositeRadiaDirectoryContents(this.radiaFileProviders, subpath);
            return directoryContents;
        }

        public IRadiaFileInfo GetFileInfo(string subpath)
        {
            foreach (IRadiaFileProvider fileProvider in this.radiaFileProviders)
            {
                IRadiaFileInfo fileInfo = fileProvider.GetFileInfo(subpath);
                if (fileInfo != null && fileInfo.Exists)
                {
                    return fileInfo;
                }
            }
            return new RadiaNotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            // Watch all file providers
            var changeTokens = new List<IChangeToken>();
            foreach (IRadiaFileProvider fileProvider in this.radiaFileProviders)
            {
                IChangeToken changeToken = fileProvider.Watch(filter);
                if (changeToken != null)
                {
                    changeTokens.Add(changeToken);
                }
            }

            // There is no change token with active change callbacks
            if (changeTokens.Count == 0)
            {
                return NullChangeToken.Singleton;
            }

            return new CompositeChangeToken(changeTokens);
        }
    }
}