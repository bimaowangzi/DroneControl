using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerSime : MonoBehaviour
{

    public float Throttle = 0.0f;
    public float Yaw = 0.0f;
    public float Pitch = 0.0f;
    public float Roll = 0.0f;

    public Text debugText;
    public Text valuesText;

    public float throttleSensitivity;
    public float YawSensitivity;
    public float pitchSensitivity;
    public float rollSensitivity;
    private Vector3 pointerStart;

    // accelStart are intialized to impossible values since X and Y are between -1 and 1
    // accelX is tilt right and left => roll
    // accelY is nod forward and backward +> pitch
    private float accelXStart = -2;
    private float accelYStart = -2;
    private float accelXdf = 0;
    private float accelYdf = 0;

    public enum ThrottleMode { None, LockHeight };

    [Header("Throttle command")]
    //public string ThrottleCommand = "Throttle";
    public bool InvertThrottle = true;

    [Header("Yaw Command")]
    //public string YawCommand = "Yaw";
    public bool InvertYaw = false;

    [Header("Pitch Command")]
    //public string PitchCommand = "Pitch";
    public bool InvertPitch = true;

    [Header("Roll Command")]
    //public string RollCommand = "Roll";
    public bool InvertRoll = true;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {

        if (accelXStart == -2 || accelYStart == -2)
        {
            //accelXStart = Input.acceleration.x;
            //accelYStart = Input.acceleration.y;

            accelXStart = 0f;
            accelYStart = 0f;
        }

        accelXdf = (Input.acceleration.x - accelXStart) * pitchSensitivity;
        accelYdf = (Input.acceleration.y - accelYStart) * rollSensitivity;

        //valuesText.text = string.Format("accelX: {0}\naccelY: {1}",
        //                  accelXdf, accelYdf);

        Pitch = (accelYdf) * (InvertPitch ? -1 : 1);
        Roll = (accelXdf) * (InvertRoll ? -1 : 1);

        if (Input.GetMouseButtonDown(0))
        {
            debugText.text = "Touch began";

            pointerStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            debugText.text = "Touch Up";

            Throttle = (0) * (InvertThrottle ? -1 : 1);
            Yaw = (0) * (InvertYaw ? -1 : 1);
        }
        // mouse click hold detected
        else if (Input.GetMouseButton(0))
        {
            Vector3 pointerCurrent = Input.mousePosition;

            debugText.text = "Touch moved";

            Vector3 pointerDF = (pointerCurrent - pointerStart);

            //float Xdf = pointerDF.x;
            //float Ydf = pointerDF.y;

            //valuesText.text = string.Format("Xdf: {0}\nYdf: {1}",
            //          Xdf, Ydf);

            pointerDF.Normalize();

            float pointerXdf = pointerDF.x * YawSensitivity;
            float pointerYdf = pointerDF.y * throttleSensitivity;

            //valuesText.text = string.Format("xdf: {0}\nydf: {1}, Updated",
            //          pointerXdf, pointerYdf);

            Throttle = (pointerYdf) * (InvertThrottle ? -1 : 1);
            Yaw = (pointerXdf) * (InvertYaw ? -1 : 1);
        }
        else
        {
            debugText.text = "No touches";

            Throttle = (0) * (InvertThrottle ? -1 : 1);
            Yaw = (0) * (InvertYaw ? -1 : 1);
        }

        //Throttle = Input.GetAxisRaw(ThrottleCommand) * (InvertThrottle ? -1 : 1);
        //Yaw = Input.GetAxisRaw(YawCommand) * (InvertYaw ? -1 : 1);
        //Pitch = Input.GetAxisRaw(PitchCommand) * (InvertPitch ? -1 : 1);
        //Roll = Input.GetAxisRaw(RollCommand) * (InvertRoll ? -1 : 1);

        valuesText.text = string.Format("Throttle: {0}, Yaw: {1}\nPitch: {2}, Roll: {3}",
                      round(Throttle/throttleSensitivity * (InvertThrottle ? -1 : 1), 3), round(Yaw/YawSensitivity * (InvertYaw ? -1 : 1), 3), round(Pitch/pitchSensitivity * (InvertPitch ? -1 : 1), 3), round(Roll/rollSensitivity * (InvertRoll ? -1 : 1), 3));

    }

    float round(float value,int dp)
    {
        return Mathf.Round(value * (Mathf.Pow(10, dp))) / (Mathf.Pow(10, dp));
    }

}
