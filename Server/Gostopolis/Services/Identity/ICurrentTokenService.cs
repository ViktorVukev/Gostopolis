namespace Gostopolis.Services.Identity;

public interface ICurrentTokenService
{
    string Get();

    void Set(string token);
}