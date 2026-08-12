using Core.Models.Enums;

namespace Core.Models
{
    public class ExtensionFilter
    {
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }
    }
}
