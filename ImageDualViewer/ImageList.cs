using System;

public class ImageList
{
    public int size;
    public string[] fileDir = new string[1000];

    public ImageList(int size, string[] fileDir)
    {
        this.size = size;
        this.fileDir = fileDir;
    }

    public ImageList()
    {
        this.size = 0;
    }

    public void Add(string inputDir)
    {
        fileDir[size] = inputDir;
        size++;
    }

    public void RemoveEnd()
    {
        size--;
    }

    public void RemoveFirst()
    {
        for (int i = 0; i < size - 1; i++)
        {
            fileDir[i] = fileDir[i + 1];
        }
        size--;
    }

    public void ResetArray()
    {
        try
        {
            Array.Clear(fileDir, 0, size);
            size = 0;
        }
        catch
        {
        }
    }
}
