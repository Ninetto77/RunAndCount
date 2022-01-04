using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public struct PowerUp
    {
        public enum Type
        {
            MULTYPLIER,
            IMMORTALITY,
            COINS_SPAWN

        }
        public Type PowerUpType;
        public float Duration;
    }

    public PlayerController player;
    public delegate void OnCoinsPowerUp(bool active);
    public static event OnCoinsPowerUp CoinsPowerUpEvent;

    private PowerUp[] powerUps = new PowerUp[3];
    private Coroutine[] powerUpCors = new Coroutine[3];


    private void Start()
    {
        powerUps[0] = new PowerUp() { PowerUpType = PowerUp.Type.MULTYPLIER, Duration = 8 };
        powerUps[1] = new PowerUp() { PowerUpType = PowerUp.Type.IMMORTALITY, Duration = 5 };
        powerUps[2] = new PowerUp() { PowerUpType = PowerUp.Type.COINS_SPAWN
, Duration = 7 };
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PowerUpUse(PowerUp.Type.COINS_SPAWN);
        }
    }

    public void PowerUpUse(PowerUp.Type type)
    {
        PowerUpReset(type);
        powerUpCors[(int)type] = StartCoroutine(PowerUpCor(type));

        switch(type)
        {
            case PowerUp.Type.COINS_SPAWN
:
                if (CoinsPowerUpEvent != null)
                {
                    CoinsPowerUpEvent(true);
                }
                break;
            case PowerUp.Type.IMMORTALITY:
                player.ChangeImmortality(true);
                break;
            case PowerUp.Type.MULTYPLIER:
                GameManager.Instance.PowerUpMultiplier = 2;
                break;
        }
    }

    public void PowerUpReset(PowerUp.Type type)
    {
        if (powerUpCors[(int)type] != null)
        {
            StopCoroutine(powerUpCors[(int)type]);
        }
        else
        {
            return;
        }

        powerUpCors[(int)type] = null;
        switch (type)
        {
            case PowerUp.Type.COINS_SPAWN
:
                if (CoinsPowerUpEvent != null)
                {
                    CoinsPowerUpEvent(false);
                }
                break;
            case PowerUp.Type.IMMORTALITY:
                player.ChangeImmortality(false);
                break;
            case PowerUp.Type.MULTYPLIER:
                GameManager.Instance.PowerUpMultiplier = 1;
                break;

        }
    }

    public void ResetAllPowerUps()
    {
        for (int i=0; i<powerUps.Length;++i)
            PowerUpReset(powerUps[i].PowerUpType);
    }
        
    IEnumerator PowerUpCor(PowerUp.Type type)
    {
        float duration = powerUps[(int)type].Duration;

        while (duration >0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        PowerUpReset(type);
    }
}
