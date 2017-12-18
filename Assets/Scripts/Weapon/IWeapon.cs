using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IWeapon
{
    void Fire(float angle);

    float getTimeBetweenBullet();

    float getEffectsDisplayTime();

    void DisableEffects(bool t); // si vrai alors desactiver en utilisant un timer sinon desactiver en utilisant les transitions

    bool IsAmmoNoNeeded();

    string GetLayerName();

    int GetLayerNumber();

    float GetFiringTime();

    void SetCanHit(bool can);
}
