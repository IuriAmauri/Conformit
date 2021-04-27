using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Entities
{
    public class Evenement
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PersonName { get; set; }
        public List<EvenementComment> Comments { get; set; }
    }
}