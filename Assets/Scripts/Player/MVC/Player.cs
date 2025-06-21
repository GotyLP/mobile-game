using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerMVC
{
    public class Player : MonoBehaviour
    {

        [field: SerializeField] public PlayerModel Model { get; private set; }
        private PlayerView _view;
        private PlayerController _controller;

        public Rigidbody Rigidbody {  get; private set; }
        public Animator Animator { get; private set; }

        [field: SerializeField] public float StartLife { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Joystick Joystick { get; private set; }



        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
           
            Model = new PlayerModel(this);
            _view = new PlayerView(this);
            _controller = new PlayerController(this);

            EventManager.Subscribe(SimpleEventType.PlayerDeathEvent, DisableComponent);
            
        }

        void Update()
        {
            _controller.UpdateInputs();
        }

        private void FixedUpdate()
        {
            _controller.FixUpdateInputs();
        }

        private void DisableComponent()
        {
            enabled = false;   
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe(SimpleEventType.PlayerDeathEvent, DisableComponent);
        }
    }

}

