using AxMapWinGIS;
using MapWinGIS;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generalizer
{
    public partial class Form1 : Form
    {
        enum CurrentLayer
        {
            OriginalLayer,
            GeneralizedLayer,
            BothLayers
        }
        private readonly Shapefile originalShapefile;
        private readonly Shapefile generalizedShapefile;

        private int originalLayer;
        private int generalizedLayer;
        CurrentLayer currentLayer;

        private float divider;

        public Form1()
        {
            InitializeComponent();
            originalButton.Enabled = false;
            generalizedButton.Enabled = false;
            bothButton.Enabled = false;
            trackBar1.Enabled = false;
            zoomButton.Enabled = false;
            moveButton.Enabled = false;
            trackBar2.Enabled = false;

            originalShapefile = new Shapefile();
            generalizedShapefile = new Shapefile();

            divider = 10000f;
            dividerLabel.Text = divider.ToString();

            //axMap1.Projection = MapWinGIS.tkMapProjection.PROJECTION_GOOGLE_MERCATOR;
            //axMap1.TileProvider = MapWinGIS.tkTileProvider.OpenStreetMap;
            //axMap1.KnownExtents = MapWinGIS.tkKnownExtents.kePoland;
        }

        private void OriginalButton_Click(object sender, EventArgs e)
        {
            axMap1.set_LayerVisible(originalLayer, true);
            axMap1.set_LayerVisible(generalizedLayer, false);
            axMap1.ZoomToLayer(originalLayer);
            viewLabel.Text = "Original";
            currentLayer = CurrentLayer.OriginalLayer;
        }

        private void GeneralizedButton_Click(object sender, EventArgs e)
        {
            axMap1.set_LayerVisible(originalLayer, false);
            axMap1.set_LayerVisible(generalizedLayer, true);
            axMap1.ZoomToLayer(generalizedLayer);
            viewLabel.Text = "Generalized";
            currentLayer = CurrentLayer.GeneralizedLayer;
        }

        private void BothButton_Click(object sender, EventArgs e)
        {
            axMap1.set_LayerVisible(originalLayer, true);
            axMap1.set_LayerVisible(generalizedLayer, true);
            axMap1.ZoomToLayer(generalizedLayer);
            viewLabel.Text = "Both";
            currentLayer = CurrentLayer.BothLayers;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            Program.corridorWidth = (trackBar1.Value / divider);
            trackBarLabel.Text = Program.corridorWidth.ToString();

            axMap1.RemoveAllLayers();
            Program.GeneralizeFile();

            AddLayers(false);
        }

        private void AddLayers(bool firstRun)
        {
            if (originalShapefile.Open(Program.inputFile, null))
            {
                originalLayer = axMap1.AddLayer(originalShapefile, true);
                originalButton.Enabled = true;

                if (generalizedShapefile.Open(Program.outputFileName, null))
                {
                    generalizedLayer = axMap1.AddLayer(generalizedShapefile, true);
                    generalizedButton.Enabled = true;
                    bothButton.Enabled = true;
                    trackBar1.Enabled = true;
                    zoomButton.Enabled = true;
                    moveButton.Enabled = true;
                    trackBar2.Enabled = true;
                }
            }

            if (firstRun == true)
            {
                viewLabel.Text = "Original";
                currentLayer = CurrentLayer.OriginalLayer;
                axMap1.ZoomToLayer(originalLayer);
            }

            switch (currentLayer)
            {
                case CurrentLayer.OriginalLayer:
                    axMap1.set_LayerVisible(originalLayer, true);
                    axMap1.set_LayerVisible(generalizedLayer, false);
                    break;
                case CurrentLayer.GeneralizedLayer:
                    axMap1.set_LayerVisible(originalLayer, false);
                    axMap1.set_LayerVisible(generalizedLayer, true);
                    break;
                case CurrentLayer.BothLayers:
                    axMap1.set_LayerVisible(originalLayer, true);
                    axMap1.set_LayerVisible(generalizedLayer, true);
                    break;
            }

        }

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            using (openFileDialog1)
            {
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.InitialDirectory = "C:\\Users\\Dominik\\Documents\\geometry-generalize\\input";
                openFileDialog1.Filter = "shp files (*.shp)|*.shp";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.inputFile = openFileDialog1.FileName;

                }
            }

            //generalize file
            Program.GeneralizeFile();

            AddLayers(true);
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmZoomIn;
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmPan;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            switch(trackBar2.Value)
            {
                case 5:
                    divider = 1f;
                    break;
                case 4:
                    divider = 10f;
                    break;
                case 3:
                    divider = 100f;
                    break;
                case 2:
                    divider = 1000f;
                    break;
                case 1:
                    divider = 10000f;
                    break;
            }
            
            dividerLabel.Text = divider.ToString();

            Program.corridorWidth = (trackBar1.Value / divider);
            trackBarLabel.Text = Program.corridorWidth.ToString();

            axMap1.RemoveAllLayers();
            Program.GeneralizeFile();

            AddLayers(false);
        }
    }
}
