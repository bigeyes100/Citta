﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Utils
{

    // 缩放接口
    public interface IScalable
    {
        void ChangeSize(int sizeL);
    }

    // 绘制接口
    public interface IDrawObject
    {
        // 判断是否鼠标在当前目标中
        bool PointInObject(Point p);
    }

    public interface IDragable
    {
        void ChangeLoc(float dx, float dy);
    }
}