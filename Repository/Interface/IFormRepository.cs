using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IFormRepository
    {
        bool AddToForm(FormListModel formData);
        List<FormModel> GetFormDetails(int userId);
        List<PropertyModel> GetPropertyDetails(int userId, int formId);
    }
}
