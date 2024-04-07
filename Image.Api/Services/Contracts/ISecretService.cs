namespace Image.Api.Services.Contracts
{
    public interface ISecretService
    {
        Task<string> GetSecret(string secretName); 
    }
}
