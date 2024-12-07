namespace documents_web_api.Models
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string CategorieNom { get; set; }
        public string CheminDocument { get; set; }
        public int PatientId { get; set; }
    }
}
