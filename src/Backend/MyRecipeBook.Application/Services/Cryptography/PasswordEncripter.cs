using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;
public class PasswordEncripter
{
    private readonly string additionalkey;
    public PasswordEncripter(string additionalkey)
    {
        this.additionalkey = additionalkey;
    }
    public string Encrypt(string password)
    {
        var newPassword = $"{password}{additionalkey}";

        var bytes = Encoding.UTF8.GetBytes(password);

        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        StringBuilder sb = new();

        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");

            sb.Append(hex);
        }

        return sb.ToString();
    }
}
