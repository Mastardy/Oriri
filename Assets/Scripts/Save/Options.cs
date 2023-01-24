[System.Serializable]
public class Options
{
    // Graphics
    public int resolution = -1;
    public int displayMode = -1;
    public int quality = -1;
    
    // Accessibility
    public int colorBlindness = -1;

    public void Load()
    {
        var temp = SaveManager.LoadData<Options>();
        resolution = temp.resolution;
        displayMode = temp.displayMode;
        quality = temp.quality;
        colorBlindness = temp.colorBlindness;
    }
    
    public void Save()
    {
        SaveManager.SaveData(this);
    }
}