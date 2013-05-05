using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Domain.Model
{
    /// <summary>
    /// this class represents Click data model 
    /// each instance of this class contains base information about one single click (touch)
    /// </summary>
    public class Click
    {
        public virtual long Id { get; protected set; }

        public virtual DateTime Date { get; protected set; }

        public virtual int X { get; protected set; }

        public virtual int Y { get; protected set; }

        public virtual int Orientation { get; protected set; }

        public virtual PageView PageView { get; protected set; }

        public Click()
        {
        }

        public Click(PageView pageView, DateTime date, int x, int y, int orientation)
        {
            this.PageView = pageView;
            this.Date = date;
            this.X = x;
            this.Y = y;
            this.Orientation = orientation;

            this.PageView.AddClick(this);
        }
    }
}
