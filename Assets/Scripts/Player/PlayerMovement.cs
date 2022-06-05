using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        [Header("Player")]
        [SerializeField] private VariableJoystick variableJoystick;
        [SerializeField] private CharacterController controller;

        [Header("Movement")]
        [SerializeField] private float horizontalmovementSpeed;
        [SerializeField] private float verticalMovementSpeed;

        [SerializeField] private float horizontalSpeedFactor;
        [SerializeField] private float thresholdToSpeedUp;
        [SerializeField] private float rotSpeed;

        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject meshGO;

        private Vector3 forward;
        private Vector3 right;

        private Camera mainCamera;

        bool isMoving;
        private bool canMove;
        private float normalSpeed;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            normalSpeed = verticalMovementSpeed;
        }

        private void Start()
        {
            // PlayerPrefs.DeleteAll();
            MyColissionHandler.OnPlayerDeath += HandleOnPlayerDeath;
            EatableBags.OnPlayerSlowMovement += HandleOnPlayerSlowMovement;
            Obstacle.OnPlayerSlowMovement += HandleOnPlayerSlowMovement;
            Boss.OnPlayerSlowMovement += HandleOnPlayerSlowMovement;
            MiniBoss.OnPlayerSlowMovement += HandleOnPlayerSlowMovement;
            isMoving = true;
            CancelPlayerControls();
           // StartMovementAnimation();
        }

        private void OnDestroy()
        {
            EatableBags.OnPlayerSlowMovement -= HandleOnPlayerSlowMovement;
            Obstacle.OnPlayerSlowMovement -= HandleOnPlayerSlowMovement;
            MyColissionHandler.OnPlayerDeath -= HandleOnPlayerDeath;
            Boss.OnPlayerSlowMovement -= HandleOnPlayerSlowMovement;
            MiniBoss.OnPlayerSlowMovement -= HandleOnPlayerSlowMovement;
        }
        
        private void Update()
        {
            if (!canMove)
            {
                return;
            }
            CheckIfTouchingScreen();
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (!isMoving || !canMove ) { return; }
            
            forward = verticalMovementSpeed * Time.deltaTime * Vector3.forward;

            right = Vector3.right * variableJoystick.Horizontal * horizontalmovementSpeed;
            

            if(Mathf.Abs(variableJoystick.Horizontal) > thresholdToSpeedUp )
            {
                right *= horizontalSpeedFactor;
            }

            Vector3 movement = forward + right/10;
            meshGO.transform.localRotation = Quaternion.Slerp(meshGO.transform.localRotation,Quaternion.LookRotation(movement),Time.deltaTime * rotSpeed ); 
            
            controller.Move( forward + right );
        }

        private void CheckIfTouchingScreen()
        {
            if(Input.touchCount > 0)
            {
                isMoving = true;
                _animator.SetBool("isMoving", true);
            } 
            else
            {
                isMoving = false;
                _animator.SetBool("isMoving", false);
            }
        }

        public void SlowDownPlayer()
        {
            StartCoroutine(SlowDown());
        }

        private IEnumerator SlowDown()
        {
            verticalMovementSpeed /= 2;

            yield return new WaitForSeconds(1f);

            verticalMovementSpeed = normalSpeed;
        }

        public void CancelPlayerControls()
        {
            canMove = false;
            variableJoystick.gameObject.SetActive(false);
        }

        public void EnablePlayerControls()
        {
            canMove = true;
            variableJoystick.gameObject.SetActive(true);
        }
        
        private void HandleOnPlayerSlowMovement()
        {
            StartCoroutine(SlowDown());
        }
        
        private void HandleOnPlayerDeath()
        {
            CancelPlayerControls();
        }


}
