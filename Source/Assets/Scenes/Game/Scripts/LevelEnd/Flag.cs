using System.Collections;
using Scenes.Game.Scripts.Mario;
using UnityEngine;

namespace Scenes.Game.Scripts.LevelEnd
{
    public class Flag : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var cutsceneController = other.gameObject.GetComponent<CutsceneController>();
            cutsceneController.LowerFlag = () => StartCoroutine(LowerFlag());
            cutsceneController.GotFlag(gameObject);
        }
        
        public IEnumerator LowerFlag()
        {
            while (gameObject.activeSelf)
            {
                var pos = transform.position;
                pos += Vector3.down * 0.1f;
                transform.position = pos;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
