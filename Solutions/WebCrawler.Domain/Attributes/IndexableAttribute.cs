using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Attributes
{
    public class IndexableAttribute : Attribute
    {
        private readonly string _name;

        public IndexableAttribute(string name) { _name = name; }

        public string GetName() { return _name; }
    }
}
