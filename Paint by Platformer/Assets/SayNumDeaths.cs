using UnityEngine;

public class SayNumDeaths : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int numDeaths;
    public float timeInLevel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numDeaths = DeathManager.getDeaths();
        timeInLevel = DeathManager.getTime();
        
    }
}
