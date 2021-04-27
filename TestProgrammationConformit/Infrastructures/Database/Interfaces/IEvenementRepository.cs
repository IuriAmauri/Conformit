using TestProgrammationConformit.Entities;

namespace TestProgrammationConformit.Infrastructures.Database.Interfaces
{
    public interface IEvenementRepository
    {
         bool SaveChanges();
         PagedList<Evenement> GetAllEvenements(EvenementParameters parameters);
         Evenement GetEvenementById(int id);
         Evenement CreateEvenement(Evenement evenementCreate);
         void UpdateEvenement(Evenement evenementUpdate);
         void DeleteEvenement(int id);
    }
}