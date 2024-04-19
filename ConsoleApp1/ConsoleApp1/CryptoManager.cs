using System.Security.Cryptography;
using System.Text;

class CryptoManager
{
    public byte[] GenerateKey()
    {
        byte[] key = new byte[32]; // 256 bits
        RandomNumberGenerator.Fill(key);
        return key;
    }

    public byte[] ComputeHmac(byte[] key, string message)
    {
        using (var hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        }
    }
}
