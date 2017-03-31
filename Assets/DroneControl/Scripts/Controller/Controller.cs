using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public float Throttle = 0.0f;
    public float Yaw = 0.0f;
    public float Pitch = 0.0f;
    public float Roll = 0.0f;

    public Text debugText;
    public Text valuesText;

    public float touchSensitivity;
    public float pitchSensitivity;
    public float rollSensitivity;
    private Vector2 pointerStart;

    // accelStart are intialized to impossible values since X and Y are between -1 and 1
    private float accelXStart = -2;
    private float accelYStart = -2;
    private float accelXdf = 0;
    private float accelYdf = 0;

    public enum ThrottleMode { None, LockHeight };

    [Header("Throttle command")]
    public string ThrottleCommand = "Throttle";
    public bool InvertThrottle = true;

    [Header("Yaw Command")]
    public string YawCommand = "Yaw";
    public bool InvertYaw = false;

    [Header("Pitch Command")]
    public string PitchCommand = "Pitch";
    public bool InvertPitch = true;

    [Header("Roll Command")]
    public string RollCommand = "Roll";
    public bool InvertRoll = true;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {

        if (accelXStart == -2 || accelYStart == -2)
        {
            accelXStart = Input.acceleration.x;
            accelYStart = Input.acceleration.y;
        }

        accelXdf = (Input.acceleration.x - accelXStart) * pitchSensitivity;
        accelYdf = (Input.acceleration.y - accelYStart) * rollSensitivity;

        //valuesText.text = string.Format("accelX: {0}\naccelY: {1}",
        //                  accelXdf, accelYdf);

        //Pitch = (accelXdf) * (InvertPitch ? -1 : 1);
        //Roll = (accelYdf) * (InvertRoll ? -1 : 1);

        if (Input.touchCount != 0)
        {
            //debugText.text = Input.touchCount + " touches detected";

            //To get the finger id
            //Input.touches[0].fingerId

            Touch pointer1 = Input.touches[0];
            if (pointer1.phase == TouchPhase.Began)
            {
                debugText.text = "Touch began";

                pointerStart = Input.touches[0].position;
            }
            else if (pointer1.phase == TouchPhase.Moved)
            {
                debugText.text = "Touch moved";

                Vector2 pointerCurrent = Input.touches[0].position;
                Vector2 pointerDF = (pointerCurrent - pointerStart);

                //float Xdf = pointerDF.x;
                //float Ydf = pointerDF.y;

                //valuesText.text = string.Format("Xdf: {0}\nYdf: {1}",
                //          Xdf, Ydf);

                pointerDF.Normalize();

                float pointerXdf = pointerDF.x * touchSensitivity;
                float pointerYdf = pointerDF.y * touchSensitivity;

                valuesText.text = string.Format("xdf: {0}\nydf: {1}",
                          pointerXdf, pointerYdf);

                Throttle = (pointerYdf) * (InvertThrottle ? -1 : 1);
                Yaw = (pointerXdf) * (InvertYaw ? -1 : 1);
            }
        }
        else
        {
            debugText.text = "No touches";
        }

        //Throttle = Input.GetAxisRaw(ThrottleCommand) * (InvertThrottle ? -1 : 1);
        //Yaw = Input.GetAxisRaw(YawCommand) * (InvertYaw ? -1 : 1);
        //Pitch = Input.GetAxisRaw(PitchCommand) * (InvertPitch ? -1 : 1);
        //Roll = Input.GetAxisRaw(RollCommand) * (InvertRoll ? -1 : 1);
    }

}
