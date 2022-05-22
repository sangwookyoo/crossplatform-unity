using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Controller")]
    public GameObject uiController;

    [Header("Safe Area")]
    [Tooltip("For Notch Design")]
    public GameObject safeAreaObject;

    [Header("Panel View")]
    public Button changeViewButton;

    [Header("Panel Clock")]
    public Text clockText;

    [Header("Panel UserInfo")]
    public Transform userInfoContent;
    public GameObject userInfoObject;

    [Header("Panel Screen Message")]
    public Transform screenContent;
    public GameObject screenObject;

    [Header("Panel System Message")]
    public Transform systemContent;
    public GameObject systemObject;

    [Header("Panel Debug")]
    public Button button;
    public Button button01;
    public Button button02;
    public Text localText;


    // Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this);
        }
        
        SetUIController();
        SetSafeArea();
    }

    void Start()
    {
        localText.text = Localization.Instance.GetMessage(0);
    }

    void SetUIController()
    {
        if (uiController == null) return;

        #if UNITY_ANDROID || UNITY_IOS
            uiController.SetActive(true);
        #endif

        // TODO: Check activeSelf
        #if UNITY_EDITOR || UNITY_STANDALONE
            uiController.SetActive(true);
        #endif
    }

    void SetSafeArea()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 newAnchorMin = safeArea.position;
        Vector2 newAnchorMax = safeArea.position + safeArea.size;
        newAnchorMin.x /= Screen.width;
        newAnchorMin.y /= Screen.height;
        newAnchorMax.x /= Screen.width;
        newAnchorMax.y /= Screen.height;

        RectTransform rect= safeAreaObject.GetComponent<RectTransform>();
        rect.anchorMin = newAnchorMin;
        rect.anchorMax = newAnchorMax;
    }

    // StartCoroutine(ScreenCoroutine("Msg", 5f));
    public IEnumerator ScreenCoroutine(string message, float duration)
    {
        GameObject screen = Instantiate(screenObject, screenContent) as GameObject;
        screen.transform.Find("Message").gameObject.GetComponent<Text>().text = message;
        GameObject.Destroy(screen, duration);
        yield return null;
    }

    // StartCoroutine(SystemCoroutine("Msg", 5f));
    public IEnumerator SystemCoroutine(string message, float duration)
    {
        GameObject system = Instantiate(systemObject, systemContent) as GameObject;
        Text msg = system.transform.Find("Message").gameObject.GetComponent<Text>();
        msg.text = message;

        while (msg.color.a > 0)
        {
            msg.color = new Color(msg.color.r, msg.color.g, msg.color.b, msg.color.a - (Time.deltaTime / duration));
            yield return null;
        }

        GameObject.Destroy(system, duration);
    }

    public IEnumerator FadeInCoroutine(GameObject targetObj, float duration)
    {
        Renderer renderer = targetObj.GetComponent<Renderer>();

        for (float f = duration; f <= 1; f += 0.1f)
        {
            Color color = renderer.material.color;
            color.a = f;
            renderer.material.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOutCoroutine(GameObject targetObj, float duration)
    {
        Renderer renderer = targetObj.GetComponent<Renderer>();

        for (float f = duration; f >= 0; f -= 0.1f)
        {
            Color color = renderer.material.color;
            color.a = f;
            renderer.material.color = color;
            yield return null;
        }
    }
}