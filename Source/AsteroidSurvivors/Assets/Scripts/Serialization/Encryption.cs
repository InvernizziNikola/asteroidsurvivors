using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;


class Encryption
{
    public static void WriteObjectToStream(Stream outputStream, Object obj)
    {
        if (object.ReferenceEquals(null, obj))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(outputStream, obj);
    }

    public static object ReadObjectFromStream(Stream inputStream)
    {
        BinaryFormatter binForm = new BinaryFormatter();
        object obj = binForm.Deserialize(inputStream);
        return obj;
    }

    private const string cryptoKey = "Q3JpcHRvZ3J6Zmlh3yBjb20gUmluamRhZWwgLyBBRVM=";
    public static string CryptoKey
    {
        get { return cryptoKey; }
    }
    private const int keySize = 256;
    private const int ivSize = 16; // block size is 128-bit

    public static CryptoStream CreateEncryptionStream(byte[] key, Stream outputStream)
    {
        byte[] iv = new byte[ivSize];

        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        {
            // Using a cryptographic random number generator
            rng.GetNonZeroBytes(iv);
        }

        // Write IV to the start of the stream
        outputStream.Write(iv, 0, iv.Length);

        Rijndael rijndael = new RijndaelManaged();
        rijndael.KeySize = keySize;

        CryptoStream encryptor = new CryptoStream(
            outputStream,
            rijndael.CreateEncryptor(key, iv),
            CryptoStreamMode.Write);
        return encryptor;
    }

    public static CryptoStream CreateDecryptionStream(byte[] key, Stream inputStream)
    {
        byte[] iv = new byte[ivSize];

        if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
        {
            throw new ApplicationException("Failed to read IV from stream.");
        }

        Rijndael rijndael = new RijndaelManaged();
        rijndael.KeySize = keySize;

        CryptoStream decryptor = new CryptoStream(
            inputStream,
            rijndael.CreateDecryptor(key, iv),
            CryptoStreamMode.Read);
        return decryptor;
    }
}
