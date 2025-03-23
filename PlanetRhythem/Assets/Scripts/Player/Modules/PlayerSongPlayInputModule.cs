using UnityEngine;
using UnityEngine.InputSystem;

namespace Rhythem.Play
{
    public class PlayerSongPlayInputModule : InputModule
    {
        protected override void Start()
        {
            base.Start();
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

        protected override void OnConfirmPerformed(InputAction.CallbackContext context)
        {
            base.OnConfirmPerformed(context);
            //TEMP FOR TESTING SESSIONS
            GameManager.Instance.LoadScene(1, true);
            SessionsManager.Instance.LoadSession<SongSession>();
        }

        protected override void OnBackPerformed(InputAction.CallbackContext context)
        {
            base.OnBackPerformed(context);

        }

        protected override void OnPausePerformed(InputAction.CallbackContext context)
        {
            base.OnPausePerformed(context);
        }

        protected override void OnSelectionChangePerformed(InputAction.CallbackContext context)
        {
            //base.OnSelectionChangePerformed(context);
            var dir = controls.Player.ChangeSelection.ReadValue<Vector2>();
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {

            }
        }

        protected override void SubscribeToControls()
        {
            base.SubscribeToControls();
        }

        protected override void UnsubscribeToControls()
        {
            base.UnsubscribeToControls();
        }
    }
}