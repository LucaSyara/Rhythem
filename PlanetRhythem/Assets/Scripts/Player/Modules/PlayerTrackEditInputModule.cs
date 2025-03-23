using UnityEngine;
using UnityEngine.InputSystem;

namespace Rhythem.Play {
    public class PlayerTrackEditInputModule : InputModule
    {
        private PlayerWand _leftHand;
        private PlayerWand _rightHand;
        private Vector2 _directionLeft;
        private Vector2 _directionRight;

        protected override void Start()
        {
            base.Start();
            var gm = GameManager.Instance;
            _leftHand = gm.player.leftHand;
            _rightHand = gm.player.rightHand;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected virtual void OnConfirmLeftPerformed(InputAction.CallbackContext context)
        {
            _leftHand.DoNoteCreate(DesiredHand.Left, _directionLeft);
        }

        protected virtual void OnConfirmRightPerformed(InputAction.CallbackContext context)
        {
            _rightHand.DoNoteCreate(DesiredHand.Right, _directionRight);
        }

        protected override void OnBackPerformed(InputAction.CallbackContext context)
        {
            base.OnBackPerformed(context);

        }

        protected override void OnPausePerformed(InputAction.CallbackContext context)
        {
            base.OnPausePerformed(context);
        }

        protected virtual void OnSelectionChangeLeftPerformed(InputAction.CallbackContext context)
        {
            var dir = controls.SongEditor.ChangeNoteTypeLeft.ReadValue<Vector2>();
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir.y = 0f;
            }
            dir.x = Mathf.Ceil(dir.x);
            dir.y = Mathf.Ceil(dir.y);
            _directionLeft = dir;
        }

        protected virtual void OnSelectionChangeRightPerformed(InputAction.CallbackContext context)
        {
            var dir = controls.SongEditor.ChangeNoteTypeRight.ReadValue<Vector2>();
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                dir.y = 0f;
            }
            dir.x = Mathf.Ceil(dir.x);
            dir.y = Mathf.Ceil(dir.y);
            _directionRight = dir;
        }

        protected override void SubscribeToControls()
        {
            controls.SongEditor.CommitNoteLeft.performed += OnConfirmLeftPerformed;
            controls.SongEditor.CommitNoteRight.performed += OnConfirmRightPerformed;
            controls.SongEditor.Back.performed += OnBackPerformed;
            controls.SongEditor.Pause.performed += OnPausePerformed;
            controls.SongEditor.ChangeNoteTypeLeft.performed += OnSelectionChangeLeftPerformed;
            controls.SongEditor.ChangeNoteTypeRight.performed += OnSelectionChangeRightPerformed;
        }
            

        protected override void UnsubscribeToControls()
        {
            controls.SongEditor.CommitNoteLeft.performed -= OnConfirmLeftPerformed;
            controls.SongEditor.CommitNoteRight.performed -= OnConfirmRightPerformed;
            controls.SongEditor.Back.performed -= OnBackPerformed;
            controls.SongEditor.Pause.performed -= OnPausePerformed;
            controls.SongEditor.ChangeNoteTypeLeft.performed -= OnSelectionChangeLeftPerformed;
            controls.SongEditor.ChangeNoteTypeRight.performed -= OnSelectionChangeRightPerformed;
        }
    }
}