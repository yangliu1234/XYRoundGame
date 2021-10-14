using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatConfig : Editor
{
    [MenuItem("Assets/CreatDatas/CreatEntityConfig")]
    public static void CreatEntityConfig()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path.Contains("."))
        {
            int index = 1;

            Debug.Log("========"+ (index<<1 | 1 ));
            Debug.Log("no folder");
            return;
        }
        path = path + "/NewEntityConfig.asset";
        Debug.Log("============Path:" + path);
        EntityConfig config = ScriptableObject.CreateInstance<EntityConfig>();
        UnityEditor.AssetDatabase.CreateAsset(config, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }

    [MenuItem("Assets/CreatDatas/CreatTeamConfig")]
    public static void CreatTeamConfig()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path.Contains("."))
        {
            Debug.Log("no folder");
            return;
        }
        path = path + "/NewTeamConfig.asset";
        Debug.Log("============Path:" + path);
        TeamConfig config = ScriptableObject.CreateInstance<TeamConfig>();
        UnityEditor.AssetDatabase.CreateAsset(config, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }

    [MenuItem("Assets/CreatDatas/CreatBattlefieldConfig")]
    public static void CreatBattlefieldConfig()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path.Contains("."))
        {
            Debug.Log("no folder");
            return;
        }
        path = path + "/NewBattlefieldConfig.asset";
        Debug.Log("============Path:" + path);
        BattlefieldConfig config = ScriptableObject.CreateInstance<BattlefieldConfig>();
        UnityEditor.AssetDatabase.CreateAsset(config, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }

    [MenuItem("Assets/CreatDatas/CreatSkillConfig")]
    public static void CreatSkillConfig()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path.Contains("."))
        {
            Debug.Log("no folder");
            return;
        }
        path = path + "/NewSkillConfig.asset";
        Debug.Log("============Path:" + path);
        SkillConfig config = ScriptableObject.CreateInstance<SkillConfig>();
        UnityEditor.AssetDatabase.CreateAsset(config, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }
}
