using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FormListModel
    {
        public string Reason { get; set; }
        public int UserId { get; set; }
        public List<PropertyModel> propertyList { get; set; }

    }
}
