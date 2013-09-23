using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Domain.Model
{
    public class ControlClick
    {
        public virtual long Id { get; protected set; }

        public virtual DateTime Date { get; protected set; }

        public virtual String Tag { get; protected set; }

        public virtual PageView PageView { get; protected set; }


        public ControlClick(PageView pageView, DateTime date, String tag)
        {
            this.PageView = pageView;
            this.Date = date;
            this.Tag = tag;

            this.PageView.AddControlClick(this);
        }

        public ControlClick()
        {
            
        }

    }
}
