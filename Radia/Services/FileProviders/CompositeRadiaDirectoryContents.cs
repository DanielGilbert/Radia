using Microsoft.Extensions.FileProviders;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Radia.Services.FileProviders
{
    public class CompositeRadiaDirectoryContents : IRadiaDirectoryContents
    {
        private readonly IList<IRadiaFileProvider> fileProviders;
        private readonly string subpath;
        private List<IRadiaFileInfo>? files;
        private bool exists;
        private List<IRadiaDirectoryContents>? directories;

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


        IEnumerator IEnumerable.GetEnumerator()
        {
            EnsureFilesAreInitialized();
            return this.files.GetEnumerator();
        }

        IEnumerator<IRadiaFileInfo> IEnumerable<IRadiaFileInfo>.GetEnumerator()
        {
            EnsureFilesAreInitialized();
            return this.files.GetEnumerator();
        }

        [MemberNotNull(nameof(this.directories))]
        private void EnsureDirectoriesAreInitialized()
        {
            if (this.directories == null)
            {
                this.directories = new List<IRadiaDirectoryContents>();
                foreach (IRadiaFileProvider fileProvider in this.fileProviders)
                {
                    IRadiaDirectoryContents directoryContents = fileProvider.GetDirectoryContents(this.subpath);
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
                this.files = new List<IRadiaFileInfo>();
                var names = new HashSet<string>();
                for (int i = 0; i < this.directories.Count; i++)
                {
                    IRadiaDirectoryContents directoryContents = this.directories[i];
                    foreach (IRadiaFileInfo file in directoryContents)
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
