using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WildernessOdyssey.Models
{
    public class ChartViewModel
    {

           public ChartViewModel(string x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            //Explicitly setting the name to be used while serializing to JSON.

            public string X = string.Empty;

            //Explicitly setting the name to be used while serializing to JSON.

            public Nullable<double> Y = null;
        
    }
}