using NZWalks.API.Models.DTO;

namespace NZWalks.API.Models.Domain
{
    public class Region
    {

        public Region()
        {

        }

        public Region(AddRegionRequestDTO addRegionRequestDTO)
        {
            this.Id = Guid.NewGuid();
            this.Name = addRegionRequestDTO.Name;
            this.Code = addRegionRequestDTO.Code;
            this.RegionImageUrl = addRegionRequestDTO.RegionImageUrl;
        }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
