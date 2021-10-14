using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TeamConfig : ScriptableObject
{
    public int id;
    public List<EntityConfig> entityConfigA;
    public List<EntityConfig> entityConfigB;
}
