using System.Text;

namespace simple_rc4
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      // 鍵とメッセージを用意
      byte[] key = Encoding.UTF8.GetBytes("this_is_a_key");
      byte[] message = Encoding.UTF8.GetBytes("this_is_a_message");

      // 暗号化して、復号する
      Console.WriteLine("key: {0} ({1} bits)", Encoding.UTF8.GetString(key), key.Length * 8);
      Console.WriteLine("message: {0}", Encoding.UTF8.GetString(message));

      byte[] ciphertext = RC4.Encrypt(message, key);
      Console.WriteLine("ciphertext: {0}", BitConverter.ToString(ciphertext).Replace("-", ""));

      byte[] message2 = RC4.Encrypt(ciphertext, key);
      Console.WriteLine("message2: {0}", Encoding.UTF8.GetString(message2));
    }
  }
}
