using UnityEngine;
using UnityEngine.UIElements;

public partial class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        main = root.Q<Menu>("main");
        play = root.Q<Menu>("play");
        options = root.Q<Menu>("options");
        controlsOptions = root.Q<Menu>("controlsOptions");
        graphicsOptions = root.Q<Menu>("graphicsOptions");
        accessibilityOptions = root.Q<Menu>("accessibilityOptions");
        credits = root.Q<Menu>("credits");
        
        playButton = main.Q<Button>("playButton");
        optionsButton = main.Q<Button>("optionsButton");
        controlsButton = options.Q<Button>("controlsButton");
        graphicsButton = options.Q<Button>("graphicsButton");
        accessibilityButton = options.Q<Button>("accessibilityButton");
        creditsButton = main.Q<Button>("creditsButton");
        quitButton = main.Q<Button>("quitButton");
        
        backButtons.Add(play.Q<Button>("back"));
        backButtons.Add(options.Q<Button>("back"));
        backButtons.Add(credits.Q<Button>("back"));
        
        optionsBackButtons.Add(controlsOptions.Q<Button>("back"));
        optionsBackButtons.Add(graphicsOptions.Q<Button>("back"));
        optionsBackButtons.Add(accessibilityOptions.Q<Button>("back"));

        resolutionPicker = graphicsOptions.Q<Picker>("resolutionPicker");

        play.RegisterCallback<NavigationCancelEvent>(RootMenu);
        options.RegisterCallback<NavigationCancelEvent>(RootMenu);
        credits.RegisterCallback<NavigationCancelEvent>(RootMenu);
        
        accessibilityOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        graphicsOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        controlsOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked += RootMenu;
        }
        
        for (int i = 0; i < optionsBackButtons.Count; i++)
        {
            optionsBackButtons[i].clicked += OptionsMenu;
        }

        playButton.clicked += PlayMenu;
        playButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        optionsButton.clicked += OptionsMenu;
        optionsButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        controlsButton.clicked += ControlsOptionsMenu;
        controlsButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        graphicsButton.clicked += GraphicsOptionsMenu;
        graphicsButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        accessibilityButton.clicked += AccessibilityOptionsMenu;
        accessibilityButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        creditsButton.clicked += CreditsMenu;
        creditsButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        quitButton.clicked += Quit;
        quitButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        
        FillPickers();
        ChangeMenu(main);
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayMenu;
        optionsButton.clicked -= OptionsMenu;
        controlsButton.clicked -= ControlsOptionsMenu;
        graphicsButton.clicked -= GraphicsOptionsMenu;
        accessibilityButton.clicked -= AccessibilityOptionsMenu;
        creditsButton.clicked -= CreditsMenu;
        quitButton.clicked -= Quit;
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked -= RootMenu;
        }

        for (int i = 0; i < optionsBackButtons.Count; i++)
        {
            optionsBackButtons[i].clicked -= OptionsMenu;
        }
        
        play.UnregisterCallback<NavigationCancelEvent>(RootMenu);
        options.UnregisterCallback<NavigationCancelEvent>(RootMenu);
        credits.UnregisterCallback<NavigationCancelEvent>(RootMenu);
        controlsOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
        graphicsOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
        accessibilityOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
    }
}
