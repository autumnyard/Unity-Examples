
namespace AutumnYard.Editor
{
    using UnityEditor;

    public sealed class ExampleAssetPostprocessor : AssetPostprocessor
    {
        //public void OnPreprocessModel()
        //{
        //  ModelImporter modelImporter = (ModelImporter)assetImporter;

        //  bool changed = false;

        //  if(modelImporter.isReadable)
        //  {
        //    modelImporter.isReadable = false;
        //    changed = true;
        //  }

        //  if(!assetImporter.assetPath.Contains("Character") && !assetImporter.assetPath.Contains("Animal"))
        //  {
        //    if(modelImporter.animationType != ModelImporterAnimationType.None)
        //    {
        //      modelImporter.animationType = ModelImporterAnimationType.None;
        //      changed = true;
        //    }
        //  }

        //  if(!modelImporter.optimizeGameObjects)
        //  {
        //    modelImporter.optimizeGameObjects = true;
        //    changed = true;
        //  }

        //  if(changed) modelImporter.SaveAndReimport();
        //}

        //public void OnPreprocessTexture()
        //{
        //  TextureImporter textureImporter = (TextureImporter)assetImporter;

        //  bool changed = false;

        //  if(textureImporter.isReadable)
        //  {
        //    textureImporter.isReadable = false;
        //    changed = true;
        //  }

        //  if(textureImporter.maxTextureSize > 2048)
        //  {
        //    textureImporter.maxTextureSize = 2048;
        //    changed = true;
        //  }

        //  if(changed) textureImporter.SaveAndReimport();
        //}
    }
}
