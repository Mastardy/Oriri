using System.Collections.Generic;
using UnityEngine.UIElements;

public partial class MainMenu
{
    private VisualElement root;
    
    private Menu main;
    
    private Menu play;
    
    private Menu options;
    private Menu graphicsOptions;
    private Menu accessibilityOptions;
    
    private Menu credits;
    
    private Button playButton;
    private Button tutorialButton;
    private Button newGameButton;
    
    private Button optionsButton;
    private Button graphicsButton;
    private Button accessibilityButton;
    
    private Button creditsButton;
    
    private Button quitButton;
    
    private Picker resolutionPicker;
    private Picker displayModePicker;
    private Picker qualityPicker;

    private Picker colorBlindPicker;
 
    private List<Button> backButtons = new();
    private List<Button> optionsBackButtons = new();
}