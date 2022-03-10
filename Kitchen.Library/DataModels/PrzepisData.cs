namespace Kitchen.Library.DataModels
{
    public class PrzepisData
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public int IdPodkategorii { get; set; }
        public string Przepis { get; set; }
        public int? LiczbaPorcji { get; set; }
        public int? BialkoWPorcji { get; set; }
        public int? WeglowodanyWPorcji { get; set; }
        public int? WartoscKalJednejPorcji { get; set; }
        public int? SzacowanaWartosc { get; set; }
        public int? TluszczeWPorcji { get; set; }
        public int IdPochodzenia { get; set; }
        public string Kraj { get; set; }
        public string Region { get; set; }
        public string UserId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
    }
}
