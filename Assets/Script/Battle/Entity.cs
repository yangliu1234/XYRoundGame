using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum EntityState { 
    IDLE,
    ATTACK,
    DIE,
}

public class Entity:FSM<EntityState>
{
    public int id;
    public string name;
    public int hp;
    public int hpMax;
    public int attackSpeed;
    public int enemyID;
    public int skillID;
    public int attTime;
    public List<int> skillIDs;
    public List<Entity> enemys;
    public SkillController skill;
    private void Awake()
    {
        skill = this.gameObject.AddComponent<SkillController>();
        stateChange += StateChangAction;
    }

    private void StateChangAction()
    {
        switch (state) 
        {
            case EntityState.IDLE:
                Idle();
                break;
            case EntityState.ATTACK:
                Attack();
                break;
            case EntityState.DIE:
                Die();
                break;
        }
    }

    public void Attack()
    {
        int sID = GetSkillID();
        Debug.Log( "Cast skill Start");
        skill.CastSkill(this, sID, enemyID);
    }

    private int GetSkillID()
    {
        int sID = 0;
        foreach (int num in skillIDs)
        {
            if (num == skillID)
                sID = num;
        }
        if (sID == 0)
            sID = skillIDs[0];
        return sID;
    }

    public void Idle()
    {
        
    }
    public void Damage(int dmNum)
    {
        if (hp > dmNum)
            hp = hp - dmNum;
        else
        {
            hp = 0;
            state = EntityState.DIE;
        }
    }
    public void Die()
    {
        
    }
    public void OnSelectEntity()
    {
        if (BattleEvent.selectEntityAction != null)
            BattleEvent.selectEntityAction(this);
    }
}
