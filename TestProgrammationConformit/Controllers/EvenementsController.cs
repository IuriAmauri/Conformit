using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestProgrammationConformit.Entities;
using TestProgrammationConformit.Infrastructures.Database.Interfaces;

namespace TestProgrammationConformit.Controllers
{
    [Route("/events")]
    [ApiController]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementRepository _evenementRepository;
        private readonly IEvenementCommentRepository _evenementCommentRepository;

        public EvenementsController(
            IEvenementRepository evenementRepository,
            IEvenementCommentRepository evenementCommentRepository)
        {
            _evenementRepository = evenementRepository;
            _evenementCommentRepository = evenementCommentRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evenement>> GetAllEvenements([FromQuery] EvenementParameters parameters)
        {
            var evenements = _evenementRepository.GetAllEvenements(parameters);

            var metadata = new
            {
                evenements.TotalCount,
                evenements.PageSize,
                evenements.CurrentPage,
                evenements.TotalPages,
                evenements.HasNext,
                evenements.HasPrevious
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(evenements);
        }

        [HttpGet("{id}", Name="GetEvenementById")]
        public ActionResult<Evenement> GetEvenementById(int id)
        {
            var evenement = _evenementRepository.GetEvenementById(id);
            
            if (evenement != null)
                return Ok(evenement);
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Evenement> PostEvenement(Evenement evenementModel)
        {
            if (evenementModel == null)
                return BadRequest(nameof(evenementModel));

            var createdEvenement = _evenementRepository.CreateEvenement(evenementModel);
            _evenementRepository.SaveChanges();

            if (createdEvenement != null)
                return CreatedAtRoute(nameof(GetEvenementById), new {Id = createdEvenement.Id}, createdEvenement);
            
            return BadRequest(nameof(evenementModel));
        }

        [HttpPost("batchPost")]
        public ActionResult<IEnumerable<Evenement>> BatchPostEvenement(IEnumerable<Evenement> evenementsModel)
        {
            if (!evenementsModel.Any())
                return BadRequest(nameof(evenementsModel));

            var evenementsCreated = new List<Evenement>();

            using (var scope = new TransactionScope())
            {
                foreach (var evenementModel in evenementsModel)
                {
                    var createdEvenement = _evenementRepository.CreateEvenement(evenementModel);
                    _evenementRepository.SaveChanges();

                    evenementsCreated.Add(createdEvenement);
                }                
                scope.Complete();
            }            

            return Ok(evenementsCreated);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEvenement(int id, Evenement evenementModel)
        {
            if (_evenementRepository.GetEvenementById(evenementModel.Id) == null)
                return NotFound();
            
            _evenementRepository.UpdateEvenement(evenementModel);
            _evenementRepository.SaveChanges();
            
            return NoContent();
        }

        [HttpPut]
        public ActionResult BatchUpdateEvenement(IEnumerable<Evenement> evenementsModel)
        {
            foreach (var evenementModel in evenementsModel)
            {
                if (_evenementRepository.GetEvenementById(evenementModel.Id) == null)
                    continue;
                
                _evenementRepository.UpdateEvenement(evenementModel);
                _evenementRepository.SaveChanges();
            }

            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteEvenementById(int id)
        {
            _evenementRepository.DeleteEvenement(id);
            return NoContent();
        }
    }    
}