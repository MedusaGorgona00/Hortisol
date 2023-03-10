using System.Collections.Generic;

namespace DTO;

public static class SettingsDto
{
    public sealed record VirtualDir
    {
        public string BaseDir { get; init; }

        public string BaseSuffixUri { get; init; }
    }

    public sealed record Cors
    {
        public IEnumerable<string> Origins { get; init; }
    }

    public sealed record ServiceUri
    {
        public Self Self { get; init; }

        public PdfGenerate PdfGenerate { get; init; }
    }

    public sealed record Self
    {
        public string BaseUri { get; init; }

        public string BaseFrontUri { get; init; }
    }

    public sealed record PdfGenerate
    {
        public string BaseUri { get; init; }

        public string Secret { get; init; }
    }
}
