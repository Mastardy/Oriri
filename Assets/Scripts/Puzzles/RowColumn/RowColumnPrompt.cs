using UnityEngine.UIElements;

public class RowColumnPrompt : VisualElement
{
    public readonly Label[] prompt;
    public int index;
    public bool finished;

    public RowColumnPrompt() : this(6) { }

    public RowColumnPrompt(int depth)
    {
        prompt = new Label[depth];
        AddToClassList("row");
        for (int i = 0; i < depth; i++)
        {
            prompt[i] = new Label();
            prompt[i].AddToClassList("prompt-label");
            Add(prompt[i]);
        }
    }

    public new class UxmlFactory : UxmlFactory<RowColumnPrompt, UxmlTraits> { }
}