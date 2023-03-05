using System.Text;

namespace simple_rc4
{
  public static class RC4
  {
    /// <summary>
    /// 暗号化キーから256マスの変換テーブルSを作る
    /// </summary>
    /// <param name="key">暗号化キー</param>
    /// <returns>変換テーブルS</returns>
    private static byte[] KSA(byte[] key)
    {
      byte[] S = new byte[256];
      for (int i = 0; i < 256; i++)
      {
        S[i] = (byte)i;
      }

      int j = 0;
      for (int i = 0; i < 256; i++)
      {
        j = (j + S[i] + key[i % key.Length]) % 256;
        (S[j], S[i]) = (S[i], S[j]);
      }

      return S;
    }

    /// <summary>
    /// RC4暗号化アルゴリズムに基づいて、渡されたメッセージを暗号化または復号化します。
    /// メッセージはバイト配列として渡され、復号化したテキストは文字列として返されます。
    /// </summary>
    /// <param name="data">暗号化または復号化するメッセージのバイト配列</param>
    /// <param name="key">暗号化キーのバイト配列</param>
    /// <returns>復号化されたテキスト</returns>
    private static List<byte> PRGA(byte[] S)
    {
      List<byte> K = new();
      int i = 0;
      int j = 0;

      while (K.Count < 256)
      {
        i = (i + 1) % 256;
        j = (j + S[i]) % 256;
        (S[j], S[i]) = (S[i], S[j]);
        K.Add(S[(S[i] + S[j]) % 256]);
      }

      return K;
    }

    /// <summary>
    /// RC4暗号化を行う関数
    /// </summary>
    /// <param name="data">暗号化対象文字列</param>
    /// <param name="key">暗号化キー</param>
    /// <returns>BASE64エンコードされた暗号された文字列</returns>
    public static string Encrypt(string data, string key)
    {
      byte[] _data = Encoding.UTF8.GetBytes(data);
      byte[] _key = Encoding.UTF8.GetBytes(key);

      byte[] S = KSA(_key);
      byte[] gen = PRGA(S).ToArray();
      byte[] result = new byte[_data.Length];
      for (int i = 0; i < _data.Length; i++)
      {
        result[i] = (byte)(_data[i] ^ gen[i]);
      }
      return Convert.ToBase64String(result);
    }

    /// <summary>
    /// RC4暗号化されたデータを復号する関数
    /// </summary>
    /// <param name="data">BASE64エンコードされた暗号化されたデータ</param>
    /// <param name="key">暗号化キー</param>
    /// <returns>復元されたデータ</returns>
    public static string Decrypt(string data, string key)
    {
      byte[] _data = Convert.FromBase64String(data);
      byte[] _key = Encoding.UTF8.GetBytes(key);

      byte[] S = KSA(_key);
      byte[] gen = PRGA(S).ToArray();
      byte[] result = new byte[_data.Length];
      for (int i = 0; i < _data.Length; i++)
      {
        result[i] = (byte)(_data[i] ^ gen[i]);
      }
      return Encoding.UTF8.GetString(result);
    }
  }
}
