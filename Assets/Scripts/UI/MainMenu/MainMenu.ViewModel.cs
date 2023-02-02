using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public partial class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        main = root.Q<Menu>("main");
        
        play = root.Q<Menu>("play");
        
        options = root.Q<Menu>("options");
        graphicsOptions = root.Q<Menu>("graphicsOptions");
        accessibilityOptions = root.Q<Menu>("accessibilityOptions");
        
        credits = root.Q<Menu>("credits");
        
        playButton = main.Q<Button>("playButton");
        tutorialButton = play.Q<Button>("tutorialButton");
        newGameButton = play.Q<Button>("newGameButton");
        
        optionsButton = main.Q<Button>("optionsButton");
        graphicsButton = options.Q<Button>("graphicsButton");
        accessibilityButton = options.Q<Button>("accessibilityButton");
        
        creditsButton = main.Q<Button>("creditsButton");
        
        quitButton = main.Q<Button>("quitButton");

        resolutionPicker = graphicsOptions.Q<Picker>("resolutionPicker");
        displayModePicker = graphicsOptions.Q<Picker>("displayModePicker");
        qualityPicker = graphicsOptions.Q<Picker>("qualityPicker");
        colorBlindPicker = accessibilityOptions.Q<Picker>("colorBlindPicker");
        
        backButtons.Add(play.Q<Button>("back"));
        backButtons.Add(options.Q<Button>("back"));
        backButtons.Add(credits.Q<Button>("back"));
        
        optionsBackButtons.Add(graphicsOptions.Q<Button>("back"));
        optionsBackButtons.Add(accessibilityOptions.Q<Button>("back"));
        
        play.RegisterCallback<NavigationCancelEvent>(RootMenu);
        options.RegisterCallback<NavigationCancelEvent>(RootMenu);
        credits.RegisterCallback<NavigationCancelEvent>(RootMenu);
        
        accessibilityOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);
        graphicsOptions.RegisterCallback<NavigationCancelEvent>(OptionsMenu);

        resolutionPicker.onValueChanged += OptionsValueChanged;
        displayModePicker.onValueChanged += OptionsValueChanged;
        qualityPicker.onValueChanged += OptionsValueChanged;
        colorBlindPicker.onValueChanged += OptionsValueChanged;
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked += RootMenu;
            backButtons[i].RegisterCallback<MouseEnterEvent>(FocusOnHover);
        }
        
        for (int i = 0; i < optionsBackButtons.Count; i++)
        {
            optionsBackButtons[i].clicked += OptionsMenu;
            optionsBackButtons[i].RegisterCallback<MouseEnterEvent>(FocusOnHover);
        }
        
        resolutionPicker.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        displayModePicker.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        qualityPicker.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        colorBlindPicker.RegisterCallback<MouseEnterEvent>(FocusOnHover);

        playButton.clicked += PlayMenu;
        playButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        tutorialButton.clicked += Tutorial;
        tutorialButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        newGameButton.clicked += NewGame;
        newGameButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
        optionsButton.clicked += OptionsMenu;
        optionsButton.RegisterCallback<MouseEnterEvent>(FocusOnHover);
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
        resolutionPicker.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        displayModePicker.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        qualityPicker.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        colorBlindPicker.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        
        playButton.clicked -= PlayMenu;
        playButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        tutorialButton.clicked -= Tutorial;
        tutorialButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        newGameButton.clicked -= NewGame;
        newGameButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        optionsButton.clicked -= OptionsMenu;
        optionsButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        graphicsButton.clicked -= GraphicsOptionsMenu;
        graphicsButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        accessibilityButton.clicked -= AccessibilityOptionsMenu;
        accessibilityButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        creditsButton.clicked -= CreditsMenu;
        creditsButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        quitButton.clicked -= Quit;
        quitButton.UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        
        for (int i = 0; i < backButtons.Count; i++)
        {
            backButtons[i].clicked -= RootMenu;
            backButtons[i].UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        }
        
        for (int i = 0; i < optionsBackButtons.Count; i++)
        {
            optionsBackButtons[i].clicked -= OptionsMenu;
            optionsBackButtons[i].UnregisterCallback<MouseEnterEvent>(FocusOnHover);
        }
        
        resolutionPicker.onValueChanged -= OptionsValueChanged;
        displayModePicker.onValueChanged -= OptionsValueChanged;
        qualityPicker.onValueChanged -= OptionsValueChanged;
        colorBlindPicker.onValueChanged -= OptionsValueChanged;
        
        play.UnregisterCallback<NavigationCancelEvent>(RootMenu);
        options.UnregisterCallback<NavigationCancelEvent>(RootMenu);
        accessibilityOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
        graphicsOptions.UnregisterCallback<NavigationCancelEvent>(OptionsMenu);
        credits.UnregisterCallback<NavigationCancelEvent>(RootMenu);
    }

    private void Update()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            var gamepad = Gamepad.all[i];
            if (gamepad.leftStick.x.ReadValue() > 0.5f) Debug.Log(gamepad.deviceId);
        }
    }
}
