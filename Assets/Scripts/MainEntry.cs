using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
/// <summary>
/// 从服务器下载lua
/// </summary>
public class MainEntry : MonoBehaviour {

    private static string luaPath;
    private static string fixedLuaName = "fixed";
    private static string disposedLuaName = "fixedDispose";

    private void Start()
    {
        luaPath = Application.streamingAssetsPath + "/lua/";
        StartCoroutine(DownLoadLua());
    }




    IEnumerator DownLoadLua()
    {
        UnityWebRequest request = UnityWebRequest.Get(@"http://localhost/"+fixedLuaName+".lua.txt");
        yield return request.SendWebRequest();
        string str = request.downloadHandler.text;
        File.WriteAllText(luaPath+fixedLuaName+".lua.txt", str);
        UnityWebRequest request1 = UnityWebRequest.Get(@"http://localhost/" + disposedLuaName + ".lua.txt");
        yield return request1.SendWebRequest();
        string str1 = request1.downloadHandler.text;
        File.WriteAllText(luaPath + disposedLuaName + ".lua.txt", str1);

        print("download lua success");
        gameObject.AddComponent<HotfixScript>();
        gameObject.AddComponent<CreatePrefabs>();
    }


}
