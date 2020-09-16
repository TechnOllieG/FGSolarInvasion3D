using UnityEngine;
using System.Collections;

namespace FG
{
    public class EnemySpawn : MonoBehaviour
    {
        #region Inspector
        public GameObject[] enemyPrefabs = new GameObject[1];
        [Tooltip("The minimum distance from the player the enemy should spawn"), Range(100f, 5000f)]
        public float minDistanceFromPlayer = 20f;
        [Tooltip("The maximum distance from the player the enemy should spawn"), Range(100f, 5000f)]
        public float maxDistanceFromPlayer = 30f;
        [Tooltip("The minimum cooldown (chosen randomly) before spawning a new enemy, expressed in seconds"), Range(0.5f, 60f)]
        public float minCooldown = 5f;
        [Tooltip("The maximum cooldown (chosen randomly) before spawning a new enemy, expressed in seconds"), Range(0.5f, 60f)]
        public float maxCooldown = 10f;
        [Tooltip("The radius around the determined random position that should be clear of any colliders for the position to be valid"), Range(1f, 20f)]
        public float clearRadius = 2f;
        #endregion

        #region PrivateVariables
        private Transform _playerTransform;
        #endregion

        private void OnValidate()
        {
            if (maxDistanceFromPlayer < minDistanceFromPlayer)
            {
                maxDistanceFromPlayer = minDistanceFromPlayer;
            }
            
            if (maxCooldown < minCooldown)
            {
                maxCooldown = minCooldown;
            }
        }

        private void Awake()
        {
            _playerTransform = GameManager.Player.transform;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                Vector3 direction = _playerTransform.forward;
                Vector3 enemyPosition = _playerTransform.position;
                
                bool positionInCollider = true;
                
                while (positionInCollider)
                {
                    float distance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
                    direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                    enemyPosition = _playerTransform.position - direction * distance;

                    if (!Physics.CheckSphere(enemyPosition, clearRadius))
                    {
                        positionInCollider = false;
                    }
                }

                float facingAngleY = GetAngle(new Vector2(direction.x, direction.z));
                Quaternion enemyRotation = Quaternion.Euler(0, facingAngleY, 0);

                int enemyType = Random.Range(0, enemyPrefabs.Length - 1);

                Instantiate(enemyPrefabs[enemyType], enemyPosition, enemyRotation);
                float timeToWait = Random.Range(minCooldown, maxCooldown);
                yield return new WaitForSeconds(timeToWait);
            }
        }
        
        private static float GetAngle(Vector2 direction)
        {
            float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
            return direction.x < 0f ? 360f - angle : angle;
        }
    }
}
