using TMPro;
using UnityEngine;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ayamaki.Game.GUI
{
    [ExecuteAlways]
    public class TextController : MonoBehaviour
    {
        [SerializeField] private TMP_Text targetText;
        [SerializeField] private float displaySpeed = 0.2f;
        private float displayTimer = 0f;
        [SerializeField] private string displayString = "";
        private int currentChar = -1;

        [Header("Editor Preview")]
        [SerializeField, Range(0, 1f)] public float previewProgress = 0f;
        private bool isAnimating = false;
        public UnityEvent onTextAnimationDone;

        private void Update()
        {
            // --- Editor Preview (sem precisar dar play) ---
        #if UNITY_EDITOR
            if (!Application.isPlaying && targetText != null && !string.IsNullOrEmpty(displayString))
            {
                int charCount = Mathf.FloorToInt(previewProgress * displayString.Length);
                targetText.text = displayString.Substring(0, charCount);
                return;
            }
        #endif
            if (Application.isPlaying && isAnimating)
            {
                if (targetText == null)
                    return;

                if (currentChar < displayString.Length - 1)
                {
                    displayTimer += Time.deltaTime;
                    if (displayTimer >= displaySpeed)
                    {
                        displayTimer = 0;
                        currentChar++;
                        if (currentChar >= 0)
                            targetText.text += displayString[currentChar];
                    }
                }
                else
                {
                    isAnimating = false;
                    onTextAnimationDone?.Invoke(); // <- Aqui dispara o evento apenas uma vez
                }
            }
        }

        public void AnimateText(string text)
        {
            targetText.text = "";
            displayString = text;
            displayTimer = 0f;
            currentChar = -1;
            isAnimating = true;
        }
    }
}
