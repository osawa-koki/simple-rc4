namespace simple_rc4
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      // 鍵とメッセージを用意
      string message = "Hello, World!";
      string key = "key";

      // 暗号化して、復号する
      Console.WriteLine("key: {0}", key);
      Console.WriteLine("message: {0}", message);

      string ciphertext = RC4.Encrypt(message, key);
      Console.WriteLine("ciphertext: {0}", ciphertext);

      string message2 = RC4.Decrypt(ciphertext, key);
      Console.WriteLine("message2: {0}", message2);
    }
  }
}
