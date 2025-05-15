using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DIalogManager : Singleton<DIalogManager>
{ 
    [Header("Config")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcdialogTMP;

    public NPCinteraction npcSelected {  get; set; }

    private bool dialogStarted;
    private PlayerAction actions;
    private Queue<string> dialogQueue = new Queue<string>();

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerAction();
    }

    private void Start()
    {
        actions.Dialogue.Interact.performed += ctx => ShowDialog();
        actions.Dialogue.Continue.performed += ctx => ContinueDialog();
    }

    private void LoadDialogFromNPC()
    {
        if (npcSelected.DialogToShow.dialogue.Length <= 0) return;
        foreach (string sentence in npcSelected.DialogToShow.dialogue)
        {
           dialogQueue.Enqueue(sentence);
        }
    }    
     private void ShowDialog()
    {
        if (npcSelected == null) return;
        if (dialogStarted) return;
        dialogPanel.SetActive(true);
        LoadDialogFromNPC();
        npcIcon.sprite = npcSelected.DialogToShow.Icon;
        npcNameTMP.text = npcSelected.DialogToShow.Name;
        npcdialogTMP.text = npcSelected.DialogToShow.Greeting;
        dialogStarted = true;
    }   
    
    public void CloseDialogPanel()
    {
        dialogPanel.SetActive(false);
        dialogStarted = false;
        dialogQueue.Clear();

    }
    private void ContinueDialog()
    {
        if (npcSelected == null)
        {
            dialogQueue.Clear();
            return;
        }
        if (dialogQueue.Count <= 0)
        {
            CloseDialogPanel();
            dialogStarted = false;
            return;
        }

        npcdialogTMP.text = dialogQueue.Dequeue();

    }    

    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }

}
