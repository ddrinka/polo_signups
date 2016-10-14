using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoloSignups.Helpers
{
    public class JsonEncryption : IDisposable
    {
        readonly byte[] _salt = { 0x9b, 0xf2, 0x34, 0xfc, 0x19, 0x70, 0xe5, 0x93, 0xd2, 0x1e, 0x4f, 0xe0, 0xb5, 0x6f, 0x02, 0xdf };
        readonly Aes _aes;
        public JsonEncryption(string key)
        {
            int keySize = 256;
            int blockSize = 128;

            _aes = Aes.Create();
            _aes.KeySize = keySize;
            _aes.BlockSize = blockSize;
            _aes.Mode = CipherMode.CBC;
            using (var derivedKey = new Rfc2898DeriveBytes(key, _salt))
            {
                _aes.Key = derivedKey.GetBytes(keySize / 8);
                _aes.IV = derivedKey.GetBytes(blockSize / 8);
            }
        }

        public T DecryptAndDeserialize<T>(string encryptedJson)
        {
            var decryptedJson = Decrypt(encryptedJson);
            var instance = JsonConvert.DeserializeObject<T>(decryptedJson);
            return instance;
        }

        public string SerializeAndEncrypt(object instance)
        {
            var json = JsonConvert.SerializeObject(instance);
            var encryptedJson = Encrypt(json);
            return encryptedJson;
        }


        string Decrypt(string encryptedData)
        {
            var decodedBytes = Convert.FromBase64String(encryptedData);
            byte[] decryptedBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var decryptor = _aes.CreateDecryptor())
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(decodedBytes, 0, decodedBytes.Length);
                }
                decryptedBytes = memoryStream.ToArray();
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        string Encrypt(string data)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var encryptor = _aes.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(dataBytes, 0, dataBytes.Length);
                }
                encryptedBytes = memoryStream.ToArray();
            }
            var encodedBytes = Convert.ToBase64String(encryptedBytes);
            return encodedBytes;
        }

        public void Dispose()
        {
            _aes.Dispose();
        }
    }
}
