using System.Security.Cryptography;
using System.Text;

namespace Blackwood.Services.Helpers;

public static class HashHelper
{
    public static string ComputeHash(string source)
    {
        using var hashAlgorithm = MD5.Create();
        var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(source));
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    public static bool VerifyHash(string source, string hash) => hash.Equals(ComputeHash(source));
}