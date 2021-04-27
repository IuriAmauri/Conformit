using System;

namespace TestProgrammationConformit.Entities
{
    public class EvenementComment
    {
        public int Id { get; set; }        
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int EvenementId { get; set; }
    }    
}