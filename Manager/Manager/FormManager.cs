using System;
using System.Collections.Generic;

namespace Manager.Manager
{
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    public class FormManager : IFormManager
    {
        private readonly IFormRepository repository;

        public FormManager(IFormRepository repository)
        {
            this.repository = repository;
        }
        public bool AddToForm(FormListModel formData)
        {
            try
            {
                return this.repository.AddToForm(formData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FormModel> GetFormDetails(int userId)
        {
            try
            {
                return this.repository.GetFormDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PropertyModel> GetPropertyDetails(int userId, int formId)
        {
            try
            {
                return this.repository.GetPropertyDetails(userId,formId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
