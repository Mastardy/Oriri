using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class Picker : VisualElement
{
    public struct ValueChangedEvent
    {
        public ValueChangedEvent(Picker picker, int oldValue, int newValue)
        {
            this.picker = picker;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    
        public Picker picker;
        public int oldValue;
        public int newValue;
    }
    
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

    public event Action<ValueChangedEvent> onValueChanged;
    
    private int index;
    public int Index
    {
        get => index;
        set
        {
            if (value < 0) value = choices.Count - 1;
            if (value > choices.Count - 1) value = 0;
            if (index == value) return;
            onValueChanged?.Invoke(new ValueChangedEvent(this, index, value));
            index = value;
            CenterLabel.text = choices[index];
        }
    }

    private static readonly string ussClassName = "mastardy-picker";
    private static readonly string titleUssClassName = ussClassName + "-title";
    private static readonly string labelUssClassName = ussClassName + "-label";
    private static readonly string buttonUssClassName = ussClassName + "-button";
    
    public Button LeftButton { get; }
    public Label CenterLabel { get; }
    public Button RightButton { get; }

    public Picker()
    {
        AddToClassList(ussClassName);
        focusable = true;

        LeftButton = new Button()
        {
            name = "leftButton",
            focusable = false,
            text = "<"
        };
        LeftButton.AddToClassList(buttonUssClassName);
        LeftButton.RegisterCallback<ClickEvent>(PreviousValue);
        Add(LeftButton);

        CenterLabel = new Label()
        {
            name = "label"
        };
        CenterLabel.AddToClassList(labelUssClassName);
        Add(CenterLabel);
        
        RightButton = new Button()
        {
            name = "rightButton",
            focusable = false,
            text = ">"
        };
        RightButton.AddToClassList(buttonUssClassName);
        RightButton.RegisterCallback<ClickEvent>(NextValue);
        Add(RightButton);
        
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
