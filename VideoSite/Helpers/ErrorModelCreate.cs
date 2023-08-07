using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VideoSite.Helpers
{
    public static class ErrorModelCreate
    {
        public static Dictionary<string, object> ListInvalidValueErrors(this ModelStateDictionary ModelState)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState.GetValidationState(key) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    data.Add(key, ModelState[key].Errors.Select(x => x.ErrorMessage));
            }
            return data;
        }
    }
}
