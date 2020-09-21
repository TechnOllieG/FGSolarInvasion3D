using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FG
{
    public class CanvasManager : MonoBehaviour
    {
        public GameObject gameOverMenu;
        public GameObject selectedWeaponDisplay;
        
        public Text pointDisplay;
        public string pointDisplayPrefix = "Points: ";
        public Text hpDisplay;
        public string hpDisplayPrefix = "HP:";
        
        public float gameOverMenuDelay = 2f;

        public void UpdatePoints(int currentPoints)
        {
            pointDisplay.text = pointDisplayPrefix + currentPoints;
        }

        public void UpdateHP(float currentHitpoints)
        {
            hpDisplay.text = hpDisplayPrefix + currentHitpoints;
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