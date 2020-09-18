using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace FG
{
    public class CanvasManager : MonoBehaviour
    {
        public GameObject gameOverMenu;
        public GameObject selectedWeaponDisplay;
        public float gameOverMenuDelay = 2f;
            
        private GameObject _player;
        private Target _playerTargetScript;
        
        private void Awake()
        {
            _player = GameManager.Player;
            Assert.IsNotNull(_player, "CanvasManager could not find player game object through GameManager");
            _playerTargetScript = _player.GetComponent<Target>();
            Assert.IsNotNull(_playerTargetScript, "CanvasManager could not find Target script on player");

            _playerTargetScript.canvasManager = this;
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