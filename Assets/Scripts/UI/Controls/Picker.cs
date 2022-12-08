using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class Picker : VisualElement
{
    private List<string> choices;
    public List<string> Choices
    {
        get => choices;
        set
        {
            choices = value;
            Index = 0;
        }
    }

    private DropdownField asdf;

    private int index;
    public int Index
    {
        get => index;
        set
        {
            if (value < 0) value = choices.Count - 1;
            if (value > choices.Count - 1) value = 0;
            index = value;
            centerLabel.text = choices[index];
        }
    }

    private static readonly string ussClassName = "mastardy-picker";
    private static readonly string titleUssClassName = ussClassName + "-title";
    private static readonly string labelUssClassName = ussClassName + "-label";
    private static readonly string buttonUssClassName = ussClassName + "-button";
    
    public Button leftButton { get; }
    public Label centerLabel { get; }
    public Button rightButton { get; }

    public Picker()
    {
        AddToClassList(ussClassName);
        focusable = true;

        leftButton = new Button()
        {
            name = "leftButton",
            focusable = false,
            text = "<"
        };
        leftButton.AddToClassList(buttonUssClassName);
        leftButton.RegisterCallback<ClickEvent>(PreviousValue);
        Add(leftButton);

        centerLabel = new Label()
        {
            name = "label"
        };
        centerLabel.AddToClassList(labelUssClassName);
        Add(centerLabel);
        
        rightButton = new Button()
        {
            name = "rightButton",
            focusable = false,
            text = ">"
        };
        rightButton.AddToClassList(buttonUssClassName);
        rightButton.RegisterCallback<ClickEvent>(NextValue);
        Add(rightButton);
        
        RegisterCallback<NavigationMoveEvent>(OnNavigationMove);
    }

    private void OnNavigationMove(NavigationMoveEvent evt)
    {
        if (Mathf.Abs(evt.move.x) < 0.1f) return;
        Index += evt.move.x < 0 ? -1 : 1;
    }

    private void PreviousValue(ClickEvent evt) => Index--;
    private void NextValue(ClickEvent evt) => Index++;
    
    public new class UxmlFactory : UxmlFactory<Picker, UxmlTraits> { }
}
