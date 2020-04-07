using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class DTOModel
    {
        public int ID { get; set; }

        public DateTime OrderTime { get; set; }

        public string Order { get; set; }

        public List<string> Names { get; set; }
    }


    public class DTOModel2
    {
        public string ID { get; set; }

    }
}
