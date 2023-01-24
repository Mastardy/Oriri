using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public partial class MainMenu
{
    private Menu current;

    private Volume colorBlindVolume;
    [SerializeField] private VolumeProfile None;
    [SerializeField] private VolumeProfile Protanopia;
    [SerializeField] private VolumeProfile Deuteranopia;
    [SerializeField] private VolumeProfile Tritanopia;
    
    private Options opt = new();

    private void Awake()
    {
        colorBlindVolume = Instantiate(new GameObject("Color Blind Volume")).AddComponent<Volume>();
        colorBlindVolume.isGlobal = true;
        colorBlindVolume.profile = None;
        DontDestroyOnLoad(colorBlindVolume);
    }

    private void RootMenu(NavigationCancelEvent _) => RootMenu();
    private void RootMenu() => ChangeMenu(main);
    
    private void PlayMenu() => ChangeMenu(play);
    private void Tutorial() => SceneManager.LoadScene(1);
    private void NewGame() => SceneManager.LoadScene(2);
    
    private void OptionsMenu(NavigationCancelEvent _) => OptionsMenu();
    private void OptionsMenu() => ChangeMenu(options);
    
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

        opt.Load();
        
        opt.resolution = opt.resolution == -1 ? resolutionIndex : 
            opt.resolution >= Screen.resolutions.Length ? resolutionIndex : opt.resolution;
        opt.displayMode = opt.displayMode == -1 ? 2 : opt.displayMode;
        opt.quality = opt.quality == -1 ? 2 : opt.quality;
        opt.colorBlindness = opt.colorBlindness == -1 ? 0 : opt.colorBlindness;
        
        resolutionPicker.Choices = resolutions;
        resolutionPicker.Index = opt.resolution;
      
        displayModePicker.Choices = new List<string>()
        {
            "Windowed",
            "Borderless",
            "Fullscreen"
        };
        displayModePicker.Index = opt.displayMode;
        
        qualityPicker.Choices = new List<string>()
        {
            "Low",
            "Medium",
            "High"
        };
        qualityPicker.Index = opt.quality;

        colorBlindPicker.Choices = new List<string>()
        {
            "None",
            "Protanopia",
            "Deuteranopia",
            "Tritanopia"
        };
        colorBlindPicker.Index = opt.colorBlindness;
        
        ApplyOptions();
    }

    private void OptionsValueChanged(Picker.ValueChangedEvent evt)
    {
        if (evt.picker == resolutionPicker) opt.resolution = evt.newValue;
        else if (evt.picker == displayModePicker) opt.displayMode = evt.newValue;
        else if (evt.picker == qualityPicker) opt.quality = evt.newValue;
        else if (evt.picker == colorBlindPicker) opt.colorBlindness = evt.newValue;
        
        ApplyOptions();
    }

    private void ApplyOptions()
    {
        var resolution = Screen.resolutions[opt.resolution];
        FullScreenMode fullscreenMode = opt.displayMode switch
        {
            0 => FullScreenMode.Windowed,
            1 => FullScreenMode.FullScreenWindow,
            2 => FullScreenMode.ExclusiveFullScreen,
            _ => throw new ArgumentOutOfRangeException()
        };
        Screen.SetResolution(resolution.width, resolution.height, fullscreenMode);
        QualitySettings.SetQualityLevel(opt.quality);

        switch (opt.colorBlindness)
        {
            case 1:
                colorBlindVolume.profile = Protanopia;
                break;
            case 2:
                colorBlindVolume.profile = Deuteranopia;
                break;
            case 3:
                colorBlindVolume.profile = Tritanopia;
                break;
            default:
                colorBlindVolume.profile = None;
                break;
        }
        
        opt.Save();
    }
    
    private void FocusOnHover(MouseEnterEvent evt) => (evt.target as Button)?.Focus();
}
