using UnityEngine.UIElements;

public class RowColumnLabel : Label
{
    public int X { get; set; }

    public int Y { get; set; }

    public new class UxmlFactory : UxmlFactory<RowColumnLabel, UxmlTraits> { }
}