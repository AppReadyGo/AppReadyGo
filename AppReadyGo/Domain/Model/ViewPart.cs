using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    public class ViewPart
    {
        public virtual long Id { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime FinishDate { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual int Orientation { get; set; }
        public virtual PageView PageView { get; set; }

        public ViewPart()
        {
        }

        public ViewPart(PageView pageView, DateTime startDate, DateTime finishDate, int x, int y, int orientation)
        {
            this.StartDate = startDate;
            this.FinishDate = finishDate;
            this.X = x;
            this.Y = y;
            this.Orientation = orientation;
            this.PageView = pageView;

            this.PageView.AddViewPart(this);
        }
    }
}
