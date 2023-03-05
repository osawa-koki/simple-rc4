using System.Text;

namespace simple_rc4
{
  public static class RC4
  {
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

    public static byte[] Encrypt(byte[] data, byte[] key)
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

    public static byte[] Decrypt(byte[] data, byte[] key)
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
