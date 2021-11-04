using Models;
using System.Collections.Generic;

namespace Manager.Interface
{
    public interface IFormManager
    {
        bool AddToForm(FormListModel formData);
        List<FormModel> GetFormDetails(int userId);
        List<PropertyModel> GetPropertyDetails(int userId, int formId);
    }
}
