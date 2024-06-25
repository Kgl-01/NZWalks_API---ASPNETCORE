using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class RegionDTO
    {
        public RegionDTO()
        {

        }

        public RegionDTO(Region region)
        {
            this.Id = region.Id;
            this.Name = region.Name;
            this.Code = region.Code;
            this.RegionImageUrl = region.RegionImageUrl;
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
