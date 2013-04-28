using AppReadyGo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppReadyGo.Core.QueryResults.Application
{
    public class PublishDetailsResult
    {
        public int Id { get; set; }

        public AgeRange? AgeRange { get; set; }

        public Gender? Gender { get; set; }

        public Tuple<int, string> Country { get; set; }

        public string Zip { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
