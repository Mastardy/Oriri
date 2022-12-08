using System.Collections.Generic;
using UnityEngine.UIElements;

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
        Add(rightButton);
        
        RegisterCallback<NavigationMoveEvent>(OnNavigationMove);
    }

    private void OnNavigationMove(NavigationMoveEvent evt)
    {
        if (evt.move.x == 0) return;

        var tempIndex = Index;
        tempIndex += evt.move.x < 0 ? -1 : 1;
        Index = tempIndex < 0 ? choices.Count - 1 : tempIndex > choices.Count - 1 ? 0 : tempIndex;
    }
    
    public new class UxmlFactory : UxmlFactory<Picker, UxmlTraits> { }
}
