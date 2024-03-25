using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models.Shared
{
    public class Searchable : Attribute
    {
        public BaseValues.SearchType SearchType { get; set; }
    }

    public static class BaseValues
    {
        public enum SearchType
        {
            Contains,
            StartsWith,
            EndsWith,
            Equals
        }
    }
}
