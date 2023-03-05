using simple_rc4;
using System.Text;

namespace Tests
{
  public static partial class UnitTest
  {
    private static readonly int iterations = 100;

    /// <summary>
    /// 同一の暗号化キーを用いれば復号に成功することを確認する
    /// </summary>
    [Fact(DisplayName = "同一の暗号化キーを用いれば復号に成功することを確認する")]
    public static void TestSameKeyEncryptDecrypt()
    {
      var random = new Random();

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

    /// <summary>
    /// 異なる暗号化キーでは復号に失敗することを確認する
    /// </summary>
    [Fact(DisplayName = "異なる暗号化キーでは復号に失敗することを確認する")]
    public static void TestDifferentKeyDecrypt()
    {
      var random = new Random();

      for (int i = 0; i < iterations; i++)
      {
        var messageBytes = Enumerable.Range(0, 16).Select(_ => (byte)random.Next(256)).ToArray();
        var keyBytes = Enumerable.Range(0, 16).Select(_ => (byte)random.Next(256)).ToArray();

        var message = Encoding.UTF8.GetString(messageBytes);
        var key = Encoding.UTF8.GetString(keyBytes);

        var ciphertext = RC4.Encrypt(message, key);

        var differentKeyBytes = Enumerable.Range(0, 16).Select(_ => (byte)random.Next(256)).ToArray();
        var differentKey = Encoding.UTF8.GetString(differentKeyBytes);

        var differentDecryptedMessage = RC4.Decrypt(ciphertext, differentKey);

        Assert.NotEqual(message, differentDecryptedMessage);
      }
    }
  }
}
