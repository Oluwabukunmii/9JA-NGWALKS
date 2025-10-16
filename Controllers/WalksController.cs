using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NGWALKSAPI.Models.Domain;
using NGWALKSAPI.Models.DTO;
using NGWALKSAPI.CustomActionFilters;
using NGWALKSAPI.API.Repositories;

namespace NGWALKSAPI.Controllers
{
    //api//walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository WalkRepository;


        public WalksController(IMapper mapper, IWalkRepository WalkRepository)
        {
            this.mapper = mapper;
            this.WalkRepository = WalkRepository;

        }


        //CREATE WALKS
        // POST : /api/walks

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto AddWalkRequestDto)
        {


            //Add Dto to domain Model

            var walkDomainModel = mapper.Map<Walk>(AddWalkRequestDto);

            await WalkRepository.CreateAsync(walkDomainModel);


            //Map domain model to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

            //testing something




        }

        //GET ALL WALKS
        //GET : /API/WALKS?filterOn=Name, filterQuery=Track&SortBy=Name&isAscending=true&pageNumber=1&pagesize =10

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string?  filterOn, [FromQuery] string? filterQuery ,
            [FromQuery] string? sortBy, [FromQuery] bool? IsAscending ,
            [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 1000)

        {
            var walksDomainModel = await WalkRepository.GetAllAsync(filterOn , filterQuery , sortBy ,
                IsAscending ?? true ,pageNumber, pageSize );

            //Create an exception

           // throw new Exception("This is a new exception");

            //Map domain model to Dto


            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));


        }

        //GET SINGLE Walk (GET WALK BY ID)
        // GET : https://localhost:portnumber/api/walks{id}//

        [HttpGet]

        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainModel = await WalkRepository.GetByIdAsync(id);

            if (walksDomainModel == null)

            {
                return NotFound();
            }
            //Map domain model to Dto


            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        // PUT:https://localhost:portnumber/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]



        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)

        {

            //Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            var theWalkDomainModel = await WalkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //Map  Domain Model back to Dto

            return Ok(mapper.Map<WalkDto>(theWalkDomainModel));



        }

        //TO DELETE A WALK.
        //DELETE: https://localhost:portnumber/api/walk/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var walkDomainModel = await WalkRepository.DeleteAsync(id);

            if (walkDomainModel == null)

            {
                return NotFound();
            }

            //Map  Domain Model back to Dto


            return Ok(mapper.Map<WalkDto>(walkDomainModel));


            
        }
    }
}