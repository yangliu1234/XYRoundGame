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
    public Entity[] heros;
    public Entity[] enemys;
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
        List<Entity> arry = new List<Entity>();
        Entity ety = null;
        int index = 0;
        foreach (EntityConfig ec in list)
        {
            if (isHero)
            {
                //ety = GameObject.Instantiate(entityPrefab).AddComponent<Hero>();
                ety = heros[index];
            }
            else
            {
                ety = enemys[index];
                //ety = GameObject.Instantiate(entityPrefab).AddComponent<Enemy>();
            }
            ety.gameObject.SetActive(true);
            //ety.gameObject.transform.parent = parentPrefab.transform;
            ety.hp = ety.hpMax = ec.hp;
            ety.name = ec.name;
            ety.id = ec.id;
            ety.attackSpeed = ec.speed;
            ety.attTime = ec.attTime;
            ety.skillIDs = ec.skillID.ToList();
            ety.state = EntityState.IDLE;
            arry.Add(ety);
            index++;
        }

        return arry;
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
