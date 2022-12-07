using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private VisualElement root;
    
    private Menu current;
    
    private Menu main;
    private Menu play;
    private Menu options;
    private Menu generalOptions;
    private Menu graphicsOptions;
    private Menu controlsOptions;
    private Menu credits;

    private Button playButton;
    private Button optionsButton;
    private Button generalButton;
    private Button graphicsButton;
    private Button controlsButton;
    private Button creditsButton;
    private Button quitButton;

    private List<Button> backButtons = new();
    private List<Button> optionsBackButtons = new();
    
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        main = root.Q<Menu>("main");
        play = root.Q<Menu>("play");
        options = root.Q<Menu>("options");
        generalOptions = root.Q<Menu>("generalOptions");
        graphicsOptions = root.Q<Menu>("graphicsOptions");
        controlsOptions = root.Q<Menu>("controlsOptions");
        credits = root.Q<Menu>("credits");
        
        playButton = main.Q<Button>("playButton");
        optionsButton = main.Q<Button>("optionsButton");
        generalButton = options.Q<Button>("generalButton");
        graphicsButton = options.Q<Button>("graphicsButton");
        controlsButton = options.Q<Button>("controlsButton");
        creditsButton = main.Q<Button>("creditsButton");
        quitButton = main.Q<Button>("quitButton");
        
        backButtons.Add(play.Q<Button>("back"));
        backButtons.Add(options.Q<Button>("back"));
        backButtons.Add(credits.Q<Button>("back"));
        
        optionsBackButtons.Add(generalOptions.Q<Button>("back"));
        optionsBackButtons.Add(graphicsOptions.Q<Button>("back"));
        optionsBackButtons.Add(controlsOptions.Q<Button>("back"));
        
        play.RegisterCallback<NavigationCancelEvent>(MainMenu);
        options.RegisterCallback<NavigationCancelEvent>(MainMenu);
        credits.RegisterCallback<NavigationCancelEvent>(MainMenu);
        
        generalOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        graphicsOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        controlsOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked += MainMenu;
        }
        
        for (int i = 0; i < optionsBackButtons.Count; i++)
        {
            optionsBackButtons[i].clicked += OptionsMenu;
        }

        playButton.clicked += PlayMenu;
        optionsButton.clicked += OptionsMenu;
        generalButton.clicked += GeneralOptionsMenu;
        graphicsButton.clicked += GraphicsOptionsMenu;
        controlsButton.clicked += ControlsOptionsMenu;
        creditsButton.clicked += CreditsMenu;
        quitButton.clicked += Quit;
        
        ChangeMenu(main);
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayMenu;
        optionsButton.clicked -= OptionsMenu;
        generalButton.clicked -= GeneralOptionsMenu;
        graphicsButton.clicked -= GraphicsOptionsMenu;
        controlsButton.clicked -= ControlsOptionsMenu;
        creditsButton.clicked -= CreditsMenu;
        quitButton.clicked -= Quit;
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked -= MainMenu;
        }

        for (int i = 0; i < optionsBackButtons.Count; i++)
        {
            optionsBackButtons[i].clicked -= OptionsMenu;
        }
        
        play.UnregisterCallback<NavigationCancelEvent>(MainMenu);
        options.UnregisterCallback<NavigationCancelEvent>(MainMenu);
        credits.UnregisterCallback<NavigationCancelEvent>(MainMenu);
        generalOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
        graphicsOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
        controlsOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
    }

    private void MainMenu(NavigationCancelEvent _) => MainMenu();
    private void MainMenu() => ChangeMenu(main);
    
    private void PlayMenu() => ChangeMenu(play);
    
    private void OptionsMenu(NavigationCancelEvent _) => OptionsMenu();
    private void OptionsMenu() => ChangeMenu(options);
    
    private void GeneralOptionsMenu() => ChangeMenu(generalOptions);
    private void GraphicsOptionsMenu() => ChangeMenu(graphicsOptions);
    private void ControlsOptionsMenu() => ChangeMenu(controlsOptions);
    
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
