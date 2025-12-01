using DorucovaciSluzba.Infrastructure.Identity;

namespace DorucovaciSluzba.Infrastructure.Database.Seeding
{
    internal class IdentityUserInit
    {
        public List<User> GetAllUsers()
        {
            return new List<User>
            {
                GetAdmin1(),
                GetAdmin2(),

                GetPodpora(),

                GetKuryr1(),
                GetKuryr2(),
                GetKuryr3(),

                
                GetUzivatel1(),
                GetUzivatel2(),
                GetUzivatel3(),
                GetUzivatel4(),
                GetUzivatel5()


            };
        }

        public User GetAdmin1()
        {
            return new User()
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@kuryr.cz",
                NormalizedEmail = "ADMIN@KURYR.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEP88Q6YXU7RQG6LcDoHvZpXtdmv1Kh0hyoBkWJtFrTAYVz+OyRD1ZvzowRpa/sI4xg==",
                SecurityStamp = "VGWVSB6MVQ7NB5QYDEG52OEBGMYC3CP5",
                ConcurrencyStamp = "b09a83ae-cfd3-4ee7-97e6-fbcf0b0fe78c",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
        }

        public User GetAdmin2()
        {
            return new User()
            {
                Id = 2,
                FirstName = "Mikydosh",
                LastName = "Mikydosh",
                UserName = "Mikydosh",
                NormalizedUserName = "MIKYDOSH",
                Email = "mikydosh@kuryr.cz",
                NormalizedEmail = "MIKYDOSH@KURYR.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEB0xgwR38FFdOSyzuCtsLKEgylMVI5QJJtX7SD4TNt+plq1SOFJIXSPV2HL2XZVzUw==",
                SecurityStamp = "3TUBCJIIU3M7B4O2CBRAFMUJQ2LMEBID",
                ConcurrencyStamp = "cee2a6bf-19a7-4e22-8d24-1d13c8c50045",
                PhoneNumber = "777 777 777",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
        }

        public User GetPodpora()
        {
            return new User()
            {
                Id = 3,
                FirstName = "Support",
                LastName = "Support",
                UserName = "support",
                NormalizedUserName = "SUPPORT",
                Email = "support@kuryr.cz",
                NormalizedEmail = "SUPPORT@KURYR.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEIK3vTeRO8jtSHQeu22rjXmOAcOrnj5TTrUyLrC2PO/CLutEmOinp2XbeeZ8JzeztQ==",
                SecurityStamp = "LZ6JM3OG3F3SHIPBSX5CXCHLIZOBAO7N",
                ConcurrencyStamp = "038db544-65bc-4eb8-babd-2b62292f550f",
                PhoneNumber = "777 888 999",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "777 888 999"
            };
        }

        public User GetKuryr1()
        {
            return new User()
            {
                Id = 4,
                FirstName = "Petr",
                LastName = "Svoboda",
                UserName = "petr.svoboda",
                NormalizedUserName = "PETR.SVOBODA",
                Email = "petr.svoboda@email.cz",
                NormalizedEmail = "PETR.SVOBODA@EMAIL.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEIwBb49OKla+4yeCCS1R94+d1xcujKitJWWBDa41PgsTXnvmJJfVoxkUZ2/+fVXjNA==",
                SecurityStamp = "5LIJBOB6YT3NYVGTHLD3WFTPCHTTFEKQ",
                ConcurrencyStamp = "0f5ced05-1c9c-4ed4-bf45-560a91558b19",
                PhoneNumber = "700 123 456",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "700 123 456"
            };
        }

        public User GetKuryr2()
        {
            return new User()
            {
                Id = 5,
                FirstName = "Martin",
                LastName = "Veselý",
                UserName = "martin_vesely",
                NormalizedUserName = "MARTIN_VESELY",
                Email = "martin_vesely@seznam.cz",
                NormalizedEmail = "MARTIN_VESELY@SEZNAM.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEIwBb49OKla+4yeCCS1R94+d1xcujKitJWWBDa41PgsTXnvmJJfVoxkUZ2/+fVXjNA==",
                SecurityStamp = "UNK27SYDWN7W5R2YEOCUUFERKXCP4ITS",
                ConcurrencyStamp = "d20c95cg-ehg5-6gg9-d0g8-hdfhb21hg90e",
                PhoneNumber = "702 456 789",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "702 456 789"
            };
        }

        public User GetKuryr3()
        {
            return new User()
            {
                Id = 6,
                FirstName = "Lukáš",
                LastName = "Černý",
                UserName = "lukas.cerny",
                NormalizedUserName = "LUKAS.CERNY",
                Email = "lukas.cerny@gmail.com",
                NormalizedEmail = "LUKAS.CERNY@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEIwBb49OKla+4yeCCS1R94+d1xcujKitJWWBDa41PgsTXnvmJJfVoxkUZ2/+fVXjNA==",
                SecurityStamp = "A3LA2H6W6F4PZIAB2UHWRER4FVJJIVUM",
                ConcurrencyStamp = "d20c95cg-ehg5-6gg9-d0g8-hdfhb21hg90e",
                PhoneNumber = "702 456 789",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "739 556 789"
            };
        }


        public User GetUzivatel1()
        {
            return new User()
            {
                Id = 7,
                FirstName = "Karel",
                LastName = "Procházka",
                UserName = "karel.prochazka",
                NormalizedUserName = "KAREL.PROCHAZKA",
                Email = "karel.prochazka@email.cz",
                NormalizedEmail = "KAREL.PROCHAZKA@EMAIL.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==",
                SecurityStamp = "56RHAYBYWWYNQZ3N53XH76OVIE6QGTPQ",
                ConcurrencyStamp = "38882ce7-1365-4e87-b881-11156cb75c5a",
                PhoneNumber = "603 111 222",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "603 111 222",
                Ulice = "Hlavní",
                CP = "123",
                Mesto = "Praha",
                Psc = "110 00"
            };
        }

        public User GetUzivatel2()
        {
            return new User()
            {
                Id = 8,
                FirstName = "Eva",
                LastName = "Málková",
                UserName = "eva.malkova",
                NormalizedUserName = "EVA.MALKOVA",
                Email = "eva.malkova@email.cz",
                NormalizedEmail = "EVA.MALKOVA@EMAIL.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==",
                SecurityStamp = "B3FEHEGUWPYSIMP6TIN7X7XUBGLJTF2T",
                ConcurrencyStamp = "6eee7fcd-c43b-42eb-acdb-909a007f85ee",
                PhoneNumber = "604 333 444",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "604 333 444",
                Ulice = "Nádražní",
                CP = "456",
                Mesto = "Brno",
                Psc = "602 00"
            };
        }

        public User GetUzivatel3()
        {
            return new User()
            {
                Id = 9,
                FirstName = "Jana",
                LastName = "Horáková",
                UserName = "jana_horakova",
                NormalizedUserName = "JANA_HORAKOVA",
                Email = "jana_horakova@email.cz",
                NormalizedEmail = "JANA_HORAKOVA@EMAIL.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==",
                SecurityStamp = "XNCJIJ2L3J6HVXENA6UKUV5ND4QGLJCA",
                ConcurrencyStamp = "2a59d5f9-9062-4722-b9f6-370de85ec0c7",
                PhoneNumber = "605 555 666",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "605 555 666",
                Ulice = "Zahradní",
                CP = "789",
                Mesto = "Ostrava",
                Psc = "700 30"
            };
        }

        public User GetUzivatel4()
        {
            return new User()
            {
                Id = 10,
                FirstName = "Pavel",
                LastName = "Dobrý",
                UserName = "paveldobry",
                NormalizedUserName = "PAVELDOBRY",
                Email = "paveldobry@gmail.com",
                NormalizedEmail = "PAVELDOBRY@GMAILL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==",
                SecurityStamp = "IAHFGXHAVOV3PM7P2TG7TNTHNJ4GBJD5",
                ConcurrencyStamp = "0f5ced05-1c9c-4ed4-bf45-560a91558b19",
                PhoneNumber = "606 777 888",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "606 777 888",
                Ulice = "Školní",
                CP = "321",
                Mesto = "Plzeň",
                Psc = "301 00"
            };
        }

        public User GetUzivatel5()
        {
            return new User()
            {
                Id = 11,
                FirstName = "Kateřina",
                LastName = "Dobrá",
                UserName = "katerina.dobra",
                NormalizedUserName = "KATERINADOBRA",
                Email = "katerina.dobra@gmail.com",
                NormalizedEmail = "KATERINA.DOBRA@GMAILL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==",
                SecurityStamp = "B4PZVLZDNK7MKANXOJL4RRETNS5EHLFJ",
                ConcurrencyStamp = "62667ef0-5589-4d42-9d78-575134d4442f",
                PhoneNumber = "609 654 888",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Telefon = "609 654 888",
                Ulice = "Školní",
                CP = "321",
                Mesto = "Plzeň",
                Psc = "301 00"
            };
        }
    }
}