using System;
using System.Windows.Forms;
using System.Collections.Generic;


public class PictureObjectSpec
{
    private int ID;
    private int index;
    private List<String> imageList = new List<string>();
    private OpenFileDialog fileDialog = new OpenFileDialog();
    private FolderBrowserDialog folderDialog = new FolderBrowserDialog();
    private PictureBox picBox = new PictureBox();
    private Label label = new Label();
   

    public PictureObjectSpec(int ID)
	{
        this.ID = ID;

        // Picture Box Section
        picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        picBox.Location = new System.Drawing.Point(0, 0);
        //picBox.Name = "pictureBox"+ID;
        picBox.Size = new System.Drawing.Size(0, 0);
        picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        picBox.TabIndex = 0;
        picBox.TabStop = false;
        picBox.MouseEnter += new System.EventHandler(this.MouseEnter);

        // Label Section
        label.AutoSize = true;
        label.BackColor = System.Drawing.Color.DimGray;
        label.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        label.ForeColor = System.Drawing.Color.WhiteSmoke;
        label.Location = new System.Drawing.Point(0, 0);
        label.Name = "label";
        label.Size = new System.Drawing.Size(47, 19);
        label.TabIndex = 4;
        label.Text = "0 / 0";
	}

    public void MouseEnter(object sender, EventArgs e){

    }
}
