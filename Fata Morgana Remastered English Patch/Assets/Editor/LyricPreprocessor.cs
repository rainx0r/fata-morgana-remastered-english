using UnityEditor;
using UnityEngine;

public class SpriteImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter importer = assetImporter as TextureImporter;
        if (importer != null && importer.assetPath.Contains("data/lyrics_en/en"))
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single; // Use Multiple if spritesheets
        }
    }
}