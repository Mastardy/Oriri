[System.Serializable]
public class Options
{
    // Graphics
    public int resolution = -1;
    public int displayMode = -1;
    public int quality = -1;
    
    // Accessibility

    public void Load()
    {
        var temp = SaveManager.LoadData<Options>();
        resolution = temp.resolution;
        displayMode = temp.displayMode;
        quality = temp.quality;
    }
    
    public void Save()
    {
        SaveManager.SaveData(this);
    }
}