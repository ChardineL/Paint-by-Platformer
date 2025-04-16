using UnityEngine;

public class Commander : MonoBehaviour
{
    public bool facingRight;
    [SerializeField] public PlayerMovement player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = player.FacingRight();
    }
    
    public bool FacingRight()
    {
        return facingRight;
    }
}
