using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    //https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(ApplicationDbContext db, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _db = db;
            _regionRepository = regionRepository;
            _mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAllRegions Action Method was invoked");

            //Get data from database - Domain models
            IEnumerable<Region> regionDomain = await _regionRepository.GetAllAsync();
            //await _db.Regions.ToListAsync();

            //Map Domain Models to DTOs
            //List<RegionDTO> regionDTO = new List<RegionDTO>();

            //foreach (Region region in regionDomain)
            //{
            //    regionDTO.Add(new RegionDTO
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });

            //}


            //Map Domain Models to DTOs
            var regionsDTO = _mapper.Map<List<RegionDTO>>(regionDomain);

            logger.LogInformation($"Finished GetAllRegions request with data {JsonSerializer.Serialize(regionDomain)}");


            //Return DTO
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model from DataBase
            Region region = await _regionRepository.GetByIdAsync(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            RegionDTO regionDTO = _mapper.Map<RegionDTO>(region);

            //RegionDTO regionDTO = new RegionDTO(region);

            return Ok(regionDTO);
        }


        //https://www.localhost:7128/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Validation Condition
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Region regionDomainModel = _mapper.Map<Region>(addRegionRequestDTO);
            //Map or Convert DTO to Domain Model

            //regionDomainModel.Name = addRegionRequestDTO.Name;
            //regionDomainModel.Code = addRegionRequestDTO.Code;
            //regionDomainModel.RegionImageUrl = addRegionRequestDTO.RegionImageUrl;

            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            RegionDTO regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);

        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Map DTO to Domain Model
            Region domainModel = _mapper.Map<Region>(updateRegionRequest);


            //Check if region exists
            Region regionDomainModel = await _regionRepository.UpdateAsync(id, domainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            RegionDTO regionDTO = _mapper.Map<RegionDTO>(domainModel);
            return Ok(regionDTO);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region regionDomainModel = await _regionRepository.DeleteAsync(r => r.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            RegionDTO regionDTO = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }

    }
}
