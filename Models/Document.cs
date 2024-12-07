namespace documents_web_api.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string nom { get; set; }
        public Categorie categorie { get; set; } 
        public int CategorieId { get; set; } 
        public string cheminDocument { get; set; }
        public int PatientId { get; set; }
    }
}
