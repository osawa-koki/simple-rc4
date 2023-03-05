using System.Text;

namespace RC4
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

      byte[] ciphertext = RC4(message, key);
      Console.WriteLine("ciphertext: {0}", BitConverter.ToString(ciphertext).Replace("-", ""));

      byte[] message2 = RC4(ciphertext, key);
      Console.WriteLine("message2: {0}", Encoding.UTF8.GetString(message2));
    }

    public static byte[] KSA(byte[] key)
    {
      // key から256マスの変換テーブル S を作る
      byte[] S = new byte[256];
      for (int i = 0; i < 256; i++)
      {
        S[i] = (byte)i;
      }

      int j = 0;
      for (int i = 0; i < 256; i++)
      {
        j = (j + S[i] + key[i % key.Length]) % 256;
        byte temp = S[i];
        S[i] = S[j];
        S[j] = temp;
      }

      return S;
    }

    public static List<byte> PRGA(byte[] S)
    {
      // S を更新しながら1バイトずつ数字を吐き出すリストを返す
      List<byte> K = new List<byte>();
      int i = 0;
      int j = 0;

      while (K.Count < 256)
      {
        i = (i + 1) % 256;
        j = (j + S[i]) % 256;
        byte temp = S[i];
        S[i] = S[j];
        S[j] = temp;
        K.Add(S[(S[i] + S[j]) % 256]);
      }

      return K;
    }

    public static byte[] RC4(byte[] data, byte[] key)
    {
      // data がメッセージなら暗号化、暗号文なら復号化する
      byte[] S = KSA(key);
      byte[] gen = PRGA(S).ToArray();
      byte[] result = new byte[data.Length];
      for (int i = 0; i < data.Length; i++)
      {
        result[i] = (byte)(data[i] ^ gen[i]);
      }
      return result;
    }
  }
}
