using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public enum SkillState 
{ 
    FRONT,
    MIDDLE,
    LATER
}

public class SkillController : MonoBehaviour
{
    private Entity selfEntity;
    private List<Entity> enemys;
    private Entity target;
    private SkillConfig skillConfig;
    private int attTime = 0;
    private float timeDel = 0;

    public void CastSkill(Entity sEntity, int skillID, int enemyID)
    {
        selfEntity = sEntity;
        enemys = selfEntity.enemys;
        skillConfig = BattleCreatManager.GetSkillConfig(skillID);

        Debug.Log("Start Select Skill");
        Debug.Log("skill Name:"+skillConfig.name);

        SeachTarget(enemyID);
        timeDel = Time.time;
        attTime = selfEntity.attTime;
    }

    private void SeachTarget(int enemyID)
    {
        if (enemys == null)
        {
            Debug.Log("Enemy list == null");
            return;
        }
        Debug.Log("Start Select Enemy");
        Entity enemy = null;
        foreach (Entity et in enemys)
        {
            if (et.id == enemyID && et.state != EntityState.DIE)
                enemy = et;
        }
        if (enemy == null)
        {
            enemy = SeachEnemyToSkillConfig();
        }
        target = enemy;

        Debug.Log(" Enemy name:"+ target.name);
    }

    private Entity SeachEnemyToSkillConfig()
    {
        if (skillConfig == null)
        {
            Debug.Log("Skillconfig == null");
            return null;
        }
        Entity em = null;
        switch (skillConfig.targetType)
        {
            case TargetSelectType.HPLEAST:
                em = GetTargetHpLeast();
                break;
            case TargetSelectType.HPMAX:
                em = GetTargetHpMax();
                break;
        }
        return em;
    }

    private Entity GetTargetHpMax()
    {
        Entity en = null;
        foreach (Entity et in enemys)
        {
            if (en == null)
                en = et;
            else
            {
                if (et.hp > en.hp)
                    en = et;
            }
        }
        return en;
    }

    private Entity GetTargetHpLeast()
    {
        Entity en = null;
        foreach (Entity et in enemys)
        {
            if (en == null)
                en = et;
            else
            {
                if (et.hp < en.hp)
                    en = et;
            }
        }
        return en;
    }
    private void Update()
    {
        if (attTime < 1) return;
        if ((Time.time - timeDel) >= 1)
        {
            timeDel = Time.time;
            attTime = attTime - 1;
            if (attTime < 1)
            {
                attTime = 0;
                //TODO
                ChangeSelfEntityState();
            }
        }
    }

    private void ChangeSelfEntityState()
    {
        target.Damage(skillConfig.hurt);
        Debug.Log(String.Format("{0} use {1} attack {2} , Damage:{3}", selfEntity.name, skillConfig.name,target.name, skillConfig.hurt));
        selfEntity.state = EntityState.IDLE;
    }
}
