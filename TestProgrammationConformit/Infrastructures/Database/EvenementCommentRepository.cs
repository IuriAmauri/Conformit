using System.Collections.Generic;
using System.Linq;
using TestProgrammationConformit.Entities;
using TestProgrammationConformit.Infrastructures.Database.Interfaces;

namespace TestProgrammationConformit.Infrastructures.Database
{
    public class EvenementCommentRepository : IEvenementCommentRepository
    {
        private readonly ConformitContext _conformitContext;

        public EvenementCommentRepository(ConformitContext conformitContext)
        {
            _conformitContext = conformitContext;
        }

        public EvenementComment CreateComment(EvenementComment comment)
        {
            return _conformitContext.EvenementComments.Add(comment).Entity;
        }

        public void DeleteCommentById(int id)
        {
            var commentDelete = GetCommentById(id);
            _conformitContext.EvenementComments.Remove(commentDelete);
        }

        public void DeleteCommentsByEvenementId(int evenementId)
        {
            var commentsDelete = GetCommentsByEvenementId(evenementId);
            _conformitContext.RemoveRange(commentsDelete);
        }

        public EvenementComment GetCommentById(int id)
        {
            return _conformitContext.EvenementComments.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<EvenementComment> GetCommentsByEvenementId(int evenementId)
        {
            return _conformitContext.EvenementComments.Where(w => w.EvenementId == evenementId)
                                                  .ToList();
        }

        public bool SaveChanges()
        {
            return _conformitContext.SaveChanges() > 0;
        }
    }
}