using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image sprite;
    [Tooltip("Wait how much time before triggering the event")]
    [SerializeField] private float fadeTimer;
    [SerializeField] private bool useTween = false;
    public UnityEvent onFadeBegin;
    public UnityEvent onFadeEnd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void StartFade()
    {
        
    }
}
