using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum AttackType 
{ 
     longRange,
     shortRange,
}
public enum TargetSelectType
{
    hpMax,
    hpLeast,
}
public class SkillConfig : ScriptableObject
{
    public int id;
    public string name;
    public AttackType attType;
    public TargetSelectType targetType;
    public int hurt;
    public int attTime;
}
