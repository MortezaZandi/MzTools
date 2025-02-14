using System;
using System.IO;
using System.Threading.Tasks;

public class FileManager
{
    private const string DefaultExtension = ".sql";
    private const string FileFilter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";

    public static async Task<string> ReadFileAsync(string path)
    {
        try
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
        catch (Exception ex)
        {
            throw new FileManagerException($"Error reading file: {path}", ex);
        }
    }

    public static void WriteFile(string path, string content)
    {
        try
        {
            // Use synchronous WriteAllText instead of WriteAllTextAsync
            File.WriteAllText(path, content);
        }
        catch (Exception ex)
        {
            throw new FileManagerException($"Error writing file: {path}", ex);
        }
    }

    public static string GetFileNameWithExtension(string path)
    {
        if (string.IsNullOrEmpty(Path.GetExtension(path)))
        {
            return path + DefaultExtension;
        }
        return path;
    }

    public static string GetFileFilter()
    {
        return FileFilter;
    }
}

public class FileManagerException : Exception
{
    public FileManagerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}