using simple_rc4;
using System.Text;

namespace Tests
{
  public static partial class UnitTest
  {
    [Fact]
    public static void TestEncryptDecrypt()
    {
      var random = new Random();
      const int iterations = 100;

      for (int i = 0; i < iterations; i++)
      {
        var messageBytes = Enumerable.Range(0, 16).Select(_ => (byte)random.Next(256)).ToArray();
        var keyBytes = Enumerable.Range(0, 16).Select(_ => (byte)random.Next(256)).ToArray();

        var message = Encoding.UTF8.GetString(messageBytes);
        var key = Encoding.UTF8.GetString(keyBytes);

        var ciphertext = RC4.Encrypt(message, key);
        var decryptedMessage = RC4.Decrypt(ciphertext, key);

        Assert.Equal(message, decryptedMessage);
      }
    }
  }
}
