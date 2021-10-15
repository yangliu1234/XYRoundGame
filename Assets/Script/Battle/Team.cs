using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Team : MonoBehaviour
{
    public delegate void NextRoundEvent();
    public event Action nextRound;

    public List<Entity> heros;
    public List<Entity> enemys;
    private List<Entity> allEntity;
    private Entity nowAttEntity;
    private int index;
    private Dictionary<int, Entity> dicAllEntitys;

    public void Initialize(List<Entity> hlist,List<Entity> elist)
    {
        heros = hlist;
        enemys = elist;
        allEntity = new List<Entity>();
        allEntity.AddRange(heros);
        allEntity.AddRange(enemys);
        dicAllEntitys = new Dictionary<int, Entity>();
        SetAllEntitysDic();
        UpdateEntityRank();
        AddEntityEvent();
    }

    private void AddEntityEvent()
    {
        foreach (Entity et in allEntity)
        {
            et.stateChange += OnEntityStateChange;
        }
    }

    private void OnEntityStateChange()
    {
        if (nowAttEntity ==null || (nowAttEntity.state == EntityState.ATTACK)) return;
        index = index + 1;
        if (index < allEntity.Count)
        {
            SeachAttEntity();
        }
        else 
        {
            RoundEnd();
        }
    }
    private void RoundEnd()
    {
        ClearModificationData();
        CheckoutResult();
    }

    private void SetAllEntitysDic()
    {
        foreach (Entity et in allEntity)
        {
            dicAllEntitys[et.id] = et;
        }
    }

    public void StarRound(List<SelectEntityData> sPool,List<SelectEntityData> ePool)
    {
        Debug.Log("Enter Team");
        SetSelectEntityData(sPool,ePool);
        UpdateEntityRank();
        index = 0;
        SeachAttEntity();
    }
    private void SeachAttEntity()
    {
        nowAttEntity = null;
        int count = allEntity.Count;
        for (int i = 0; i < count; i++)
        {
            if (allEntity[i].state != EntityState.DIE && i>=index)
            {
                index = i;
                nowAttEntity = allEntity[i];
                break;
            }
        }
        if (nowAttEntity == null)
        {
            RoundEnd();
            return;
        }

        Debug.Log("Name:" + nowAttEntity.name +"  =============Round================");
        Debug.Log("nowAttEntity state: " + nowAttEntity.state.ToString() + " lastState:" + nowAttEntity.lastState.ToString());
        nowAttEntity.state = EntityState.ATTACK;
    }

    private void SetSelectEntityData(List<SelectEntityData> sPool, List<SelectEntityData> ePool)
    {
        foreach (SelectEntityData sed in sPool)
        {
            dicAllEntitys[sed.selfEntity.id].skillID = sed.skillID;
        }
        foreach (SelectEntityData sed in ePool)
        {
            dicAllEntitys[sed.selfEntity.id].enemyID = sed.enemyID;
        }
    }

    private void UpdateEntityRank()
    {

        allEntity.Sort(AllEntityRank);
    }

    private int AllEntityRank(Entity x, Entity y)
    {
        return (x.attackSpeed > y.attackSpeed) ? -1 : 1;
    }

    private void ClearModificationData()
    {
        foreach (Entity et in allEntity)
        {
            if (et.state != EntityState.DIE)
            {
                et.skillID = 0;
                et.enemyID = 0;
            }
        }
        nowAttEntity = null;
        index = 0;
    }
    private void CheckoutResult()
    {
        int mark = 0;
        foreach (Entity et in heros)
        {
            if (et.state != EntityState.DIE)
                mark = 1;
        }
        mark = mark << 1;
        foreach (Entity et in enemys)
        {
            if (et.state != EntityState.DIE)
            {
                mark = mark | 1;
                break;
            }
        }

        if (mark == 0)
        {
            Debug.Log("平局");
        }
        else if (mark == 2)
        {
            Debug.Log("胜利");
        }
        else if (mark == 1)
        {
            Debug.Log("失败");
        }
        else if (mark == 3)
        {
            if (nextRound != null)
                nextRound();
        }
    }
}
