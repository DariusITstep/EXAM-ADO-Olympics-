using static System.Console;

namespace EXAM_ADO_Olympics_
{
    internal static class UI
    {
        public static void ShowMainMenu(OlympicsEntities db)
        {
            while (true)
            {
                Clear();

                WriteLine("╔══════════════════════════════════════╗");
                WriteLine("║              OLYMPICS                ║");
                WriteLine("╠══════════════════════════════════════╣");
                WriteLine("║  1. INFORMATION                      ║");
                WriteLine("║  2. STATISTICS                       ║");
                WriteLine("║  3. CHANGES                          ║");
                WriteLine("║  4. EXIT                             ║");
                WriteLine("╚══════════════════════════════════════╝");

                int choice = GetChoice(1, 4);

                switch (choice)
                {
                    case 1: ShowInformationMenu(db); break;
                    case 2: ShowStatisticsMenu(db); break;
                    case 3: ShowChangesMenu(db); break;
                    case 4: return;
                }
            }
        }

        private static void ShowInformationMenu(OlympicsEntities db)
        {
            while (true)
            {
                Clear();

                WriteLine("╔══════════════════════════════════════╗");
                WriteLine("║            INFORMATION               ║");
                WriteLine("╠══════════════════════════════════════╣");
                WriteLine("║  1. Countries                        ║");
                WriteLine("║  2. Cities                           ║");
                WriteLine("║  3. Olympiads                        ║");
                WriteLine("║  4. Sports                           ║");
                WriteLine("║  5. Disciplines                      ║");
                WriteLine("║  6. Athletes                         ║");
                WriteLine("║  7. Participations                   ║");
                WriteLine("║  8. Back                             ║");
                WriteLine("╚══════════════════════════════════════╝");

                int choice = GetChoice(1, 8);

                if (choice == 8) return;

                Handler.Run(1, choice, db);

                ReadKey();
            }
        }

        private static void ShowStatisticsMenu(OlympicsEntities db)
        {
            while (true)
            {
                Clear();

                WriteLine("╔══════════════════════════════════════╗");
                WriteLine("║             STATISTICS               ║");
                WriteLine("╠══════════════════════════════════════╣");
                WriteLine("║  1. Medal standings                  ║");
                WriteLine("║  2. Medalists by sport               ║");
                WriteLine("║  3. Top gold country                 ║");
                WriteLine("║  4. Athlete with most gold           ║");
                WriteLine("║  5. Most frequent host country       ║");
                WriteLine("║  6. Country statistics               ║");
                WriteLine("║  7. Back                             ║");
                WriteLine("╚══════════════════════════════════════╝");

                int choice = GetChoice(1, 7);

                if (choice == 7) return;

                Handler.Run(2, choice, db);

                ReadKey();
            }
        }

        private static void ShowChangesMenu(OlympicsEntities db)
        {
            while (true)
            {
                Clear();

                WriteLine("╔══════════════════════════════════════╗");
                WriteLine("║               CHANGES                ║");
                WriteLine("╠══════════════════════════════════════╣");
                WriteLine("║  1. Add participation                ║");
                WriteLine("║  2. Delete participation             ║");
                WriteLine("║  3. Edit participation               ║");
                WriteLine("║  4. Back                             ║");
                WriteLine("╚══════════════════════════════════════╝");

                int choice = GetChoice(1, 4);

                if (choice == 4) return;

                Handler.Run(3, choice, db);

                ReadKey();
            }
        }

        private static int GetChoice(int min, int max)
        {
            while (true)
            {
                Write("\nChoose option: ");

                if (int.TryParse(ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                WriteLine($"Invalid choice. Enter a number from {min} to {max}.");
                ReadKey();
            }
        }
    }
}