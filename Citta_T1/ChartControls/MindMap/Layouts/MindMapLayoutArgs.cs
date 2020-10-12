﻿using System;
using System.Drawing;
using System.Drawing.Printing;
using Citta_T1.Configuration;
using Citta_T1.Core;
using Citta_T1.Model.MindMaps;

namespace Citta_T1.Controls.MapViews
{
    public class MindMapLayoutArgs
    {
        public MindMapLayoutArgs()
        {
            ShowRemarkIcon = Options.Current.GetBool(Citta_T1.Configuration.OptionNames.Charts.ShowRemarkIcon);
            ShowLineArrowCap = Options.Current.GetBool(OptionNames.Charts.ShowLineArrowCap);
        }

        public MindMapLayoutArgs(Graphics graphics, MindMap chart, Font font)
            : this()
        {
            Graphics = graphics;
            Chart = chart;
            Font = font;
        }

        public MindMapLayoutArgs(MindMap chart, Font font)
        {
            Chart = chart;
            this.Font = font;
        }

        public MindMap Chart { get; private set; }

        public Graphics Graphics { get; private set; }

        public Font Font { get; private set; }

        public int ItemsSpace
        {
            get { return Chart.ItemsSpace; }// (int)Math.Ceiling(View.Map.Style.ItemsSpace * Zoom); }
        }

        public int LayerSpace
        {
            get { return Chart.LayerSpace; }// (int)Math.Ceiling(View.Map.Style.LayerSpace * Zoom); }
        }

        public bool ShowRemarkIcon { get; private set; }

        public bool ShowLineArrowCap { get; private set; }

        public int RemarkIconSize
        {
            get { return 16; }
        }
    }
}