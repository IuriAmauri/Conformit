using System.Collections.Generic;
using TestProgrammationConformit.Entities;

namespace TestProgrammationConformit.Infrastructures.Database.Interfaces
{
    public interface IEvenementCommentRepository
    {
         bool SaveChanges();
         EvenementComment GetCommentById(int id);
         IEnumerable<EvenementComment> GetCommentsByEvenementId(int evenementId);
         void DeleteCommentById(int id);
         void DeleteCommentsByEvenementId(int evenementId);
         EvenementComment CreateComment(EvenementComment comment);
    }
}