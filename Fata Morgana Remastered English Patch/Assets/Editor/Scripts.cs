using UnityEditor;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class EditorScripts
{
    const string ManifestPath = "Assets/Editor/bgimage.manifest";
    static readonly Regex ManifestFileRegex = new Regex(@"(Assets\/fata_unity\/AssetBundleResources\/data\/bgimage\/.+)", RegexOptions.Compiled);
    static readonly char[] FileNameTrim = { '"', '\n', '\r' };

    static string[] GetManifestFiles(string path)
    {
        using (var reader = new StreamReader(path))
        {
            var files = new List<string>();
            for (string line; (line = reader.ReadLine()) != null;)
            {
                foreach (Match match in ManifestFileRegex.Matches(line))
                {
                    files.Add(Regex.Unescape(match.Value.Trim(FileNameTrim)).Replace("bgimage", "bgimage_en"));
                }
            }
            return files.ToArray();
        }
    }

    [Serializable]
    public struct AssetBundleEntry
    {
        public string AssetsPath;
        public string BundlePath;
    }

    [Serializable]
    public class AssetBundleConfig
    {
        public string BundleName;
        public AssetBundleEntry[] Entries;
    }

    [MenuItem("Project/Merge Sprites into Assets")]
    static void MergeSprites()
    {
        // Get Filenames
        var files = GetManifestFiles(ManifestPath);
        // Sprite processing
        foreach(var file in files) {
            var extension = Path.GetExtension(file);
            if (extension == ".png" || extension == ".jpg") {
                var pngPath = Path.ChangeExtension(file, ".png").Replace("fata_unity/AssetBundleResources/data/bgimage_en", "PNGs");
                Debug.Log("PNG Path: " + pngPath);
                var assetPath = Path.ChangeExtension(file, ".asset");
                Debug.Log("Asset Path: " + assetPath);

                if (File.Exists(assetPath))
                {
                    var pngSprite = AssetDatabase.LoadAssetAtPath<Sprite>(pngPath);
                    var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

                    // Get importer settings in order to copy sprite settings exactly
                    var settings = new TextureImporterSettings();
                    var importer = (TextureImporter) AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(pngSprite));
                    importer.ReadTextureSettings(settings);

                    // Uncomment to enable cropping
                    // var rect = pngSprite.rect;
                    // rect = new Rect(
                    //     (rect.x     / pngSprite.texture.width)  * texture.width,
                    //     (rect.y     / pngSprite.texture.height) * texture.height,
                    //     (rect.width / pngSprite.texture.width)  * texture.width,
                    //     (rect.height / pngSprite.texture.height) * texture.height
                    // );
                    // rect.xMax = Mathf.Min(rect.xMax, texture.width);
                    // rect.yMax = Mathf.Min(rect.yMax, texture.height);
                    var rect = new Rect(0, 0, texture.width, texture.height);

                    /* Copy sprite data to asset */
                    var sprite = Sprite.Create(
                        texture,
                        rect,
                        settings.spritePivot,
                        settings.spritePixelsPerUnit,
                        settings.spriteExtrude,
                        settings.spriteMeshType,
                        settings.spriteBorder,
                        settings.spriteGenerateFallbackPhysicsShape
                    );

                    // Ensure name matches texture
                    sprite.name = texture.name;

                    // Add as child of texture
                    AssetDatabase.AddObjectToAsset(sprite, AssetDatabase.GetAssetPath(texture));
                }
            }
        }
    }

    [MenuItem("Project/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "../build/FataMorgana_Data/StreamingAssets/AssetBundles/Windows64";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        /* bgimage */
        var filenames = GetManifestFiles(ManifestPath);
        var bgimageAddressableNames = new List<string>();
        var bgimageAssetNames = new List<string>();
        for (int i = 0; i < filenames.Length; i++)
        {
            var filename = filenames[i];
            var assetPath = Path.ChangeExtension(filename, ".asset");
            var pngPath = Path.ChangeExtension(filename, ".png");
            if (File.Exists(assetPath))
            {
                bgimageAddressableNames.Add(filename);
                bgimageAssetNames.Add(assetPath);
            }
            else if (File.Exists(pngPath))
            {
                bgimageAddressableNames.Add(filename);
                bgimageAssetNames.Add(pngPath);
            }
        }

        var bgimage_en = new AssetBundleBuild {
            assetBundleName = "bgimage_en",
            assetNames = bgimageAssetNames.ToArray(),
            addressableNames = bgimageAddressableNames.ToArray()
        };

        /* patch */
        var PatchBundleConfig = JsonUtility.FromJson<AssetBundleConfig>(File.ReadAllText("Assets/Editor/patch.json"));
        var patchAddressableNames = new List<string>();
        var patchAssetNames = new List<string>();
        foreach (var entry in PatchBundleConfig.Entries)
        {
            patchAssetNames.Add(entry.AssetsPath);
            patchAddressableNames.Add(entry.BundlePath);
        }
        var patch = new AssetBundleBuild {
            assetBundleName = PatchBundleConfig.BundleName,
            assetNames = patchAssetNames.ToArray(),
            addressableNames = patchAddressableNames.ToArray()
        };

        /* lyrics_en */
        var LyricsEnBundleConfig = JsonUtility.FromJson<AssetBundleConfig>(File.ReadAllText("Assets/Editor/lyrics_en.json"));
        var lyricsAddressableNames = new List<string>();
        var lyricsAssetNames = new List<string>();
        foreach (var entry in LyricsEnBundleConfig.Entries)
        {
            lyricsAssetNames.Add(entry.AssetsPath);
            lyricsAddressableNames.Add(entry.BundlePath);
        }
        var lyrics_en = new AssetBundleBuild {
            assetBundleName = LyricsEnBundleConfig.BundleName,
            assetNames = lyricsAssetNames.ToArray(),
            addressableNames = lyricsAddressableNames.ToArray()
        };


        BuildPipeline.BuildAssetBundles(assetBundleDirectory, new[] { bgimage_en, lyrics_en, patch },
                                        BuildAssetBundleOptions.ChunkBasedCompression, 
                                        BuildTarget.StandaloneWindows64);
    }
}