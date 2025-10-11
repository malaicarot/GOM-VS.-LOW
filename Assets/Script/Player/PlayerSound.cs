using UnityEngine;

public enum WeaponType
{
    Hammer,
    Axe,
    Scythe,
    Sword,
    Polearm,
    Dagger
}


public class PlayerSound : MonoBehaviour
{
    public void PlayWeaponSound(WeaponType type)
    {
        SoundManager.Instance.PlaySFX(type.ToString());
    }


    public void Footstep()
    {
        SoundManager.Instance.PlayerActionSound("Footstep", true);
    }

    public void JumpStart()
    {
        SoundManager.Instance.PlayerActionSound("JumpStart");
    }

    public void Grounded()
    {
        SoundManager.Instance.PlayerActionSound("Grounded");
    }
}
