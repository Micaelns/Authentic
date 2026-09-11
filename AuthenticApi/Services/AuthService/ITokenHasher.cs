namespace AuthenticApi.Services.AuthService
{
    public interface ITokenHasher
    {
        string Hash(string token);
    }
}
