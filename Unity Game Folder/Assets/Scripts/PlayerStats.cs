using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public float damageMultiplier = 1f;
    public float atkSpdMultiplier = 1f;
    public float shootAngleRange = 45f; // Range of shooting angles in degrees
    public float bulletSpeedMultiplier = 1f;
    public int maxLevel = 10; // Maximum level of the player
    public int currentLevel = 1; // Current level of the player
    public int maxXP = 100; // Maximum XP required to reach the next level
    public int currentXP = 0; // Current XP of the player

    public UnityEvent OnLevelUp; // Event triggered when the player levels up

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= maxXP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            currentXP = 0;
            maxXP = CalculateNextLevelXP();

            // Trigger level up event
            OnLevelUp.Invoke();
        }
    }

    private int CalculateNextLevelXP()
    {
        // Example formula: Next level requires double the XP of the previous level
        return maxXP * 2;
    }

    public void AddAtkDmgMultiplier(float multiplier){ damageMultiplier *= multiplier; }
    public void AddAtkSpdMultiplier(float multiplier){ atkSpdMultiplier *= multiplier; }
    public void AddBulletSpdMultiplier(float multiplier) { bulletSpeedMultiplier *= multiplier; }

}
