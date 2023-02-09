using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BLL.Services;

public static class PwdService
{
    private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
    private const int Pbkdf2IterCount = 1000;
    private const int Pbkdf2SubkeyLength = 256 / 8;
    private const int SaltSize = 128 / 8;

    public static (string PasswordHash, string SecurityStamp) HashPasswordV2(string pwd)
    {
        var salt = new byte[SaltSize];
        new RNGCryptoServiceProvider().GetBytes(salt);

        return HashPasswordV2(pwd, salt);
    }

    private static (string PasswordHash, string SecurityStamp) HashPasswordV2(string pwd, byte[] salt)
    {
        var subKey = KeyDerivation.Pbkdf2(pwd, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
        var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
        outputBytes[0] = 0x00;
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subKey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);

        return (Convert.ToBase64String(outputBytes), new Guid(salt).ToString());
    }
}
