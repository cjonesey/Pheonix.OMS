using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models.Shared
{
    public class SearchModel
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public ConnectorType Connector { get; set; }
        public string Value { get; set; }
    }

    public enum ConnectorType
    {
        And = 0,
        Or = 1
    }
}
