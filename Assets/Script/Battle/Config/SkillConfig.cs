using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum AttackType 
{ 
     LONGRANGE,
     SHORTRANGE,
}
public enum TargetSelectType
{
    HPMAX,
    HPLEAST,
}
public class SkillConfig : ScriptableObject
{
    public int id;
    public string name;
    public AttackType attType;
    public TargetSelectType targetType;
    public int hurt;
}
