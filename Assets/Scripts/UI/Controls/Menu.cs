using UnityEngine.UIElements;

public class Menu : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Menu, UxmlTraits> { }
    
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        private UxmlStringAttributeDescription propertyPath;
        
        public UxmlTraits() => propertyPath = new UxmlStringAttributeDescription { name = "first-focus" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        { 
            base.Init(ve, bag, cc);
            var valueFromBag = propertyPath.GetValueFromBag(bag, cc);
            if (string.IsNullOrEmpty(valueFromBag) || !(ve is Menu menu)) return;
            menu.firstFocus = valueFromBag;
        }
    }
    
    public string firstFocus { get; set; }
}