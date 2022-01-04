using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public Vector3 Speed;
    public float SpeedForce = 100;
    public float JumpForce;

    [HideInInspector]
    public bool CanBeChange;
    [HideInInspector]
    private float timeToDamage;
    [ReadOnly]
    public int PlayerPoints=1;
    [ReadOnly]
    public float BestCountValue=0;
    [ReadOnly]
    public bool IsImmortal = false;

    private PowerUpController _powerUpController;
    private Rigidbody _rigidbody;
    private bool isGrounded = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _powerUpController = GetComponent<PowerUpController>();
        PlayerPoints = 1;
        IsImmortal = false;
    }


    void Update()
    {
        if (GameManager.Instance.GameType != GameTypes.Play)
        {
            return;
        }



        //_rigidbody.velocity = new Vector3(Speed.x >= 0 ? Speed.x : _rigidbody.velocity.x,
        //                                 Speed.y >= 0 ? Speed.y : _rigidbody.velocity.y,
        //                                 Speed.z >= 0 ? Speed.z : _rigidbody.velocity.z);
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * SpeedForce * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * SpeedForce * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }

        timeToDamage += Time.deltaTime;
        if (timeToDamage >= .5f)
        {
            CanBeChange = true;
        }

        if (PlayerPoints <= 0)
        {
            if (GameManager.Instance.Points > BestCountValue)
            {
                BestCountValue = GameManager.Instance.Points;
            }
            _powerUpController.ResetAllPowerUps() ;
            GameManager.Instance.FinishGame();
        }
    }

    public void FobidToDamage()
    {
        CanBeChange = false;
        timeToDamage = 0;
    }


    public void ChangeImmortality(bool isImmortal)
    {
        IsImmortal = isImmortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coins":
                GameManager.Instance.IncreaseCoins();
                Destroy(other.gameObject);
                break;
            case "PUCoin":
                _powerUpController.PowerUpUse(PowerUpController.PowerUp.Type.COINS_SPAWN);
                Destroy(other.gameObject);
                break;
            case "PUPoints":
                _powerUpController.PowerUpUse(PowerUpController.PowerUp.Type.MULTYPLIER);
                Destroy(other.gameObject);
                break;
            case "PUImmortal":
                _powerUpController.PowerUpUse(PowerUpController.PowerUp.Type.IMMORTALITY); 
                Destroy(other.gameObject);
                break;
            default: 
                break;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
