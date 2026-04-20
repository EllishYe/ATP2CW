using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryboardController : MonoBehaviour
{
    [Header("Storyboard Pages")]
    public GameObject[] pages;  

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] voiceClips;  

    [Header("UI")]
    public Button continueButton;

    [Header("Scene Transition")]
    public SceneField sceneFrom;
    public SceneField sceneTo;

    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClicked);
        }
    }

    void ShowPage(int pageIndex)
    {
        // Hide all pages first
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
                pages[i].SetActive(i == pageIndex);
        }

        // Stop current voiceover
        if (audioSource != null)
        {
            audioSource.Stop();

            // Play new voiceover if available
            if (pageIndex < voiceClips.Length && voiceClips[pageIndex] != null)
            {
                audioSource.clip = voiceClips[pageIndex];
                audioSource.Play();
            }
        }
    }

    public void OnContinueClicked()
    {
        // Stop current voiceover
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // If not on the last page, go to next page
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
        else
        {
            // Last page -> load next scene
            SceneTransitionManager.Instance.Transition(sceneFrom, sceneTo);
        }
    }
}