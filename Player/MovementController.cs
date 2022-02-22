using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Movement
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]

    public class MovementController : MonoBehaviour
    {
        public InventoryObject inventory;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float speedSmoothTime = 0.1f;
        //private float jumpspeed = 10.0f;
        //private float gravity = 10.0f;

        private CharacterController contorller = null;
        private Animator animator = null;
        private Transform mainCameraTransfrom = null;

        //private float velocityY = 0f;
        private float speedSmoothVelocity = 0f;
        private float currentSpeed = 0f;

        private static readonly int hashSpeedPercenttage = Animator.StringToHash("SpeedPercentage");

        private void Start()
        {
            contorller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            mainCameraTransfrom = Camera.main.transform;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 movementInput = new Vector2(
                Input.GetAxisRaw("Horizontal"), 
                Input.GetAxisRaw("Vertical")
                ).normalized;

            Vector3 forward = mainCameraTransfrom.forward;
            Vector3 right = mainCameraTransfrom.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

            if (desiredMoveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
            }

            float targetSpeed = moveSpeed * movementInput.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            /*
             if (Input.GetKeyDown(KeyCode.Space))
             {
                 print("1");
                 desiredMoveDirection.y = jumpspeed;
             }
             desiredMoveDirection.y -= gravity * Time.deltaTime;
         */

            contorller.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);

            animator.SetFloat(hashSpeedPercenttage, 0.5f * movementInput.magnitude, speedSmoothTime, Time.deltaTime);         
        }

        public void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<GroundItem>();
            if (item)
            {
                inventory.AddItem(new Item(item.item), 1);
                Destroy(other.gameObject);
            }
        }
    }
}

