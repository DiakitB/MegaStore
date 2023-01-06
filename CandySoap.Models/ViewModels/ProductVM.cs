using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CandySoap.Models.ViewModels
{
	public class ProductVM
	{
		[ValidateNever]
		public	IEnumerable<SelectListItem> CoverTypeList { get; set; }
		[ValidateNever]
		public	IEnumerable<SelectListItem> CategoryList { get; set; }
public Product Product { get; set; }

	}
}
