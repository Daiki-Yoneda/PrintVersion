using System;
using System.Reflection;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // 現在のアセンブリ情報を取得
        Assembly assembly = Assembly.GetExecutingAssembly();

        // アセンブリのバージョンを取得
        Version version = assembly.GetName().Version;

        // バージョンを表示
        Console.WriteLine($"プロジェクトバージョン: {version}");

        // Gitのコミットハッシュを取得
        string commitHash = GetGitCommitHash();

        if (commitHash != null)
        {
            Console.WriteLine($"現在のGitコミットハッシュ: {commitHash}");
        }
        else
        {
            Console.WriteLine("Gitリポジトリが見つかりません。");
        }
    }

    static string GetGitCommitHash()
    {
        try
        {
            // プロセスを設定
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "rev-parse HEAD", // 最新のコミットハッシュを取得
                RedirectStandardOutput = true,  // 標準出力をリダイレクト
                UseShellExecute = false,  // シェル実行を無効にする
                CreateNoWindow = true  // 新しいウィンドウを作成しない
            };

            // プロセスを開始
            using (Process process = Process.Start(startInfo))
            {
                // 出力を読み取って結果を返す
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    string commitHash = reader.ReadToEnd().Trim();
                    return commitHash;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"エラー: {ex.Message}");
            return null;
        }
    }
}