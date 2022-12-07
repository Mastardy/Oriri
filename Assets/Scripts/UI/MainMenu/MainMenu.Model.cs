using UnityEngine;
using UnityEngine.UIElements;

public partial class MainMenu
{
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
}
