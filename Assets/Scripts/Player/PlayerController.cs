using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public Vector3 Speed;
    public float SpeedForce = 100;
    public float JumpForce;
    public float Points;
    public float PointBaseValue;
    public float PointMultiplier;

    [HideInInspector]
    public bool CanBeChange;
    [HideInInspector]
    private float timeToDamage;
    [ReadOnly]
    public int PlayerPoints=1;
    [ReadOnly]
    public float BestCountValue=0;



    private Rigidbody _rigidbody;
    private bool isGrounded = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PlayerPoints = 1;
    }


    void Update()
    {
        if (GameManager.Instance.GameType != GameTypes.Play)
        {
            return;
        }

        Points += PointBaseValue * PointMultiplier*Time.deltaTime;
        PointMultiplier += 0.05f * Time.deltaTime;
        PointMultiplier = Mathf.Clamp(PointMultiplier, 1, 10);

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
        if (timeToDamage >= 1.5f)
        {
            CanBeChange = true;
        }

        if (PlayerPoints <= 0)
        {
            if (Points > BestCountValue)
            {
                BestCountValue = Points;
            }
            GameManager.Instance.FinishGame();
        }
    }

    public void FobidToDamage()
    {
        CanBeChange = false;
        timeToDamage = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coins")
        {
            GameManager.Instance.IncreaseCoins();
            Destroy(other.gameObject);
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
