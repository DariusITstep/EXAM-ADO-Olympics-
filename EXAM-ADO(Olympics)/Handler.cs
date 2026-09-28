using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace EXAM_ADO_Olympics_
{
    internal static class Handler
    {
        public static void Run(int category, int choice, OlympicsEntities db)
        {
            switch (category)
            {
                case 1: Information(choice, db); break;
                case 2: Statistics(choice, db); break;
                case 3: Changes(choice, db); break;
            }
        }

        private static void Information(int choice, OlympicsEntities db)
        {
            switch (choice)
            {
                case 1:
                    {
                        var result = db.Countries
                            .Where(c => c.IsActive)
                            .Select(c => new
                            {
                                c.Id,
                                c.Name
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 2:
                    {
                        var result = db.Cities
                            .Where(c => c.IsActive)
                            .Select(c => new
                            {
                                c.Id,
                                c.Name,
                                Country = c.Countries.Name
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 3:
                    {
                        var result = db.Olympiads
                            .Where(o => o.IsActive)
                            .Select(o => new
                            {
                                o.Id,
                                o.Year,
                                o.Type,
                                Country = o.Countries.Name,
                                City = o.Cities.Name
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 4:
                    {
                        var result = db.Sports
                            .Where(s => s.IsActive)
                            .Select(s => new
                            {
                                s.Id,
                                s.Name
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 5:
                    {
                        var result = db.Disciplines
                            .Where(d => d.IsActive)
                            .Select(d => new
                            {
                                d.Id,
                                d.Name,
                                Sport = d.Sports.Name
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 6:
                    {
                        var result = db.Athletes
                            .Where(a => a.IsActive)
                            .Select(a => new
                            {
                                a.Id,
                                a.FirstName,
                                a.LastName,
                                a.BirthDate,
                                Country = a.Countries.Name
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 7:
                    {
                        var result = db.Participations
                            .Where(p => p.IsActive)
                            .Select(p => new
                            {
                                p.Id,
                                Athlete = p.Athletes.FirstName + " " + p.Athletes.LastName,
                                Discipline = p.Disciplines.Name,
                                Olympiad = p.Olympiads.Year + " " + p.Olympiads.Type,
                                p.Result,
                                p.Medal
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }
            }
        }

        private static void Statistics(int choice, OlympicsEntities db)
        {
            switch (choice)
            {
                case 1:
                    {
                        var result = db.Participations
                            .Where(p => p.IsActive && p.Medal != "Empty")
                            .GroupBy(p => p.Athletes.Countries.Name)
                            .Select(g => new
                            {
                                Country = g.Key,
                                Gold = g.Count(p => p.Medal == "Gold"),
                                Silver = g.Count(p => p.Medal == "Silver"),
                                Bronze = g.Count(p => p.Medal == "Bronze")
                            })
                            .OrderByDescending(x => x.Gold)
                            .ThenByDescending(x => x.Silver)
                            .ThenByDescending(x => x.Bronze)
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 2:
                    {
                        var result = db.Participations
                            .Where(p => p.IsActive && p.Medal != "Empty")
                            .Select(p => new
                            {
                                Sport = p.Disciplines.Sports.Name,
                                Discipline = p.Disciplines.Name,
                                Athlete = p.Athletes.FirstName + " " + p.Athletes.LastName,
                                Medal = p.Medal
                            })
                            .OrderBy(x => x.Sport)
                            .ThenBy(x => x.Discipline)
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 3:
                    {
                        var result = db.Participations
                            .Where(p => p.IsActive && p.Medal == "Gold")
                            .GroupBy(p => p.Athletes.Countries.Name)
                            .Select(g => new
                            {
                                Country = g.Key,
                                GoldMedals = g.Count()
                            })
                            .OrderByDescending(x => x.GoldMedals)
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 4:
                    {
                        var result = db.Participations
                            .Where(p => p.IsActive && p.Medal == "Gold")
                            .GroupBy(p => new
                            {
                                Sport = p.Disciplines.Sports.Name,
                                Athlete = p.Athletes.FirstName + " " + p.Athletes.LastName
                            })
                            .Select(g => new
                            {
                                g.Key.Sport,
                                g.Key.Athlete,
                                GoldMedals = g.Count()
                            })
                            .OrderBy(x => x.Sport)
                            .ThenByDescending(x => x.GoldMedals)
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 5:
                    {
                        var result = db.Olympiads
                            .Where(o => o.IsActive)
                            .GroupBy(o => o.Countries.Name)
                            .Select(g => new
                            {
                                Country = g.Key,
                                OlympiadsHosted = g.Count()
                            })
                            .OrderByDescending(x => x.OlympiadsHosted)
                            .ToList();

                        ShowResult(result);
                        break;
                    }

                case 6:
                    {
                        var result = db.Countries
                            .Where(c => c.IsActive)
                            .Select(c => new
                            {
                                Country = c.Name,
                                Athletes = c.Athletes.Count(a => a.IsActive),
                                Gold = c.Athletes
                                    .SelectMany(a => a.Participations)
                                    .Count(p => p.IsActive && p.Medal == "Gold"),
                                Silver = c.Athletes
                                    .SelectMany(a => a.Participations)
                                    .Count(p => p.IsActive && p.Medal == "Silver"),
                                Bronze = c.Athletes
                                    .SelectMany(a => a.Participations)
                                    .Count(p => p.IsActive && p.Medal == "Bronze")
                            })
                            .ToList();

                        ShowResult(result);
                        break;
                    }
            }
        }

        private static void Changes(int choice, OlympicsEntities db)
        {
            switch (choice)
            {
                case 1: AddParticipation(db); break;
                case 2: DeleteParticipation(db); break;
                case 3: EditParticipation(db); break;
            }
        }

        private static void AddParticipation(OlympicsEntities db)
        {
            Clear();

            WriteLine("ADD PARTICIPATION");
            WriteLine();

            WriteLine("OLYMPIADS");
            WriteLine();

            var olympiads = db.Olympiads
                .Where(o => o.IsActive)
                .OrderBy(o => o.Year)
                .ToList();

            for (int i = 0; i < olympiads.Count; i++)
            {
                WriteLine("{0}. {1} {2} - {3}, {4}",
                    i + 1,
                    olympiads[i].Year,
                    olympiads[i].Type,
                    olympiads[i].Countries.Name,
                    olympiads[i].Cities.Name);
            }

            WriteLine();
            Write("Choice: ");

            int choice;

            if (!int.TryParse(ReadLine(), out choice) || choice < 1 || choice > olympiads.Count) return;

            var olympiad = olympiads[choice - 1];


            Clear();

            WriteLine("ATHLETES");
            WriteLine();

            var athletes = db.Athletes
                .Where(a => a.IsActive)
                .OrderBy(a => a.LastName)
                .ToList();

            for (int i = 0; i < athletes.Count; i++)
            {
                WriteLine("{0}. {1} {2} - {3}",
                    i + 1,
                    athletes[i].FirstName,
                    athletes[i].LastName,
                    athletes[i].Countries.Name);
            }

            WriteLine();
            Write("Choice: ");

            if (!int.TryParse(ReadLine(), out choice) || choice < 1 || choice > athletes.Count) return;

            var athlete = athletes[choice - 1];

            Clear();

            WriteLine("DISCIPLINES");
            WriteLine();

            var disciplines = db.Disciplines
                .Where(d => d.IsActive)
                .OrderBy(d => d.Sports.Name)
                .ThenBy(d => d.Name)
                .ToList();

            for (int i = 0; i < disciplines.Count; i++)
            {
                WriteLine("{0}. {1} - {2}",
                    i + 1,
                    disciplines[i].Name,
                    disciplines[i].Sports.Name);
            }

            WriteLine();
            Write("Choice: ");

            if (!int.TryParse(ReadLine(), out choice) || choice < 1 || choice > disciplines.Count) return;

            var discipline = disciplines[choice - 1];


            Clear();

            Write("Result: ");
            string result = ReadLine();

            WriteLine();
            WriteLine("MEDAL");
            WriteLine("1. Gold");
            WriteLine("2. Silver");
            WriteLine("3. Bronze");
            WriteLine("4. Empty");
            WriteLine();

            Write("Choice: ");

            int medalChoice;

            if (!int.TryParse(ReadLine(), out medalChoice) ||
                medalChoice < 1 || medalChoice > 4)
                return;

            string medal;

            switch (medalChoice)
            {
                case 1:
                    medal = "Gold";
                    break;

                case 2:
                    medal = "Silver";
                    break;

                case 3:
                    medal = "Bronze";
                    break;

                default:
                    medal = "Empty";
                    break;
            }

            if (result == "Empty" && medal != "Empty")
            {
                WriteLine("Medal cannot be assigned when Result is Empty.");
                return;
            }

            bool exists = db.Participations.Any(p =>
                p.IsActive &&
                p.OlympiadId == olympiad.Id &&
                p.AthleteId == athlete.Id &&
                p.DisciplineId == discipline.Id);

            if (exists)
            {
                WriteLine("This participation already exists.");
                return;
            }

            var participation = new Participations
            {
                Olympiads = olympiad,
                Athletes = athlete,
                Disciplines = discipline,
                Result = result,
                Medal = medal,
                IsActive = true
            };

            db.Participations.Add(participation);
            db.SaveChanges();

            WriteLine();
            WriteLine("Participation added.");
        }

        private static void DeleteParticipation(OlympicsEntities db)
        {
            Clear();

            WriteLine("DELETE PARTICIPATION");
            WriteLine();

            ShowParticipations(db);

            WriteLine();
            Write("Participation Id: ");

            int participationId;

            if (!int.TryParse(ReadLine(), out participationId)) return;

            var participation = db.Participations
                .FirstOrDefault(p =>
                    p.Id == participationId &&
                    p.IsActive);

            if (participation == null) return;

            participation.IsActive = false;

            db.SaveChanges();

            WriteLine();
            WriteLine("Participation deleted.");
        }

        private static void EditParticipation(OlympicsEntities db)
        {
            Clear();

            WriteLine("EDIT PARTICIPATION");
            WriteLine();

            ShowParticipations(db);

            WriteLine();
            Write("Participation Id: ");

            int participationId;

            if (!int.TryParse(ReadLine(), out participationId)) return;

            var participation = db.Participations
                .FirstOrDefault(p =>
                    p.Id == participationId &&
                    p.IsActive);

            if (participation == null) return;

            Clear();

            WriteLine("EDIT FIELD");
            WriteLine();
            WriteLine("1. Result");
            WriteLine("2. Medal");
            WriteLine("3. Olympiad");
            WriteLine("4. Athlete");
            WriteLine("5. Discipline");
            WriteLine("6. Back");
            WriteLine();

            Write("Choice: ");

            int choice;

            if (!int.TryParse(ReadLine(), out choice) || choice < 1 || choice > 6) return;

            switch (choice)
            {
                case 1:
                    {
                        Write("New Result: ");

                        string result = ReadLine();

                        if (result == "Empty" && participation.Medal != "Empty")
                        {
                            WriteLine("Medal cannot be assigned when Result is Empty.");
                            return;
                        }

                        participation.Result = result;
                        break;
                    }

                case 2:
                    {
                        WriteLine("MEDAL");
                        WriteLine("1. Gold");
                        WriteLine("2. Silver");
                        WriteLine("3. Bronze");
                        WriteLine("4. Empty");
                        WriteLine();

                        Write("Choice: ");

                        int medalChoice;

                        if (!int.TryParse(ReadLine(), out medalChoice) || medalChoice < 1 || medalChoice > 4) return;

                        string medal;

                        switch (medalChoice)
                        {
                            case 1: medal = "Gold"; break;
                            case 2: medal = "Silver"; break;
                            case 3: medal = "Bronze"; break;
                            default: medal = "Empty"; break;
                        }

                        if (participation.Result == "Empty" && medal != "Empty")
                        {
                            WriteLine("Medal cannot be assigned when Result is Empty.");
                            return;
                        }

                        participation.Medal = medal;
                        break;
                    }

                case 3:
                    {
                        WriteLine("OLYMPIADS");
                        WriteLine();

                        var olympiads = db.Olympiads
                            .Where(o => o.IsActive)
                            .OrderBy(o => o.Year)
                            .Select(o => new
                            {
                                o.Id,
                                o.Year,
                                o.Type,
                                Country = o.Countries.Name,
                                City = o.Cities.Name
                            })
                            .ToList();

                        ShowResult(olympiads);

                        WriteLine();
                        Write("Olympiad Id: ");

                        int olympiadId;

                        if (!int.TryParse(ReadLine(), out olympiadId) ||
                            !db.Olympiads.Any(o =>
                                o.Id == olympiadId &&
                                o.IsActive))
                            return;

                        bool existsOlympiad = db.Participations.Any(p =>
                            p.Id != participation.Id &&
                            p.IsActive &&
                            p.OlympiadId == olympiadId &&
                            p.AthleteId == participation.AthleteId &&
                            p.DisciplineId == participation.DisciplineId);

                        if (existsOlympiad)
                        {
                            WriteLine("This participation already exists.");
                            return;
                        }

                        participation.OlympiadId = olympiadId;
                        break;
                    }

                case 4:
                    {
                        WriteLine("ATHLETES");
                        WriteLine();

                        var athletes = db.Athletes
                            .Where(a => a.IsActive)
                            .OrderBy(a => a.LastName)
                            .Select(a => new
                            {
                                a.Id,
                                a.FirstName,
                                a.LastName,
                                Country = a.Countries.Name
                            })
                            .ToList();

                        ShowResult(athletes);

                        WriteLine();
                        Write("Athlete Id: ");

                        int athleteId;

                        if (!int.TryParse(ReadLine(), out athleteId) ||
                            !db.Athletes.Any(a =>
                                a.Id == athleteId &&
                                a.IsActive))
                            return;

                        bool existsAthlete = db.Participations.Any(p =>
                            p.Id != participation.Id &&
                            p.IsActive &&
                            p.OlympiadId == participation.OlympiadId &&
                            p.AthleteId == athleteId &&
                            p.DisciplineId == participation.DisciplineId);

                        if (existsAthlete)
                        {
                            WriteLine("This participation already exists.");
                            return;
                        }

                        participation.AthleteId = athleteId;
                        break;
                    }

                case 5:
                    {
                        WriteLine("DISCIPLINES");
                        WriteLine();

                        var disciplines = db.Disciplines
                            .Where(d => d.IsActive)
                            .OrderBy(d => d.Sports.Name)
                            .ThenBy(d => d.Name)
                            .Select(d => new
                            {
                                d.Id,
                                d.Name,
                                Sport = d.Sports.Name
                            })
                            .ToList();

                        ShowResult(disciplines);

                        WriteLine();
                        Write("Discipline Id: ");

                        int disciplineId;

                        if (!int.TryParse(ReadLine(), out disciplineId) ||
                            !db.Disciplines.Any(d =>
                                d.Id == disciplineId &&
                                d.IsActive))
                            return;

                        bool existsDiscipline = db.Participations.Any(p =>
                            p.Id != participation.Id &&
                            p.IsActive &&
                            p.OlympiadId == participation.OlympiadId &&
                            p.AthleteId == participation.AthleteId &&
                            p.DisciplineId == disciplineId);

                        if (existsDiscipline)
                        {
                            WriteLine("This participation already exists.");
                            return;
                        }

                        participation.DisciplineId = disciplineId;
                        break;
                    }

                case 6: return;
            }

            db.SaveChanges();

            WriteLine();
            WriteLine("Participation updated.");
        }


        private static void ShowResult<T>(IEnumerable<T> result)
        {
            var properties = typeof(T).GetProperties();
            var items = result.ToList();

            int[] widths = new int[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                widths[i] = properties[i].Name.Length;

                foreach (var item in items)
                {
                    string value = properties[i].GetValue(item)?.ToString() ?? "";

                    if (value.Length > widths[i]) widths[i] = value.Length;
                }

                widths[i] += 2;
            }

            for (int i = 0; i < properties.Length; i++) Write($"{properties[i].Name.PadRight(widths[i])}");

            WriteLine();

            for (int i = 0; i < properties.Length; i++) Write(new string('-', widths[i]));

            WriteLine();

            foreach (var item in items)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    string value = properties[i].GetValue(item)?.ToString() ?? "";
                    Write(value.PadRight(widths[i]));
                }

                WriteLine();
            }
        }

        private static void ShowParticipations(OlympicsEntities db)
        {
            var result = db.Participations
                .Where(p => p.IsActive)
                .OrderBy(p => p.Olympiads.Year)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    p.Id,
                    Olympiad = p.Olympiads.Year + " " + p.Olympiads.Type,
                    Athlete = p.Athletes.FirstName + " " + p.Athletes.LastName,
                    Discipline = p.Disciplines.Name,
                    p.Result,
                    p.Medal
                })
                .ToList();

            ShowResult(result);
        }
    }
}