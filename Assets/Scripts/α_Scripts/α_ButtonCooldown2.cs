using UnityEngine;

public class α_ButtonCooldown2 : MonoBehaviour
{
    private static ButtonCooldownManager instance;
    private static bool cooldownActive = false;

    public static ButtonCooldownManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ButtonCooldownManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ButtonCooldownManager");
                    instance = obj.AddComponent<ButtonCooldownManager>();
                }
            }
            return instance;
        }
    }

    public bool CooldownActive
    {
        get { return cooldownActive; }
    }

    public void StartCooldown(float cooldownDuration)
    {
        cooldownActive = true;
        Invoke("FinishCooldown", cooldownDuration);
    }

    private void FinishCooldown()
    {
        cooldownActive = false;
    }
}