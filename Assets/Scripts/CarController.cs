using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    [Header("Wheel Meshes")]
    public Transform meshFL;
    public Transform meshFR;
    public Transform meshRL;
    public Transform meshRR;

    [Header("Car Settings")]
    public float motorForce = 1500f;
    public float maxSteerAngle = 30f;
    public float brakeForce = 4500f;
    public float handBrakeForce = 8000f;

    Rigidbody rb;

    float motorInput;
    float steerInput;
    bool brakeInput;
    bool handBrakeInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // CONFIGURAÇÃO IDEAL DO CARRO
        rb.mass = 900f;
        rb.drag = 0.05f;
        rb.angularDrag = 0.05f;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        SetupWheelFriction(wheelFL);
        SetupWheelFriction(wheelFR);
        SetupWheelFriction(wheelRL);
        SetupWheelFriction(wheelRR);
    }

    void Update()
    {
        motorInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

        brakeInput = Input.GetKey(KeyCode.Space);   // FREIO
        handBrakeInput = Input.GetKey(KeyCode.LeftShift); // FREIO DE MÃO

        UpdateWheelMeshes();
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        HandleBrakes();
    }

    // ================= MOTOR =================
    void HandleMotor()
    {
        float motor = motorInput * motorForce;

        wheelRL.motorTorque = motor;
        wheelRR.motorTorque = motor;
    }

    // ================= DIREÇÃO =================
    void HandleSteering()
    {
        float steer = steerInput * maxSteerAngle;

        wheelFL.steerAngle = steer;
        wheelFR.steerAngle = steer;
    }

    // ================= FREIOS =================
    void HandleBrakes()
    {
        float brake = brakeInput ? brakeForce : 0f;

        wheelFL.brakeTorque = brake;
        wheelFR.brakeTorque = brake;
        wheelRL.brakeTorque = brake;
        wheelRR.brakeTorque = brake;

        // FREIO DE MÃO (SÓ TRASEIRAS)
        if (handBrakeInput)
        {
            wheelRL.brakeTorque = handBrakeForce;
            wheelRR.brakeTorque = handBrakeForce;
        }
    }

    // ================= ATRITO DAS RODAS =================
    void SetupWheelFriction(WheelCollider wheel)
    {
        WheelFrictionCurve forward = wheel.forwardFriction;
        forward.extremumSlip = 0.4f;
        forward.extremumValue = 1f;
        forward.asymptoteSlip = 0.8f;
        forward.asymptoteValue = 0.5f;
        forward.stiffness = 2.2f;
        wheel.forwardFriction = forward;

        WheelFrictionCurve side = wheel.sidewaysFriction;
        side.extremumSlip = 0.2f;
        side.extremumValue = 1f;
        side.asymptoteSlip = 0.5f;
        side.asymptoteValue = 0.75f;
        side.stiffness = 2.5f;
        wheel.sidewaysFriction = side;
    }

    // ================= WHEEL MESH =================
    void UpdateWheelMeshes()
    {
        UpdateWheel(wheelFL, meshFL);
        UpdateWheel(wheelFR, meshFR);
        UpdateWheel(wheelRL, meshRL);
        UpdateWheel(wheelRR, meshRR);
    }

    void UpdateWheel(WheelCollider wc, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        wc.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }
}
