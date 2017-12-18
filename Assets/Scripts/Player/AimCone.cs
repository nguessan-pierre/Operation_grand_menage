using System;
using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;

public class AimCone : MonoBehaviour
{
    public static double thetaMin = Math.PI / 16;
    public static double thetaMax = Math.PI / 6;
    public static double maxSpeed = 5d;
    public static float lineLength = 60f;
    
    public static float GetAngle(CharacterStatistics statistics, float playerSpeed)
    {
        return (float) (0.975 * statistics.Precision.GetValue()
                         * (thetaMin + (playerSpeed / maxSpeed) * thetaMax));
    }
}
