using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code should be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code should be maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Code should be maximum of 3 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
