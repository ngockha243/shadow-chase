using _0.Game.Scripts.State;
using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class PlayerMovement : StateMachine
    {
        public Camera cam;
        public Transform orientation;
        public PlayerController player;
        public float accelGround = 30f;
        public float accelAir = 10f;
        public float dragGround = 5f;
        public float dragAir = 0f;
        public float rotationSpeed = 12f;

        [Header("Jump")] public bool enableJump = true;
        public float jumpForce = 5f;
        public float coyoteTime = 0.1f;
        public float jumpCooldown = 0.1f;

        [Header("Ground Check")] public LayerMask groundMask = ~0;
        public float groundCheckRadius = 0.2f;
        public float groundCheckOffset = 0.05f;
        public float maxGroundAngle = 55f;

        // --- runtime ---
        Rigidbody rb;
        Collider col;
        Vector2 joyDir;
        Vector3 desiredMove;
        bool isGrounded;
        float lastGroundedTime;
        float lastJumpTime;
        private float movementSpeed;
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            col = GetComponentInChildren<Collider>();
            if (cam == null) cam = Camera.main;

            movementSpeed = player.stats.GetMoveMentSpeed();
        }

        void Update()
        {
            Vector3 planeForward, planeRight;

            if (orientation != null)
            {
                planeForward = Vector3.ProjectOnPlane(orientation.forward, Vector3.up).normalized;
                planeRight = Vector3.ProjectOnPlane(orientation.right, Vector3.up).normalized;
            }
            else if (cam != null)
            {
                planeForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
                planeRight = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
            }
            else
            {
                planeForward = Vector3.forward;
                planeRight = Vector3.right;
            }

            desiredMove = (planeRight * joyDir.x + planeForward * joyDir.y);
            if (desiredMove.sqrMagnitude > 1f) desiredMove.Normalize();

            
            
        }

        void FixedUpdate()
        {
            GroundCheck();

            rb.linearDamping = isGrounded ? dragGround : dragAir;

            Vector3 current = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            Vector3 target = desiredMove * movementSpeed;

            float accel = isGrounded ? accelGround : accelAir;

            Vector3 newFlat = Vector3.MoveTowards(current, target, accel * Time.fixedDeltaTime);

            rb.linearVelocity = new Vector3(newFlat.x, rb.linearVelocity.y, newFlat.z);

            if (isGrounded && rb.linearVelocity.y < 0f)
            {
                rb.AddForce(Physics.gravity * 0.5f, ForceMode.Acceleration);
            }
            
            
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            Vector3 look = flatVel.sqrMagnitude > 0.0001f ? flatVel.normalized : desiredMove;
            if (look.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(look, Vector3.up);
                Transform t = orientation ? orientation : transform;
                t.rotation = Quaternion.Slerp(t.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }

        void GroundCheck()
        {
            Vector3 basePoint = transform.position + Vector3.down * groundCheckOffset;

            float radius = groundCheckRadius;
            if (col is CapsuleCollider cap)
            {
                radius = cap.radius * 0.95f;
            }
            else if (col is CharacterController cc)
            {
                radius = cc.radius * 0.95f;
            }

            if (Physics.SphereCast(basePoint + Vector3.up * 0.1f, radius, Vector3.down, out RaycastHit hit, 0.3f,
                    groundMask, QueryTriggerInteraction.Ignore))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);
                isGrounded = angle <= maxGroundAngle;
            }
            else
            {
                isGrounded = false;
            }

            if (isGrounded) lastGroundedTime = Time.time;
        }


        public void SetJoystickDirection(Vector2 dir)
        {
            if(dir == Vector2.zero) ChangeState<PlayerIdle>();
            else ChangeState<PlayerMove>();
            joyDir = dir.magnitude > 1f ? dir.normalized : dir;
        }

        public void Jump()
        {
            if (!enableJump) return;
            bool canCoyote = (Time.time - lastGroundedTime) <= coyoteTime;
            bool cdReady = (Time.time - lastJumpTime) >= jumpCooldown;

            if ((isGrounded || canCoyote) && cdReady)
            {
                Vector3 v = rb.linearVelocity;
                if (v.y < 0f) v.y = 0f;
                rb.linearVelocity = v;

                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                lastJumpTime = Time.time;
            }
        }
    }
}