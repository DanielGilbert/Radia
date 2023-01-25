﻿using Radia.Services.ContentProcessors;

namespace Radia.ViewModels
{
    public interface IPhysicalFileViewModel : IViewModel
    {
        IContentResult<string> ContentResult { get; }
        string? ContentType { get; }
        string? FileName { get; }
    }
}
