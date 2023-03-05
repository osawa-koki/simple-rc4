using Microsoft.Extensions.Configuration;

namespace simple_rc4
{
  public static class Program
  {
    public static int Main(string[] args)
    {
      var builder = new ConfigurationBuilder();
      builder.AddCommandLine(args);

      var config = builder.Build();

      // 鍵とメッセージを用意
      var message = config["message"];
      var key = config["key"];

      if (string.IsNullOrEmpty(message))
      {
        Console.WriteLine("message is empty.");
        return 1;
      }

      if (string.IsNullOrEmpty(key))
      {
        Console.WriteLine("key is empty.");
        return 1;
      }

      // 暗号化して、復号する
      Console.WriteLine("key: {0}", key);
      Console.WriteLine("message: {0}", message);

      string ciphertext = RC4.Encrypt(message, key);
      Console.WriteLine("ciphertext: {0}", ciphertext);

      string decrupted_message = RC4.Decrypt(ciphertext, key);
      Console.WriteLine("message2: {0}", decrupted_message);

      return 0;
    }
  }
}
