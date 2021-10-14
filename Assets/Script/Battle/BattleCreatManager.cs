using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BattleCreatManager : MonoBehaviour
{
    public GameObject entityPrefab;
    public GameObject parentPrefab;
    public SkillConfig[] skillConfigList;
    private List<GameObject> allEntity;

    private static List<SkillConfig> skillConfigs;
    private void Awake()
    {
        BattleCreatManager.skillConfigs = new List<SkillConfig>();
        foreach (SkillConfig sc in skillConfigList)
            BattleCreatManager.skillConfigs.Add(sc);
    }
    public List<Entity> CreatEntitys(bool isHero , List<EntityConfig> list)
    {
        return new List<Entity>();
    }

    public void DestoryAll()
    { 
    }

    public static SkillConfig GetSkillConfig(int id)
    {
        SkillConfig sconfig = null;
        foreach (SkillConfig sc in BattleCreatManager.skillConfigs)
        {
            if (sc.id == id)
                sconfig = sc;
        }
        return sconfig;
    }
}
