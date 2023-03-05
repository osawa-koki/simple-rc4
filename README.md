# simple-rc4

🐵🐵🐵 C#でRC4暗号アルゴリズムを実装したライブラリです。  
簡単のため、UTF-8で符号化された文字列データのみを扱っています。  

## 実行方法

```shell
dotnet build -o ./bin ./src/Program.csproj
dotnet ./bin/Program.dll --key <暗号化キー> --message <暗号化対象文字列>
```

---

出力結果。  

```shell
---------- ---------- ----------
key: kagi
message: hello-world
ciphertext: o2XEYmSVKbQgsu4=
message: hello-world
---------- ---------- ----------
```
