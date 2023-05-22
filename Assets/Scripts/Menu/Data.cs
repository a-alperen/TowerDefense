using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Data 
{
    public float money;
    public List<List<int>> upgradeLevels;
    

    public Data()
    {
        money = 0;

        upgradeLevels = new List<List<int>>
        {
            new int[4].ToList(),
            new int[4].ToList(),
            new int[4].ToList(),
        };
    }
}
