using UnityEngine.UIElements;

public class RowColumnGrid : VisualElement
{
    private int height;
    private int width;
    
    public readonly RowColumnLabel[,] labels;
    
    public RowColumnGrid() : this(5, 4) { }

    public RowColumnGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
        labels = new RowColumnLabel[height, width];
        
        for (int y = 0; y < height; y++)
        {
            var ve = new VisualElement();
            ve.AddToClassList("row");
            ve.AddToClassList("expand");
            for (int x = 0; x < width; x++)
            {
                labels[y, x] = new RowColumnLabel
                {
                    focusable = true,
                    Y = y,
                    X = x
                };
                labels[y, x].AddToClassList("expand");
                labels[y, x].AddToClassList("borderless");
                labels[y, x].AddToClassList("grid-label");
                ve.Add(labels[y, x]);
            }
            Add(ve);
        }
    }
    
    public new class UxmlFactory : UxmlFactory<RowColumnGrid, UxmlTraits> { }
}