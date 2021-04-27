using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Entities;
using TestProgrammationConformit.Infrastructures;
using TestProgrammationConformit.Infrastructures.Database.Interfaces;

namespace TestProgrammationConformit.Infrastructures.Database
{
    public class EvenementRepository : IEvenementRepository
    {
        private readonly ConformitContext _conformitContext;

        public EvenementRepository(ConformitContext conformitContext)
        {
            _conformitContext = conformitContext;
        }

        public Evenement CreateEvenement(Evenement evenementModel)
        {
            return _conformitContext.Evenements.Add(evenementModel).Entity;
        }

        public PagedList<Evenement> GetAllEvenements(EvenementParameters parameters)
        {
            return PagedList<Evenement>.ToPagedList(_conformitContext.Evenements.Include(c => c.Comments), 
                parameters.PageNumber, parameters.PageSize);
        }

        public Evenement GetEvenementById(int id)
        {
            return _conformitContext.Evenements.FirstOrDefault(f => f.Id == id);
        }

        public bool SaveChanges()
        {
            return _conformitContext.SaveChanges() > 0;
        }

        public void DeleteEvenement(int id)
        {
            var evenementDelete = GetEvenementById(id);
            _conformitContext.Evenements.Remove(evenementDelete);
        }

        public void UpdateEvenement(Evenement evenementUpdate)
        {
            var local = _conformitContext.Set<Evenement>()
                                         .Local
                                         .FirstOrDefault(entry => entry.Id.Equals(evenementUpdate.Id));

            if (local != null)
                _conformitContext.Entry(local).State = EntityState.Detached;

            _conformitContext.Entry(evenementUpdate).State = EntityState.Modified;
        }
    }
}