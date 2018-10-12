using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
/// <summary>
/// 测试从服务器下载ab和lua
/// </summary>
[Hotfix]
public class HotfixScript : MonoBehaviour {

    private LuaEnv luaEnv;
    public static Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();


    void Start()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(CustomLoader);
        luaEnv.DoString("require 'fixed'");

    }


    public byte[] CustomLoader(ref string filepath)
    {
        string path = Application.streamingAssetsPath + "/lua/" + filepath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
    }


    [LuaCallCSharp]
    public void LoadResource(string resName, string filePath)
    {
        StartCoroutine(LoadResourceCorotine(resName, filePath));
    }






    IEnumerator LoadResourceCorotine(string resName, string filePath)
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(@"http://localhost/AssetBundles/" + filePath);
        yield return request.SendWebRequest();
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        GameObject gameObject = ab.LoadAsset<GameObject>(resName);
        prefabDict.Add(resName, gameObject);
    }

    public  GameObject GetOBJ(string name)
    {
        if (prefabDict.ContainsKey(name))
            return prefabDict[name];
        return null;
    }

    private void OnDisable()
    {
        luaEnv.DoString("require 'fixedDispose'");
    }



    private void OnDestroy()
    {
        luaEnv.Dispose();
    }


}
