using UnityEngine;
using UnityEditor;
using System.IO;

public static class TextureFileLogic
{
    public static bool GetTextureAlphaCoverage(Texture2D tex2D, out float alphaCoveragePercentage)
    {
        alphaCoveragePercentage = 0f;
        bool isalphatexture = false;

        if (!tex2D.isReadable)
            MakeTextureReadable(tex2D);

        if (tex2D != null)
        {
            if (tex2D.format == TextureFormat.Alpha8 ||
                tex2D.format == TextureFormat.ARGB32 ||
                tex2D.format == TextureFormat.RGBA32 ||
                tex2D.format == TextureFormat.DXT5 ||
                tex2D.format == TextureFormat.PVRTC_RGBA4 ||
                tex2D.format == TextureFormat.BGRA32)
            {
                float totalPixels = tex2D.width * tex2D.height;
                float transparentPixels = 0;

                Color32[] pixels = tex2D.GetPixels32();

                foreach (Color32 pixel in pixels)
                    if (pixel.a == 0)
                        transparentPixels++;

                alphaCoveragePercentage = (transparentPixels / totalPixels) * 100f;
                if (transparentPixels != 0)
                    isalphatexture = true;
                else
                    isalphatexture = false;
            }
            else
                isalphatexture = false;
        }
        return isalphatexture;
    }

    private static void MakeTextureReadable(Texture2D tex2D)
    {
        string assetPath = AssetDatabase.GetAssetPath(tex2D);
        TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (textureImporter != null)
        {
            textureImporter.isReadable = true;
            AssetDatabase.ImportAsset(assetPath);
        }
        else
            Debug.LogWarning("Could not access texture importer for " + tex2D.name + " at " + assetPath);
    }

    public static long GetTextureSize(Texture2D texture)
    {
        if (texture != null)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
                return GetFileSize(path);
        }

        return 0;
    }

    static long GetFileSize(string filePath, bool useKB = true)
    {
        if (!File.Exists(filePath))
            return 0;
        long size = new FileInfo(filePath).Length;
        return useKB ? size / 1024 : size;
    }
}
