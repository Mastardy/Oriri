using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RowColumn : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int promptAmount;
    [SerializeField] private int depth;

    public event Action GameCompleted;
    
    private int currentX;
    private int CurrentX
    {
        get => currentX;
        set => currentX = value < 0 ? width - 1 : value > width - 1 ? 0 : value;
    }

    private int currentY;
    private int CurrentY
    {
        get => currentY;
        set => currentY = value < 0 ? height - 1 : value > height - 1 ? 0 : value;
    }

    private VisualElement root;
    
    private RowColumnGrid boardGrid;
    private RowColumnPrompt[] prompts;

    private int[,] boardNumbers;
    
    private bool columnSelection = true;

    public bool beingPlayed;
    
    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root = root.Q<VisualElement>("root");
        root.style.flexDirection = FlexDirection.ColumnReverse;
    }

    public void GenerateGame()
    {
        if (beingPlayed) return;
        beingPlayed = true;
        GenerateBoard();
        GeneratePrompts();
    }
    
    private void GenerateBoard()
    {
        boardGrid = new RowColumnGrid(width, height);
        root.Add(boardGrid);
        
        boardNumbers = new int[height, width];
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                boardNumbers[y, x] = Random.Range(1, 10);
                boardGrid.labels[y, x].text = boardNumbers[y, x].ToString();
                boardGrid.labels[y, x].RegisterCallback<NavigationSubmitEvent>(OnClicked);
                boardGrid.labels[y, x].RegisterCallback<NavigationMoveEvent>(OnNavigationMove);
                boardGrid.labels[y, x].RegisterCallback<FocusEvent>(OnFocus);
            }
        }
        
        boardGrid.labels[0, 0].Focus();
    }

    private void GeneratePrompts()
    {
        prompts = new RowColumnPrompt[promptAmount];

        int lastX = 0;
        int lastY = 0;
        
        for (int i = 0; i < promptAmount; i++)
        {
            prompts[i] = new RowColumnPrompt(depth);

            for (int j = 0; j < depth; j++)
            {
                if (j % 2 == 0)
                {
                    int x = lastX;
                    while(lastX == x) x = Random.Range(0, width);
                    lastX = x;
                }
                else
                {
                    int y = lastY;
                    while (lastY == y) y = Random.Range(0, height);
                    lastY = y;   
                }

                prompts[i].prompt[j].text = boardNumbers[lastY, lastX].ToString();
            }
        }
        
        for(int i = promptAmount - 1; i >= 0; i--) root.Add(prompts[i]);    
    }
    
    private void OnNavigationMove(NavigationMoveEvent evt)
    {
        evt.PreventDefault();
        
        var label = evt.target as RowColumnLabel;
        if (label == null) return;
        
        if (columnSelection)
        {
            int x = label.X + (evt.move.x < 0 ? -1 : 1);
            CurrentX = x;
        }
        else
        {
            int y = label.Y + (evt.move.y < 0 ? 1 : -1);
            CurrentY = y;
        }
        
        boardGrid.labels[CurrentY, CurrentX].Focus();
    }
    
    private void OnClicked(NavigationSubmitEvent evt)
    {
        columnSelection = !columnSelection;
        var target = evt.target as RowColumnLabel;
        RecolorLabels(target);
        UpdatePrompt(target);
        RecolorPrompt(target);
    }

    private void OnFocus(FocusEvent evt)
    {
        var target = evt.target as RowColumnLabel;
        RecolorLabels(target);
        RecolorPrompt(target);
    }

    private void RecolorLabels(RowColumnLabel target)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                boardGrid.labels[y, x].RemoveFromClassList("label-selected");
                boardGrid.labels[y, x].RemoveFromClassList("label-current");
                boardGrid.labels[y, x].RemoveFromClassList("label-next");

                if (x == target.X && y == target.Y)
                {
                    target.AddToClassList("label-selected");
                    continue;
                }
                
                if (x == target.X)
                {
                    boardGrid.labels[y, x].AddToClassList(columnSelection ? "label-next" : "label-current");
                }
                else if (y == target.Y)
                {
                    boardGrid.labels[y, x].AddToClassList(columnSelection ? "label-current" : "label-next");
                }
            }
        }
    }

    private void RecolorPrompt(RowColumnLabel target)
    {
        for (int i = 0; i < promptAmount; i++)
        {
            if (prompts == null) continue;
            if (prompts[i].finished) continue;

            var prompt = prompts[i];
            var index = prompt.index;
            var promptLabel = prompt.prompt[index];

            for (int j = index; j < depth; j++)
            {
                prompt.prompt[j].RemoveFromClassList("label-next");
                prompt.prompt[j].RemoveFromClassList("label-current");
            }

            if(int.Parse(promptLabel.text) == int.Parse(target.text))
                promptLabel.AddToClassList("label-current");
        }
    }

    private void UpdatePrompt(RowColumnLabel target)
    {
        for (int i = 0; i < promptAmount; i++)
        {
            if (prompts == null) continue;
            if (prompts[i].finished) continue;
            var index = prompts[i].index;
            var prompt = prompts[i].prompt[index];
            prompt.RemoveFromClassList("label-current");
            if (int.Parse(prompt.text) == int.Parse(target.text))
            {
                prompt.AddToClassList("label-next");
                prompts[i].index++;
                if (prompts[i].index >= depth) prompts[i].finished = true;
                var finished = true;
                for (int j = 0; j < promptAmount; j++)
                {
                    if (!prompts[j].finished) finished = false;
                }

                if (finished)
                {
                    FinishGame();
                }
            }
            else
            {
                prompts[i].index = 0;
            }
        }
    }
    
    private void FinishGame()
    {
        GameCompleted?.Invoke();
        root.Clear();
        beingPlayed = false;
    }
}