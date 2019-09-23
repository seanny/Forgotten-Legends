//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using Core.Actor;
using Core.Camera;
using Core.Services;
using UnityEngine;

namespace Core.Player
{
    // Ensure that a rigidbody is assigned.
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        public Vector3 velocity;

        [SerializeField] private float m_JumpForce = 5f;
        [SerializeField] private float m_WalkSpeed = 2f;
        [SerializeField] private float m_RunSpeed = 6f;

        [SerializeField] private float m_TurnSmoothTime = 0.2f;
        [SerializeField] private float m_TurnSmoothVelocity;
        [SerializeField] private Rigidbody m_Rigidbody;
        private bool m_Jumping;
        private bool m_Falling;
        private float m_Speed;
        private Vector3 m_Direction;
        private float FALLING_VELOCITY = -7.5f;
        
        private Transform m_Camera;
        private ActorAnimationController m_AnimationController;

        #endregion

        #region Methods

        private void Start()
        {
            // Assign the main components via script instead of drag-n-drop.
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Camera = UnityEngine.Camera.main.transform;
            
            // We do not need to RequireComponenet ActorAnimationController
            // as it will be automatically RequireComponenet'd by the Actor class (which the Player is a child of)'
            m_AnimationController = GetComponent<ActorAnimationController>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // If the player is falling or jumping and they detect a collision, stop the falling animation.
            if (m_Falling == true || m_Jumping == true)
            {
                m_AnimationController.StopFalling();
                m_Falling = false;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            // Ensure that the velocity is set to the rigidbody velocity.
            velocity = m_Rigidbody.velocity;

            // Check if the velocity is below FALLING_VELOCITY to determine if we are falling.
            if (velocity.y < FALLING_VELOCITY)
            {
                // Tell the class that we are indeed falling and also enable the animation accordingly.
                m_Falling = true;
                m_AnimationController.StartFalling();
                return;
            }

            // If the free camera is enabled, stop executing the code at this point.
            if (ServiceLocator.GetService<CameraController>().freeCamera == true)
            {
                return;
            }
            
            // If the player presses a specific key (SPACE in our case), tell the class that we want to jump.
            if (Input.GetKeyUp(KeyCode.Space) && m_Jumping == false)
            {
                m_AnimationController.StartJump();
                m_Jumping = true;
            }

            // If the player is pressing their WASD keys or controller equivelants, set the movement and movement direction.
            Vector2 _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 _inputDir = _input.normalized;

            // If the player _inputDir is not 0,0,0 move the camera around the player.
            if (_inputDir != Vector2.zero)
            {
                // Set the camera target rotation to go around the player based on their current position and also smooth the camera rotation
                float _targetRot = Mathf.Atan2(_inputDir.x, _inputDir.y) * Mathf.Rad2Deg + m_Camera.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRot, ref m_TurnSmoothVelocity, m_TurnSmoothTime);
            }

            // Determine if the player is running by checking if they are pressing their LEFT SHIFT key.
            bool _running = Input.GetKey(KeyCode.LeftShift);
            
            // Set the speed accordingly if they are running or walking.
            m_Speed = ((_running) ? m_RunSpeed : m_WalkSpeed) * _inputDir.magnitude;
            
            // Set the input direction.
            m_Direction = _inputDir;

            // If the player is not jumping, continue with this block.
            if (m_Jumping == false)
            {
                // If the player movement direction is not 0,0,0 then continue with this block.
                if (_inputDir != Vector2.zero)
                {
                    if (!_running)
                    {
                        // Set the player animation to the walking animation.
                        m_AnimationController.StartWalking();
                    }
                    else
                    {
                        // Set it to running otherwise.
                        m_AnimationController.StartRunning();
                    }
                }
                else
                {
                    // Set the player into an idle animation otherwise.
                    m_AnimationController.StartIdle();
                }
            }
        }

        private void FixedUpdate()
        {
            if (m_Jumping == true)
            {
                m_Rigidbody.MovePosition(transform.position + (transform.up * m_JumpForce * Time.deltaTime));
            }
            else
            {
                m_Rigidbody.MovePosition(transform.position + (transform.forward * m_Speed * Time.deltaTime));
            }
        }

        #endregion
    }
}
