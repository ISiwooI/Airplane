using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlaneMovement : MonoBehaviour
{
    private float _enginePower=0f;
    private Rigidbody _rb;
    [SerializeField] private float accelAmount;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rollAmount;
    [SerializeField] private float pitchAmount;
    [SerializeField] private float yawAmount;
    #region Input
    //-----InputValues------//
    private float i_engine = 0f;
    private float i_roll = 0f;
    private float i_pitch = 0f;
    private float i_yaw = 0f;
    //-----InputEvent-----//
    void OnEngine(InputValue value) {
        i_engine = value.Get<float>();
        i_engine -= 0.3f;
        
    }
    void OnLook(InputValue value) {
        var inputValue = value.Get<Vector2>();
        i_roll=inputValue.x;
        i_pitch=inputValue.y;
        Debug.Log(i_roll);
    }
    void OnMove(InputValue value)
    {
        var inputValue = value.Get<Vector2>();
        i_yaw=inputValue.x;
    }

    void OnExit(InputValue value)
    {
        Application.Quit();
    }
    #endregion Input
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //------rotate-------//
        _rb.MoveRotation(transform.rotation*Quaternion.Euler(pitchAmount*i_pitch*Time.deltaTime,rollAmount*i_roll*Time.deltaTime,  yawAmount*i_yaw*Time.deltaTime));
        //-----speed------//
        float speedEase=(maxSpeed-_enginePower)/maxSpeed;
        _enginePower+=i_engine*accelAmount*Time.fixedDeltaTime*speedEase;
        if(_enginePower<maxSpeed/10)
            _enginePower=maxSpeed/10;
        _rb.linearVelocity = transform.up * -(_enginePower * Time.fixedDeltaTime);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
