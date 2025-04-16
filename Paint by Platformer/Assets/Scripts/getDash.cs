using UnityEngine;
using UnityEngine.EventSystems;
public class getDash : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject selectedButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player"){
            GameObject player=collision.gameObject;
            PlayerMovement playerScript=player.GetComponent<PlayerMovement>();
            if(playerScript){
                playerScript.canDash=true;
                PlayerPrefs.SetInt("DashUnlocked", 1);
                Destroy(gameObject);
            }
            
        }
    }
    private void OnDestroy()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true); // Show the popup
            EventSystem.current.SetSelectedGameObject(null); // Clear previous selection
            EventSystem.current.SetSelectedGameObject(selectedButton); 
        }
    }
}
