using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestProgrammationConformit.Entities;
using TestProgrammationConformit.Infrastructures.Database.Interfaces;

namespace TestProgrammationConformit.Controllers
{
    [Route("/events/{id}/comments")]
    [ApiController]
    public class EvenementCommentsController : ControllerBase
    {
        private readonly IEvenementCommentRepository _evenementCommentRepository;

        public EvenementCommentsController(IEvenementCommentRepository evenementCommentRepository)
        {
            _evenementCommentRepository = evenementCommentRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EvenementComment>> GetAllCommentsByEvenementId(int evenementId)
        {
            return Ok(_evenementCommentRepository.GetCommentsByEvenementId(evenementId));
        }

        [HttpDelete("{commentId}")]
        public ActionResult DeleteCommentById(int commentId)
        {
            _evenementCommentRepository.DeleteCommentById(commentId);
            return Accepted();
        }
    }
}