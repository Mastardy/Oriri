using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement root;

    private Button playButton;
    private Button optionsButton;
    private Button creditsButton;
    private Button quitButton;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("root");
        
        playButton = root.Q<Button>("play");
        optionsButton = root.Q<Button>("options");
        creditsButton = root.Q<Button>("credits");
        quitButton = root.Q<Button>("quit");

        playButton.Focus();
        
        playButton.clicked += Play;
        optionsButton.clicked += OptionsMenu;
        creditsButton.clicked += CreditsMenu;
        quitButton.clicked += Quit;
    }

    private void OnDisable()
    {
        playButton.clicked -= Play;
        optionsButton.clicked -= OptionsMenu;
        creditsButton.clicked -= CreditsMenu;
        quitButton.clicked -= Quit;
    }

    private void Play()
    {
        Debug.Log("Play Button");
    }

    private void OptionsMenu()
    {
        Debug.Log("Options Button");
    }

    private void CreditsMenu()
    {
        
    }

    private void Quit()
    {
        Application.Quit();
    }
}
