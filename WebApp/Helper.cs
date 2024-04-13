using System.Security.Cryptography;
using System.Text;

namespace WebApp;
public static class Helper{
    public static byte[] Encrypt(string key, string iv, string plantext){
        using Aes aes = Aes.Create();
        aes.Key = Hash(key);
        aes.IV = Hash(iv);
        return aes.EncryptCbc(Encoding.UTF8.GetBytes(plantext), aes.IV);
    }
    public static string Descrypt(string key, string iv, byte[] cipher){
        using Aes aes = Aes.Create();
        aes.Key = Hash(key);
        aes.IV = Hash(iv);
        byte[] bytes = aes.DecryptCbc(cipher, aes.IV);
        return Encoding.UTF8.GetString(bytes);
    }
    public static byte[] Hash(string plantext){
        using HashAlgorithm hash = MD5.Create();
        return hash.ComputeHash(Encoding.ASCII.GetBytes(plantext));
    }
}