using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Modification : MonoBehaviour
{
    public List<SelectEntityData> skillPool;
    public List<SelectEntityData> enemyPool;
    private void Awake()
    {
        skillPool = new List<SelectEntityData>();
        enemyPool = new List<SelectEntityData>();
        BattleEvent.updateEntityEnemy += UpdateEntityEnemyEvent;
        BattleEvent.updateEntitySkill += UpdateEntitySkillEvent;
    }

    private void UpdateEntitySkillEvent(Entity arg1, int arg2)
    {
        SelectEntityData data = CheckoutSkill(arg1);
        if (data == null)
        {
            data = new SelectEntityData();
            data.selfEntity = arg1;
            skillPool.Add(data);
        }
        data.skillID = arg2;
    }

    private void UpdateEntityEnemyEvent(Entity arg1, int arg2)
    {
        SelectEntityData data = CheckoutEnemy(arg1);
        if (data == null)
        {
            data = new SelectEntityData();
            data.selfEntity = arg1;
            enemyPool.Add(data);
        }
        data.enemyID = arg2;
    }

    private SelectEntityData CheckoutEnemy(Entity entity)
    {
        foreach (SelectEntityData data in enemyPool)
        {
            if (data.selfEntity.id == entity.id)
                return data;
        }
        return null;
    }
    private SelectEntityData CheckoutSkill(Entity entity)
    {
        foreach (SelectEntityData data in skillPool)
        {
            if (data.selfEntity.id == entity.id)
                return data;
        }
        return null;
    }

    public void Destroy()
    {
        BattleEvent.updateEntityEnemy -= UpdateEntityEnemyEvent;
        BattleEvent.updateEntitySkill -= UpdateEntitySkillEvent;
    }

}


public class SelectEntityData
{
    public Entity selfEntity;
    public int skillID;
    public int enemyID;
}
