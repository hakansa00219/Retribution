using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    private readonly AxisLimits _orthographicLimits = new AxisLimits()
    {
        XPosLimits = new Vector2(-8.5f, 8.5f),
        YPosLimits = Vector2.zero,
        ZPosLimits = new Vector2(-5f, 4f),
        RotationLimits = new Vector2(-75f, 75f),
    };

    private readonly AxisLimits _sideLimits = new AxisLimits()
    {
        XPosLimits = Vector2.zero,
        YPosLimits = new Vector2(-1.5f, 5.3f),
        ZPosLimits = new Vector2(-5f, 7.5f),
        RotationLimits = new Vector2(-75f, 75f),
    };
    private Vector2 _inputMovement;
    
    private float _rotationSpeed;
    private float _currentYRotation;
    private float _currentXRotation;
    public bool IsDead { get; private set; }
    
    [SerializeField]
    private Deflect deflect;
    
    public Deflect Deflect => deflect;
    [SerializeField] private int health;

    private CamType _camType;
    private bool _isGameStarted = false;

    public event Action<int> HealthDropped;
    
    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        CameraAnimationPlayer.Instance.CameraChanged += OnCameraChanged;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Deflect.performed += OnDeflect;
        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        GameManager.GameStarted += OnGameStarted;
    }
    

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.Deflect.performed -= OnDeflect;
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        
        CameraAnimationPlayer.Instance.CameraChanged -= OnCameraChanged;
        
        GameManager.GameStarted -= OnGameStarted;
    }

    private void OnCameraChanged(CamType camType)
    {
        _camType = camType;
    }
    
    private void OnGameStarted()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isGameStarted = true;
    }
    
    private void Update()
    {
        if (!_isGameStarted) return;
        
        // Get the delta position from the input action
        Vector2 deltaPosition = _inputMovement;

        switch (_camType)
        {
            case CamType.Orthographic:
                // Calculate reversed X movement and normal Z movement
                float reversedX = deltaPosition.x * 2f * Time.deltaTime;
                float zMovement = deltaPosition.y * 2f * Time.deltaTime;
                
                // Apply movement on the X-Z plane
                Vector3 movement = new Vector3(reversedX, 0, zMovement);
              
                transform.Translate(movement, Space.World);

                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, _orthographicLimits.XPosLimits.x, _orthographicLimits.XPosLimits.y),
                    2f,
                    Mathf.Clamp(transform.position.z, _orthographicLimits.ZPosLimits.x, _orthographicLimits.ZPosLimits.y));

                float yRotation = -deltaPosition.x * 50f * Time.deltaTime;
                float zExtraRotation = Mathf.Abs(-deltaPosition.y) * 20f * Time.deltaTime;
        
                _currentYRotation += yRotation;
                switch (_currentYRotation)
                {
                    case > 0:
                        _currentYRotation -= zExtraRotation;
                        break;
                    case < 0:
                        _currentYRotation += zExtraRotation;
                        break;
                }
        
                _currentYRotation = Mathf.Clamp(_currentYRotation, _orthographicLimits.RotationLimits.x, _orthographicLimits.RotationLimits.y);
        
                transform.rotation = Quaternion.Euler(0, _currentYRotation, 0);
                break;
            case CamType.Side:
                // Calculate reversed X movement and normal Z movement
                float zMove = deltaPosition.x * 2f * Time.deltaTime;
                float yMove = deltaPosition.y * 2f * Time.deltaTime;
                
                // Apply movement on the X-Z plane
                Vector3 move = new Vector3(0f, yMove, zMove);
                
                transform.Translate(move, Space.World);
                
                transform.position = new Vector3(
                    0f,
                    Mathf.Clamp(transform.position.y, _sideLimits.YPosLimits.x, _sideLimits.YPosLimits.y),
                    Mathf.Clamp(transform.position.z, _sideLimits.ZPosLimits.x, _sideLimits.ZPosLimits.y));
                
                float xRotation = -deltaPosition.y * 50f * Time.deltaTime;
                float zExRotation = Mathf.Abs(-deltaPosition.x) * 20f * Time.deltaTime;
                
                _currentXRotation += xRotation;
                switch (_currentXRotation)
                {
                    case > 0:
                        _currentXRotation -= zExRotation;
                        break;
                    case < 0:
                        _currentXRotation += zExRotation;
                        break;
                }
        
                _currentXRotation = Mathf.Clamp(_currentXRotation, _sideLimits.RotationLimits.x, _orthographicLimits.RotationLimits.y);
        
                transform.rotation = Quaternion.Euler(_currentXRotation, 0f, -90f);
                break;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _inputMovement = context.ReadValue<Vector2>();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        _inputMovement = Vector2.zero;
    }

    private void OnDeflect(InputAction.CallbackContext context)
    {
        if(!_isGameStarted) return;
        
        deflect.Use();
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
        //TODO: dmg effect hit effect or any effect sound
        if (health >= 0)
            HealthDropped?.Invoke(health);
        if (health <= 0)
        {
            Debug.LogError("Dead!");
            IsDead = true;
            GameManager.Instance.LostGame();
        }
        
    }

    private struct AxisLimits
    {
        public Vector2 XPosLimits;
        public Vector2 YPosLimits;
        public Vector2 ZPosLimits;
        public Vector2 RotationLimits;
    }
}

