namespace Image.Api.Dto.Common
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<string>();
        }

        public ICollection<string> Errors { get; set; }
    }
}
