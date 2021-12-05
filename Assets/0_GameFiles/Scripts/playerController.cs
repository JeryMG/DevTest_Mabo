using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public static playerController Instance;
    
    public bool startLookAt;
    public CharacterController controller;

    private Action DoAction;
    public float currentSpeed;
    private Vector3 direction;
    [HideInInspector] public bool isMoving;
    [Header("Player Config")]
    [SerializeField] private float inputSensibility = 0.5f;
    [SerializeField] private float maxHorizontalValue = 0.8f;
    private bool isDefeated;
    private bool input;
    private Vector2 inputStartPos;
    private bool isArrived;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.OnGameEnd += GameManager_OnGameEnd;
        GameManager.OnGameStart += GameManager_OnGameStart;
        
        Controller.OnTouchEnter += Controller_OnTouchEnter;
        Controller.OnTouchDrag += Controller_OnTouchDrag;
        Controller.OnTouchRelease += Controller_OnTouchRelease;
        
        GameManager.Instance.StartGame();
        //SetModeVoid();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.EndGame(true);
            LevelManager.Instance.CreateCurrentLevel(true);
        }

        DoAction?.Invoke();
    }

    private GameObject GetClosestEnemy()
    {
        Vector3 position = transform.position;
        return GameObject.FindGameObjectsWithTag("Enemy")
                .OrderBy(o => (o.transform.position - position).sqrMagnitude)
                .FirstOrDefault();
        }

    private void SwitchTarget()
    {
        //transform.LookAt(GetClosestEnemy().transform);
        if (startLookAt)
        {
            Quaternion startRotation = transform.rotation;
            DOVirtual.Float(0, 1, 1f, _f =>
            {
                transform.rotation = Quaternion.Slerp(startRotation,
                    Quaternion.LookRotation(GetClosestEnemy().transform.position - transform.position), _f);
            }).OnComplete( ()=> startLookAt = false);
        }
    }
    
    private void GameManager_OnGameStart()
    {
        //Debug.Log("Lets gooooooooooo");
        SetModeMove();
        GetComponent<ShootingSystem>().isShooting = true;
    }

    private void GameManager_OnGameEnd(bool _success)
    {
        SetModeVoid();
        GetComponent<ShootingSystem>().isShooting = false;
    }
    
    public void SetModeMove()
    {
        isMoving = true;
        DoAction = DoActionMove;
    }

    public void SetModeVoid()
    {
        isMoving = false;
        DoAction = null;
    }

    private void DoActionMove()
    {
        Move();
    }

    private void Move()
    {
        direction.z = 1;
        direction.x = Mathf.Min(maxHorizontalValue, Mathf.Abs(direction.x)) * Mathf.Sign(direction.x);

        if (input)
        {
            
        }
        else
        {
            direction = Vector3.Lerp(direction, new Vector3(0f, direction.y, direction.z), Time.deltaTime * 10f);
        }

        if (controller.enabled)
            controller.SimpleMove(new Vector3(direction.x, direction.y, direction.z * currentSpeed));
        
        //Debug.Log(direction.x);
        //transform.Translate(new Vector3(direction.x, direction.y, direction.z * currentSpeed));
    }
    
    #region ControllerCallbacks
    private void Controller_OnTouchEnter(Vector2 inputPos)
    {
        inputStartPos = inputPos;

        input = true;
        //isMoving = true;
    }

    private void Controller_OnTouchDrag(Vector2 inputPos)
    {
        direction.x = (inputPos - inputStartPos).x * inputSensibility;
        inputStartPos = inputPos;
    }

    private void Controller_OnTouchRelease(Vector2 inputPos)
    {
        input = false;
        //isMoving = false;
    }
    #endregion
    
    virtual protected void OnDestroy()
    {
        GameManager.OnGameEnd -= GameManager_OnGameEnd;
        GameManager.OnGameStart -= GameManager_OnGameStart;
        
        Controller.OnTouchEnter -= Controller_OnTouchEnter;
        Controller.OnTouchDrag -= Controller_OnTouchDrag;
        Controller.OnTouchRelease -= Controller_OnTouchRelease;
    }
}
