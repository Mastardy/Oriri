using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject eventSystemGO;
    
    private VisualElement root;

    private Menu current;
    
    private Menu main;
    private Menu play;
    private Menu options;
    private Menu credits;

    private Button playButton;
    private Button optionsButton;
    private Button creditsButton;
    private Button quitButton;

    private List<Button> backButtons = new();
    
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        main = root.Q<Menu>("main");
        play = root.Q<Menu>("play");
        options = root.Q<Menu>("options");
        credits = root.Q<Menu>("credits");
        
        playButton = main.Q<Button>("playButton");
        optionsButton = main.Q<Button>("optionsButton");
        creditsButton = main.Q<Button>("creditsButton");
        quitButton = main.Q<Button>("quitButton");
        
        backButtons.Add(play.Q<Button>("back"));
        backButtons.Add(options.Q<Button>("back"));
        backButtons.Add(credits.Q<Button>("back"));
        
        play.RegisterCallback<NavigationCancelEvent>(MainMenu);
        options.RegisterCallback<NavigationCancelEvent>(MainMenu);
        credits.RegisterCallback<NavigationCancelEvent>(MainMenu);

        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked += (MainMenu);
        }
        
        playButton.clicked += PlayMenu;
        optionsButton.clicked += OptionsMenu;
        creditsButton.clicked += CreditsMenu;
        quitButton.clicked += Quit;
        
        ChangeMenu(main);
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayMenu;
        optionsButton.clicked -= OptionsMenu;
        creditsButton.clicked -= CreditsMenu;
        quitButton.clicked -= Quit;
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked -= (MainMenu);
        }
        
        play.UnregisterCallback<NavigationCancelEvent>(MainMenu);
        options.UnregisterCallback<NavigationCancelEvent>(MainMenu);
        credits.UnregisterCallback<NavigationCancelEvent>(MainMenu);
    }

    private void MainMenu(NavigationCancelEvent _) => MainMenu();

    private void MainMenu() => ChangeMenu(main);
    
    private void PlayMenu() => ChangeMenu(play);

    private void OptionsMenu() => ChangeMenu(options);

    private void CreditsMenu() => ChangeMenu(credits);
    
    private void Quit() => Application.Quit();

    private void ChangeMenu(Menu menu)
    {
        if (current != null) current.AddToClassList("hidden");
        current = menu;
        current.RemoveFromClassList("hidden");
        current.Q<VisualElement>(current.firstFocus).Focus();
    }
}
