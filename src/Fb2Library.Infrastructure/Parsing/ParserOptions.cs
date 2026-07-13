namespace Fb2Library.Infrastructure.Parsing
{
    public class ParserOptions
    {
        public long MaxFileSize { get; set; } = 50 * 1024 * 1024; // 50 MB
        public bool ExtractCoverImage { get; set; } = true;
        public int MaxAnnotationLength { get; set; } = 1000;
    }
}
