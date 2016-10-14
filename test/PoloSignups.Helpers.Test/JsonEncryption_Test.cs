using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PoloSignups.Helpers.Test
{
    public class JsonEncryption_Test
    {
        class TestObject
        {
            public bool TestBool { get; set; }
            public string TestString { get; set; }
            public int TestInt { get; set; }
        }

        [Fact]
        public void Encrypt_Decrypt()
        {
            var testInstance = new TestObject { TestBool = true, TestString = "test123", TestInt = 537127 };
            var key = "some key text here";
            var jsonEncryption = new JsonEncryption(key);
            var encryptedData = jsonEncryption.SerializeAndEncrypt(testInstance);
            var encodedEncryptedData = UrlHelper.UrlEncodeBase64String(encryptedData);
            var decodedEncryptedData = UrlHelper.UrlDecodeBase64String(encodedEncryptedData);
            Assert.Equal(encryptedData, decodedEncryptedData);
            var decryptedInstance = jsonEncryption.DecryptAndDeserialize<TestObject>(decodedEncryptedData);
            Assert.Equal(true, decryptedInstance.TestBool);
            Assert.Equal("test123", decryptedInstance.TestString);
            Assert.Equal(537127, decryptedInstance.TestInt);
        }
    }
}
