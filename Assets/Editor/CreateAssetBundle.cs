using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle : MonoBehaviour {

	[MenuItem("Assets/BuildAssetBundles")]
    private static void BuildAssetBundles()
    {
        string path = "AssetBundles";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
