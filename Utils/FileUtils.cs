namespace TestMVCSharpC.Utils;

public static class FileUtils
{
    public static string DetectMimeType(byte[]? content)
    {
        if (content == null || content.Length == 0)
            return "application/octet-stream";

        // PNG
        if (content.Length >= 4 &&
            content[0] == 0x89 &&
            content[1] == 0x50 &&
            content[2] == 0x4E &&
            content[3] == 0x47)
        {
            return "image/png";
        }

        // JPEG
        if (content.Length >= 3 &&
            content[0] == 0xFF &&
            content[1] == 0xD8 &&
            content[2] == 0xFF)
        {
            return "image/jpeg";
        }

        // GIF
        if (content.Length >= 3 &&
            content[0] == 0x47 &&
            content[1] == 0x49 &&
            content[2] == 0x46)
        {
            return "image/gif";
        }

        // PDF
        if (content.Length >= 4 &&
            content[0] == 0x25 &&
            content[1] == 0x50 &&
            content[2] == 0x44 &&
            content[3] == 0x46)
        {
            return "application/pdf";
        }

        // BMP
        if (content.Length >= 2 &&
            content[0] == 0x42 &&
            content[1] == 0x4D)
        {
            return "image/bmp";
        }

        // WEBP
        if (content.Length >= 12 &&
            content[0] == 0x52 &&
            content[1] == 0x49 &&
            content[2] == 0x46 &&
            content[3] == 0x46 &&
            content[8] == 0x57 &&
            content[9] == 0x45 &&
            content[10] == 0x42 &&
            content[11] == 0x50)
        {
            return "image/webp";
        }

        return "application/octet-stream";
    }

    public static bool IsImage(byte[]? content)
    {
        return DetectMimeType(content).StartsWith("image/");
    }

    public static string BuildFileSource(byte[]? content)
    {
        if (content == null || content.Length == 0)
            return string.Empty;

        var mimeType = DetectMimeType(content);
        var base64 = Convert.ToBase64String(content);

        return $"data:{mimeType};base64,{base64}";
    }
}
