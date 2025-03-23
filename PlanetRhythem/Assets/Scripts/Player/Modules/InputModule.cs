using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;

namespace Rhythem.Play
{
    public class InputModule : Module
    {
        [Title("Player-Specific References")]
        public TrackedPoseDriver head;
        public PlayerWand leftHand;
        public PlayerWand rightHand;
        protected PlayerControls controls;

        private Ray _selectionRay;
        private RaycastHit _selectionHit;

        protected override void Start()
        {
            base.Start();
            controls ??= new PlayerControls();
            SubscribeToControls();
        }
        protected virtual void OnDestroy()
        {
            UnsubscribeToControls();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            controls ??= new PlayerControls();
            controls.Enable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            controls?.Disable();
        }
        protected override void Update()
        {
            base.Update();
        }

        protected virtual void OnConfirmPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("CONFIRM PRESSED");
        }

        protected virtual void OnBackPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("BACK PRESSED");
        }

        protected virtual void OnPausePerformed(InputAction.CallbackContext context)
        {
            Debug.Log("PAUSE PRESSED");
            GameManager.Instance.PauseGame(!GameManager.Instance.Paused);
        }

        protected virtual void OnSelectionChangePerformed(InputAction.CallbackContext context)
        {
            var dir = controls.Player.ChangeSelection.ReadValue<Vector2>();
            Debug.Log($"DIRECTION PRESSED: {dir}");
        }

        protected virtual void SubscribeToControls()
        {
            if (controls == null) { return; }
            controls.Player.Interact.performed += OnConfirmPerformed;
            controls.Player.Back.performed += OnBackPerformed;
            controls.Player.Pause.performed += OnPausePerformed;
            controls.Player.ChangeSelection.performed += OnSelectionChangePerformed;
        }
        protected virtual void UnsubscribeToControls()
        {
            if (controls == null) { return; }
            controls.Player.Interact.performed -= OnConfirmPerformed;
            controls.Player.Back.performed -= OnBackPerformed;
            controls.Player.Pause.performed -= OnPausePerformed;
            controls.Player.ChangeSelection.performed -= OnSelectionChangePerformed;
        }
    }
}