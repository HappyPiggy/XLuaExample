using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class CreatePrefabs : MonoBehaviour {

    private HotfixScript hotfixScript;

	void Start () {
        hotfixScript =GetComponent<HotfixScript>();
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(hotfixScript.GetOBJ("cube1"));
        }
	}
}
