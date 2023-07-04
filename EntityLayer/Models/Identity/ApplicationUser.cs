using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Town { get; set; }
		[Required]
		public string City{ get; set; }
		public string? WebsiteUrl { get; set; }
		[DataType(DataType.Url)]
		public string? Maplink { get; set; }
		[MinLength(7), MaxLength(7), Required]
		public List<WorkTime> WorkTime { get; set; } 
		public DateTime CreatedDate { get; set; }=DateTime.Now;
		public string? ContactEmail { get; set; }
		public string? FaxNumber { get; set; }
		[Required]
		public string LogoLink { get; set; }
		public string? BackgroundImageLink { get; set; }
		public string? YoutubeLink { get; set; }
		public bool AllowEmail { get; set; }
		public int Index { get; set; } = 1;
	}
	public class WorkTime
	{
		public WorkTime(string day)
		{
			if(!string.IsNullOrWhiteSpace(day))
				this.Day= day;
		}
		public WorkTime()
		{
		}
		public string Day { get; set; }
		public DateTime Opening { get; set; }
		public DateTime Closing { get; set; }
		public string Value
		{
			get
			{
				if (Opening.ToShortTimeString() == Closing.ToShortTimeString())
					return "Kapalı";
				else
					return Opening.ToShortTimeString() + " - " + Closing.ToShortTimeString();
			}
		}
	}
}
