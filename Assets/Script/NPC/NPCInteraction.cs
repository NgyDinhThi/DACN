using Unity.Cinemachine;
using UnityEngine;


public class NPCinteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private NPCdialog dialogToShow;
    [SerializeField] private GameObject interactionBox;

    public NPCdialog DialogToShow => dialogToShow;

    private bool dialogStarted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DIalogManager.instance.npcSelected = this;
            interactionBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DIalogManager.instance.npcSelected = null;
            DIalogManager.instance.CloseDialogPanel();
            interactionBox.SetActive(false);
        }
        
    }
}