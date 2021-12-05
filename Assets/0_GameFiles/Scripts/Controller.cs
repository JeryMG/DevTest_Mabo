using System;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Singleton

        private static Controller Instance;

        #endregion

    #region Members

    public static event Action<Vector2> OnTouchEnter;
    public static event Action<Vector2> OnTouchDrag;
    public static event Action<Vector2> OnTouchRelease;
    public static event Action<Vector2> OnStartPosChanged;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private float maxDistanceStartToDrag;
    [SerializeField] private float durationSwipe = 0.25f;

    private Vector2 startMousePos;
    private Vector2[] currentMousePos;

    //private int fingerID = -1;
    private Touch touchInput;

    private float currentDistanceStartToDrag;

    private bool isInputEnabled = false;
    private bool isInputStarted = false;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentMousePos = new Vector2[60];
        startMousePos = Vector2.zero;

        ClearMousePositions();
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += TouchOnStart;
        GameManager.OnGameEnd += GameManager_OnGameEnd;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= TouchOnStart;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && isInputEnabled)
        {
            StartTouch();
        }
        else if (Input.GetMouseButton(0) && isInputEnabled && isInputStarted)
        {
            DragTouch();
        }
        else if (Input.GetMouseButtonUp(0) && isInputEnabled && isInputStarted)
        {
            EndTouch();
        }
#else
    if (Input.touchCount > 0)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (fingerID != -1)
            {
                if (Input.touches[i].fingerId == fingerID && (Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary)  && isInputEnabled && isInputStarted)
                {
                    touchInput = Input.touches[i];
                    DragTouch();
                    return;
                }
                else if (Input.touches[i].fingerId == fingerID && Input.touches[i].phase == TouchPhase.Ended && isInputEnabled && isInputStarted)
                {
                    fingerID = -1;
                    EndTouch();
                    return;
                }
            }
            else if (Input.touches[i].phase == TouchPhase.Began && isInputEnabled)
            {
                fingerID = Input.touches[i].fingerId;
                touchInput = Input.touches[i];
                StartTouch();
                return;
            }
        }
    }
#endif
    }

    #region Actual Input (Private Methods)

    private void StartTouch()
    {
#if UNITY_EDITOR
        startMousePos.x = Input.mousePosition.x / mainCamera.scaledPixelWidth;
        startMousePos.y = Input.mousePosition.y / mainCamera.scaledPixelHeight;
#else
    startMousePos.x = touchInput.position.x / mainCamera.scaledPixelWidth;
    startMousePos.y = touchInput.position.y / mainCamera.scaledPixelHeight;
#endif
        SetUpStartMousePosition(startMousePos);

        isInputStarted = true;

        OnTouchEnter?.Invoke(startMousePos);
    }

    private void DragTouch()
    {
        SaveMousePositions();

#if UNITY_EDITOR
        currentMousePos[0].x = Input.mousePosition.x / mainCamera.scaledPixelWidth;
        currentMousePos[0].y = Input.mousePosition.y / mainCamera.scaledPixelHeight;
#else
    currentMousePos[0].x = touchInput.position.x / mainCamera.scaledPixelWidth;
    currentMousePos[0].y = touchInput.position.y / mainCamera.scaledPixelHeight;
#endif

        currentDistanceStartToDrag = Vector2.Distance(startMousePos, currentMousePos[0]) - maxDistanceStartToDrag;

        if (currentDistanceStartToDrag > 0)
        {
            startMousePos = Vector3.MoveTowards(startMousePos, currentMousePos[0], currentDistanceStartToDrag);
            OnStartPosChanged?.Invoke(startMousePos);
        }

        OnTouchDrag?.Invoke(currentMousePos[0]);
    }

    private void EndTouch()
    {
#if UNITY_EDITOR
        currentMousePos[0].x = Input.mousePosition.x / mainCamera.scaledPixelWidth;
        currentMousePos[0].y = Input.mousePosition.y / mainCamera.scaledPixelHeight;
#else
    currentMousePos[0].x = touchInput.position.x / mainCamera.scaledPixelWidth;
    currentMousePos[0].y = touchInput.position.y / mainCamera.scaledPixelHeight;
#endif

        isInputStarted = false;

        OnTouchRelease?.Invoke(currentMousePos[0]);
    }

    #endregion

    #region Private Methods
    
    private Vector2 GetMouseAmplitude()
    {
        // Current framerate
        float _framerate = 1f / Time.deltaTime;

        // How many occured frames since a defined time elapsed
        int _ref_frame = Mathf.Max(Mathf.RoundToInt(_framerate * durationSwipe), Application.targetFrameRate);

        return (currentMousePos[0] - currentMousePos[_ref_frame]);
    }
    
    private void SaveMousePositions()
    {
        for (int i = currentMousePos.Length - 1; i > 0; i--)
        {
            currentMousePos[i] = currentMousePos[i - 1];
        }
    }
    
    private void ClearMousePositions()
    {
        for (int i = 0; i < currentMousePos.Length; i++)
        {
            currentMousePos[i] = Vector2.zero;
        }
    }
    
    private void SetUpStartMousePosition(Vector2 _start_mouse_position)
    {
        for (int i = 0; i < currentMousePos.Length; i++)
        {
            currentMousePos[i] = _start_mouse_position;
        }
    }

    #endregion

    #region Public Methods

    public void TouchOnStart()
    {
        ToggleInput(true);

        return;

#if UNITY_EDITOR
        //StartTouch();
#else
    if (Input.touchCount > 0)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began || Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary)
            {
                fingerID = Input.touches[i].fingerId;
                touchInput = Input.touches[i];
                StartTouch();
                return;
            }
        }
    }
#endif
    }

    private void GameManager_OnGameEnd(bool obj)
    {
        ToggleInput(false);
    }

    public void ToggleInput(bool state)
    {
        //fingerID = -1;
        isInputEnabled = state;

        if (isInputStarted)
            EndTouch();
    }

    #endregion
}
