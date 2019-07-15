//
// 	Copyright (C) 2019 Outlaw Games Studio. All Rights Reserved.
//
// 	This document is the property of Outlaw Games Studio.
// 	It is considered confidential and proprietary.
//
// 	This document may not be reproduced or transmitted in any form
// 	without the consent of Outlaw Games Studio.
//

using UnityEngine;

namespace Core.Player
{
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

        private Transform m_Camera;

        #endregion

        #region Methods

        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Camera = UnityEngine.Camera.main.transform;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (m_Jumping == true)
            {
                m_Jumping = false;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            // TODO: Set player to face direction of camera when pressing forward button.
            velocity = m_Rigidbody.velocity;

            if (Input.GetKeyUp(KeyCode.Space) && m_Jumping == false)
            {
                m_Jumping = true;
                transform.Translate(transform.up * m_JumpForce * Time.deltaTime, Space.World);
                //m_Rigidbody.AddForce(new Vector3(0, m_JumpForce * Time.deltaTime, 0));
            }

            Vector2 _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 _inputDir = _input.normalized;

            if (_inputDir != Vector2.zero)
            {
                float _targetRot = Mathf.Atan2(_inputDir.x, _inputDir.y) * Mathf.Rad2Deg + m_Camera.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRot, ref m_TurnSmoothVelocity, m_TurnSmoothTime);
            }

            bool _running = Input.GetKey(KeyCode.LeftShift);
            float _speed = ((_running) ? m_RunSpeed : m_WalkSpeed) * _inputDir.magnitude;

            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }

        #endregion
    }
}
