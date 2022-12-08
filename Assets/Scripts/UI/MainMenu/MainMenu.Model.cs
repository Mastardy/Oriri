using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MainMenu
{
    private Menu current;
    
    private void RootMenu(NavigationCancelEvent _) => RootMenu();
    private void RootMenu() => ChangeMenu(main);
    
    private void PlayMenu() => ChangeMenu(play);
    
    private void OptionsMenu(NavigationCancelEvent _) => OptionsMenu();
    private void OptionsMenu() => ChangeMenu(options);
    
    private void ControlsOptionsMenu() => ChangeMenu(controlsOptions);
    private void GraphicsOptionsMenu() => ChangeMenu(graphicsOptions);
    private void AccessibilityOptionsMenu() => ChangeMenu(accessibilityOptions);
    
    private void CreditsMenu() => ChangeMenu(credits);
    
    private void Quit() => Application.Quit();

    private void ChangeMenu(Menu menu)
    {
        if (current != null) current.AddToClassList("hidden");
        current = menu;
        current.RemoveFromClassList("hidden");
        current.Q<VisualElement>(current.firstFocus).Focus();
    }

    private void FillPickers()
    {
        var resolutions = new List<string>();
        int resolutionIndex = 0;
        
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var resolution = Screen.resolutions[i];
            resolutions.Add(resolution.width + " x " + resolution.height);
            if (resolution.Equals(Screen.currentResolution)) resolutionIndex = i;
        }
        
        resolutionPicker.Choices = resolutions;
        resolutionPicker.Index = resolutionIndex;

        displayModePicker.Choices = new List<string>()
        {
            "Windowed",
            "Borderless",
            "Fullscreen"
        };
        displayModePicker.Index = 2;

        qualityPicker.Choices = new List<string>()
        {
            "Low",
            "Medium",
            "High"
        };
        qualityPicker.Index = 2;
    }

    private void OptionsValueChanged(Picker.ValueChangedEvent evt)
    {
        
    }
    
    private void FocusOnHover(MouseEnterEvent evt) => (evt.target as Button)?.Focus();
}
