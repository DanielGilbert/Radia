using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Radia.Services.FileProviders
{
    public class CompositeRadiaDirectoryContents : IRadiaDirectoryContents
    {
        private readonly IList<IRadiaFileProvider> fileProviders;
        private readonly string subpath;
        private List<IFileInfo>? files;
        private bool exists;
        private List<IDirectoryContents>? directories;

        /// <summary>
        /// True if any given providers exists
        /// </summary>
        public bool Exists
        {
            get
            {
                EnsureDirectoriesAreInitialized();
                return exists;
            }
        }

        public CompositeRadiaDirectoryContents(IList<IRadiaFileProvider> fileProviders, string subpath)
        {
            ArgumentNullException.ThrowIfNull(fileProviders);
            this.fileProviders = fileProviders;
            this.subpath = subpath;
        }

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            EnsureFilesAreInitialized();
            return this.files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            EnsureFilesAreInitialized();
            return this.files.GetEnumerator();
        }

        [MemberNotNull(nameof(this.directories))]
        private void EnsureDirectoriesAreInitialized()
        {
            if (this.directories == null)
            {
                this.directories = new List<IDirectoryContents>();
                foreach (IFileProvider fileProvider in this.fileProviders)
                {
                    IDirectoryContents directoryContents = fileProvider.GetDirectoryContents(this.subpath);
                    if (directoryContents != null && directoryContents.Exists)
                    {
                        this.exists = true;
                        this.directories.Add(directoryContents);
                    }
                }
            }
        }

        [MemberNotNull(nameof(this.files))]
        [MemberNotNull(nameof(this.directories))]
        private void EnsureFilesAreInitialized()
        {
            EnsureDirectoriesAreInitialized();
            if (this.files == null)
            {
                this.files = new List<IFileInfo>();
                var names = new HashSet<string>();
                for (int i = 0; i < this.directories.Count; i++)
                {
                    IDirectoryContents directoryContents = this.directories[i];
                    foreach (IFileInfo file in directoryContents)
                    {
                        if (names.Add(file.Name))
                        {
                            this.files.Add(file);
                        }
                    }
                }
            }
        }
    }
}
