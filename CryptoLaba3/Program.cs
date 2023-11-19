using System.Security.Cryptography;

byte[] masterKey = GenerateRandomBytes(32);

byte[] sessionKey = GenerateSessionKey(masterKey);

Console.WriteLine("Master Key: " + Convert.ToBase64String(masterKey));
Console.WriteLine("Session Key: " + Convert.ToBase64String(sessionKey));

byte[] GenerateRandomBytes(int length)
{
    using var rng = new RNGCryptoServiceProvider();
    var randomBytes = new byte[length];
    rng.GetBytes(randomBytes);

    return randomBytes;
}

byte[] GenerateSessionKey(byte[] masterKey)
{
    using Aes aes = Aes.Create();
    aes.Key = masterKey;
    aes.GenerateIV();

    using ICryptoTransform encryptor = aes.CreateEncryptor();
    byte[] encryptedSessionKey = encryptor.TransformFinalBlock(masterKey, 0, masterKey.Length);

    return encryptedSessionKey;
}