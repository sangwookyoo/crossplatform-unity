using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    private Text _nickName;

    void Start()
    {
        _nickName = GetComponent<Text>();
        //_nickName.text = ;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}