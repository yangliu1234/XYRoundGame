using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleEvent
{
    public static Action<Entity, int> updateEntityEnemy;
    public static Action<Entity, int> updateEntitySkill;
    public static Action<Entity> selectEntityAction;
    public static Action<int> prewarAction;
    public static Action<bool> readyAction;
}
