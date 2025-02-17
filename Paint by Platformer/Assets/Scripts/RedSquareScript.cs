using Unity.VisualScripting;
using UnityEngine;

public class RedSquareScript : MonoBehaviour
{
    public int currentColor;
    GameObject myPlayer = GameObject.Find("Goob_0");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScriptReference someScript = GameObject.Find("Goob_0").GetComponent<ScriptReference>();
    }
}
