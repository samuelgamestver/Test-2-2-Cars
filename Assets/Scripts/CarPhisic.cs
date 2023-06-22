using UnityEngine;

public class CarPhisic : MonoBehaviour
{
    Rigidbody Rb;

    [SerializeField] Transform COM;

    [SerializeField] int MotorPower = 200;

    [SerializeField] int BrakePower = 150;

    public bool FWD = true;

    public bool RWD = false;

    public int Accel;

    public int Brake;

    public int Steer;

    [SerializeField] WheelCollider[] FWCol;

    [SerializeField] Transform[] FWMesh;

    [SerializeField] WheelCollider[] RWCol;

    [SerializeField] Transform[] RWMesh;

    [SerializeField] Light[] StopLights;

    public float Speed;

    private float fwdSpeed;

    private void Awake()
    {
        Rb = gameObject.GetComponent<Rigidbody>();

        Rb.centerOfMass = COM.localPosition;
    }

    private void FixedUpdate()
    {
        Speed = Rb.velocity.magnitude * 3.6f;

        fwdSpeed = Vector3.Dot(Rb.velocity, transform.forward);

        for (int i = 0; i < FWCol.Length; i++)
        {
            FWCol[i].steerAngle = Mathf.Lerp(FWCol[i].steerAngle, 28f * Steer, 1.9f * Time.deltaTime);

            if (fwdSpeed >= 0)
            {
                if (FWD)
                    FWCol[i].motorTorque = MotorPower * Accel / FWCol.Length;

                FWCol[i].brakeTorque = BrakePower * Brake;

            } 
            else
            {
                if (FWD)
                    FWCol[i].motorTorque = -MotorPower * Brake / FWCol.Length;

                FWCol[i].brakeTorque = BrakePower * Accel;
            }

        }


        for (int j = 0; j < RWCol.Length; j++)
        {
            if (fwdSpeed >= 0)
            {
                if (RWD)
                    RWCol[j].motorTorque = MotorPower * Accel / FWCol.Length;

                RWCol[j].brakeTorque = BrakePower * Brake;

            }
            else
            {
                if (RWD)
                    RWCol[j].motorTorque = -MotorPower * Brake / FWCol.Length;

                RWCol[j].brakeTorque = BrakePower * Accel;
            }
        }

       

        

    }
    private void Update()
    {
        for( int i = 0; i< FWCol.Length; i ++)
        {

            Vector3 FWpos = Vector3.zero;

            Quaternion FWrot = new Quaternion(0,0,0,0);

            FWCol[i].GetWorldPose(out FWpos, out FWrot);

            FWMesh[i].position = FWpos;

            FWMesh[i].rotation = FWrot;
        }

        for (int j = 0; j < RWCol.Length; j++)
        {

            Vector3 RWpos = Vector3.zero;

            Quaternion RWrot = new Quaternion(0, 0, 0, 0);

            RWCol[j].GetWorldPose(out RWpos, out RWrot);

            RWMesh[j].position = RWpos;

            RWMesh[j].rotation = RWrot;
        }


        for (int l = 0; l < StopLights.Length; l++)
        {
            if ((Brake > 0 && fwdSpeed > 0) || (Accel > 0 && fwdSpeed < 0))
               StopLights[l].enabled = true;
            else
                StopLights[l].enabled = false;
        }
    }
}
