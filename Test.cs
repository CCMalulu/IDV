using System;

public class ImageList
{
    private int length;
    private string[] fileDir;

    public ImageList(int length, string[] fileDir)
    {
        this.length = length;
        this.fileDir = fileDir;
    }

    public void add(string inputDir)
    {
        fileDir(length) = inputDir;
        length++;
    }

    public void removeEnd()
    {
        length--;
    }

    public void removeFirst()
    {
        for (int i = 0; i < length - 1; i++)
        {
            fileDir[i] = fileDir[i + 1];
        }
        length--;
    }
}
