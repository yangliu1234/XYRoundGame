using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum PrewarState
{
    NONE,
    START,
    CONTROLLER,
    END,
}
public class Battlefield : MonoBehaviour
{
    public Team team;
    public BattlefieldConfig battlefieldConfig;
    public BattleCreatManager battleCreatManager;
    public Modification modification;

    private PrewarState state = PrewarState.NONE;
    private PrewarState lastState = PrewarState.NONE;
    private int psTime;
    private int nowPSTime;
    private float timeDel;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (battlefieldConfig == null || team == null || battleCreatManager == null )return;
        psTime = battlefieldConfig.time;
        List<Entity> heros = battleCreatManager.CreatEntitys(true, battlefieldConfig.teamConfig.entityConfigA);
        List<Entity> enemys = battleCreatManager.CreatEntitys(false, battlefieldConfig.teamConfig.entityConfigB);
        team.Initialize(heros, enemys);
        team.nextRound += NextRoundEvent;
        BattleEvent.readyAction += ReadActionEvent;
    }

    private void ReadActionEvent(bool obj)
    {
        if(obj)
            state = PrewarState.END;
    }

    private void NextRoundEvent()
    {
        state = PrewarState.START;

    }
    private void StarRound()
    {
        nowPSTime = 0;
        state = PrewarState.NONE;
        team.StarRound(modification.skillPool, modification.enemyPool);
    }
    private void RefreshPrewarTime()
    {
        if (BattleEvent.prewarAction != null)
            BattleEvent.prewarAction(nowPSTime);
        if(nowPSTime < 1)
            state = PrewarState.END;
    }
    private void Update()
    {
        UpdateTime();
        if (lastState == state) return;
        switch (state)
        {
            case PrewarState.NONE:
                break;
            case PrewarState.START:
                SetPrewarTime();
                break;
            case PrewarState.CONTROLLER:
                break;
            case PrewarState.END:
                StarRound();
                break;
        }
        lastState = state;
    }

    private void SetPrewarTime()
    {
        nowPSTime = psTime;
        timeDel = Time.time;
        state = PrewarState.CONTROLLER;
    }

    private void UpdateTime()
    {
        if (nowPSTime <= 0) return;
        if ((Time.time - timeDel) >= 1)
        {
            timeDel = Time.time;
            nowPSTime = nowPSTime - 1;
            RefreshPrewarTime();
        }

    }

    private void OnDestroy()
    {
        battleCreatManager.DestoryAll();
        team.nextRound -= NextRoundEvent;
        BattleEvent.readyAction -= ReadActionEvent;
    }
}
