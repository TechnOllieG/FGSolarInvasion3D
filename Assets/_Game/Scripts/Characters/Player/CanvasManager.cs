using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FG
{
    public enum HomingCrosshairState
    {
        Disabled,
        Resting,
        Targeting,
        Locked
    }
    
    public class CanvasManager : MonoBehaviour
    {
        public GameObject gameOverMenu;
        public float gameOverMenuDelay = 2f;
        
        public GameObject selectedWeaponDisplay;

        public Text pointDisplay;
        public string pointDisplayPrefix = "Points: ";
        public Text hpDisplay;
        public string hpDisplayPrefix = "HP:";

        [Tooltip("Crosshair at the center of the screen")]
        public Image crosshair;
        [Tooltip("The crosshair that follows the target when target is locked")]
        public Image homingTargetCrosshair;

        public Color defaultCrosshairColor = Color.white;
        public Color homingLockedColor = Color.red;

        public void UpdatePoints(int currentPoints)
        {
            pointDisplay.text = pointDisplayPrefix + currentPoints;
        }

        public void UpdateHP(float currentHitpoints)
        {
            hpDisplay.text = hpDisplayPrefix + currentHitpoints;
        }

        public void UpdateHomingCrosshair(HomingCrosshairState state, Transform target = null)
        {
            switch (state)
            {
                case HomingCrosshairState.Disabled:
                    crosshair.gameObject.SetActive(false);
                    homingTargetCrosshair.gameObject.SetActive(false);
                    break;
                case HomingCrosshairState.Resting:
                    crosshair.gameObject.SetActive(true);
                    homingTargetCrosshair.gameObject.SetActive(false);
                    crosshair.color = defaultCrosshairColor;
                    break;
                case HomingCrosshairState.Targeting:
                    crosshair.gameObject.SetActive(true);
                    homingTargetCrosshair.gameObject.SetActive(false);
                    crosshair.color = homingLockedColor;
                    break;
                case HomingCrosshairState.Locked:
                    crosshair.gameObject.SetActive(true);
                    homingTargetCrosshair.gameObject.SetActive(true);
                    crosshair.color = homingLockedColor;
                    homingTargetCrosshair.color = homingLockedColor;
                    break;
            }
        }

        public void GameOver()
        {
            StartCoroutine(GameOverCoroutine());
        }
        
        private IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(gameOverMenuDelay);
            gameOverMenu.SetActive(true);
            GameManager.ReleaseMouse();
            if (selectedWeaponDisplay == isActiveAndEnabled)
            {
                selectedWeaponDisplay.SetActive(false);
            }
        }
    }
}