using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Infrastructure.Database.Seeding
{
    internal class PackageInit
    {
        public List<Zasilka> GetPackages()
        {
            return new List<Zasilka>
            {
            
                new Zasilka
                {
                    Id = 1,
                    Cislo = "123-45-67",
                    DatumOdeslani = new DateTime(2025, 11, 20, 10, 30, 0),
                    StavId = 3, // Doručeno
                    DestinaceUlice = "Nádražní",
                    DestinaceCP = "456",
                    DestinaceMesto = "Brno",
                    DestinacePsc = "602 00",
                    OdesilatelId = 7,
                    PrijemceId = 8,
                    KuryrId = 4
                },
                
                
                new Zasilka
                {
                    Id = 2,
                    Cislo = "234-56-78",
                    DatumOdeslani = new DateTime(2025, 11, 23, 14, 15, 0),
                    StavId = 2, // V přepravě
                    DestinaceUlice = "Zahradní",
                    DestinaceCP = "789",
                    DestinaceMesto = "Ostrava",
                    DestinacePsc = "700 30",
                    OdesilatelId = 7,
                    PrijemceId = 9, 
                    KuryrId = 4     
                },
                
                
                new Zasilka
                {
                    Id = 3,
                    Cislo = "345-67-89",
                    DatumOdeslani = new DateTime(2025, 11, 24, 9, 0, 0),
                    StavId = 1, // Objednávka vytvořena
                    DestinaceUlice = "Školní",
                    DestinaceCP = "321",
                    DestinaceMesto = "Plzeň",
                    DestinacePsc = "301 00",
                    OdesilatelId = 9,
                    PrijemceId = 11,  
                    KuryrId = null
                },
                

                new Zasilka
                {
                    Id = 4,
                    Cislo = "456-78-90",
                    DatumOdeslani = new DateTime(2025, 11, 22, 16, 45, 0),
                    StavId = 2, // V přepravě
                    DestinaceUlice = "Hlavní",
                    DestinaceCP = "123",
                    DestinaceMesto = "Praha",
                    DestinacePsc = "110 00",
                    OdesilatelId = 10,
                    PrijemceId = 11,  
                    KuryrId = 4      
                },
                

                new Zasilka
                {
                    Id = 5,
                    Cislo = "567-89-01",
                    DatumOdeslani = new DateTime(2025, 11, 18, 11, 20, 0),
                    StavId = 3, // Doručeno
                    DestinaceUlice = "Zahradní",
                    DestinaceCP = "789",
                    DestinaceMesto = "Ostrava",
                    DestinacePsc = "700 30",
                    OdesilatelId = 8,
                    PrijemceId = 10,  
                    KuryrId = 5      
                },
                

                new Zasilka
                {
                    Id = 6,
                    Cislo = "678-90-12",
                    DatumOdeslani = new DateTime(2025, 11, 15, 8, 30, 0),
                    StavId = 2, // V přepravě
                    DestinaceUlice = "Hlavní",
                    DestinaceCP = "123",
                    DestinaceMesto = "Praha",
                    DestinacePsc = "110 00",
                    OdesilatelId = 11,
                    PrijemceId = 7,  
                    KuryrId = 5      
                },

                new Zasilka
                {
                    Id = 7,
                    Cislo = "789-01-23",
                    DatumOdeslani = new DateTime(2025, 11, 15, 8, 30, 0),
                    StavId = 2, // V přepravě
                    DestinaceUlice = "Školní",
                    DestinaceCP = "300",
                    DestinaceMesto = "Zlín",
                    DestinacePsc = "760 01",
                    OdesilatelId = 7,
                    PrijemceId = 10,
                    KuryrId = 6
                }
            };
        }
    }
}