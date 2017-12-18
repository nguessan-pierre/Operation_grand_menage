using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArea : MonoBehaviour
{
    public Areas Area;

    public void SetArea(Areas area)
    {
        if (area != this.Area)
        {
            Area = area;
            Area.numberOfZombies++;
        }
    }

    public void Kill()
    {
        if (Area != null)
        {
            Area.ZombieKilled();
        }
    }
}
