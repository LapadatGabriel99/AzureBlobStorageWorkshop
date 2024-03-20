namespace Image.Api.Models
{
    public class BlobFileResult
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public Stream FileStream { get; set; }
    }
}
