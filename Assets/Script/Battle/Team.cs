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

    private List<Entity> heros;
    private List<Entity> enemys;
    private List<Entity> allEntity;
    private Entity nowAttEntity;
    private int index;
    private Entity nowEntity;
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
        if (nowAttEntity.state == EntityState.ATTACK) return;
        index = index + 1;
        if (index < allEntity.Count)
        {
            SeachAttEntity();
        }
        else 
        {
            ClearModificationData();
            CheckoutResult();
        }
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
        SetSelectEntityData(sPool,ePool);
        UpdateEntityRank();
        index = 0;
        SeachAttEntity();
    }
    private void SeachAttEntity()
    {
        int count = allEntity.Count;
        for (int i = 0; i < count; i++)
        {
            if (allEntity[i].state != EntityState.DIE && i>=index)
            {
                index = i;
                nowAttEntity = allEntity[i];
                return;
            }
        }
        nowAttEntity.Attack();
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
        return (x.attackSpeed > y.attackSpeed) ? 1 : -1;
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
                return;
            }
        }
        if (mark == 0)
        {
            Debug.Log("平局");
        }
        else if (mark == 1)
        {
            Debug.Log("胜利");
        }
        else if (mark == 2)
        {
            Debug.Log("失败");
        }
        else if (mark == 3)
        {
            Debug.Log("Next Round");
            if (nextRound != null)
                nextRound();
        }
    }
}
