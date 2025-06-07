using System.Collections;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField, TextArea(1,5)] private string[] phrases;
    [SerializeField] private float timeBetweenLetters;
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private bool speaking = false;
    private int currentIndex = -1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Interact()
    {
        dialogueFrame.SetActive(true);
        if (!speaking)
        {
            NextPhrase();
        }
        else
        {
            CompletePhrase();
        }
    }

    IEnumerator WritePhrase()
    {
        speaking = true;
        dialogueText.text = "";

        // Subdividir la frase actual en caracteres
        char[] phraseCharacters = phrases[currentIndex].ToCharArray();
        foreach(char character in phraseCharacters)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(timeBetweenLetters);
        }

        speaking = false;
    }

    private void CompletePhrase()
    {
        StopAllCoroutines();
        dialogueText.text = phrases[currentIndex];
        speaking = false;
    }

    private void NextPhrase()
    {
        currentIndex++;
        if (currentIndex >= phrases.Length)
        {
            FinishDialogue();
        }
        else
        {
            StartCoroutine(WritePhrase());
        }
    }

    private void FinishDialogue()
    {
        speaking = false;
        dialogueText.text = "";
        currentIndex = -1;
        dialogueFrame.SetActive(false);
    }
}
