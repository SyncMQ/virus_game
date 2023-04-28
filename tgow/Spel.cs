using DataStructures;
using tgow.Actors;

namespace tgow; 

public class Spel {
    public bool IsSpelVoorbij { get; private set; } = false;
    public Bord Bord { get; }
    public Stapel<Bord> HuidigBordStaat = new Stapel<Bord>();
    private bool SpelerEenBeurt { get; set; } = true;
    public Speler[] Spelers = new Speler[2];

    public Spel() {
        Bord = new Bord(7,7);
    }

    public void InitialiseerSpel() {
        Bord.InitialiseerBord();
        Bord.InitialiseerSpelers("[ H ]" ,  "[ B ]");
        Spelers[0] = new Mens(Type.Hoodie, Bord);
        Spelers[1] = new Ai(Type.BaggySweater,Bord);
    }
    
    public void SpeelBeurt() {
        var beurtVoorbij = false;
        while (!beurtVoorbij && !IsSpelVoorbij) {
            Console.Clear();

            Bord.HuidigBord();
            if (SpelerEenBeurt && !beurtVoorbij) {
                Console.WriteLine("Speler 1, kies een [ H ]oodie vak.");
                Console.WriteLine("Rij: ");
                var selectieRij = Console.ReadLine();
                Console.WriteLine("Kolom: ");
                var selectieKolom = Console.ReadLine();
                try {
                    if (!IsValideSelectie(Convert.ToInt32(selectieRij), Convert.ToInt32(selectieKolom))) continue;
                    var IsKeuzeValide = false;
                    while (!IsKeuzeValide) {
                        Console.Clear();
                        Bord.HuidigBord();
                        Console.WriteLine("Kies een positie om je hoodie naartoe te verplaatsen.");
                        Console.WriteLine("Rij: ");
                        var selectieRij2 = Console.ReadLine();
                        Console.WriteLine("Kolom: ");
                        var selectieKolom2 = Console.ReadLine();
                        try {
                            if (!IsLegeSelectie(Convert.ToInt32(selectieRij2), Convert.ToInt32(selectieKolom2))) continue;
                        }
                        catch (FormatException) {
                            continue;
                        }
                        IsKeuzeValide = true;
                        ((Mens)Spelers[0]).DoeZet(selectieRij, selectieKolom, selectieRij2, selectieKolom2);
                        beurtVoorbij = true;
                        SpelerEenBeurt = false;
                    }
                }
                catch (FormatException) {}
            } else {
                //Computer beurt
                ((Ai)Spelers[1]).BepaalZet();
                Console.WriteLine("Computer maakt een zet...");
                Thread.Sleep(1000);
                beurtVoorbij = true;
                SpelerEenBeurt = true;
            }
        }
    }
    
    private bool IsValideSelectie(int rij, int kolom) {
        try {
            return Bord.Vakken[rij, kolom].Type == Type.Hoodie;
        }
        catch (IndexOutOfRangeException) {
            return false;
        }
    }

    private bool IsLegeSelectie(int rij, int kolom) {
        try {
            return Bord.Vakken[rij, kolom].Type == Type.Leeg;
        }
        catch (IndexOutOfRangeException) {
            return false;
        }
    }
    
    public void CheckSpelStatus() {
        
        if (!Bord.HeeftLegeVakjes())
        {
            IsSpelVoorbij = true;
        }

        var spelerTotaal = 0;
        var aiTotaal = 0;

        foreach (var vak in Bord.Vakken) {
            switch (vak.Type) {
                case Type.Hoodie:
                    spelerTotaal++;
                    break;
                case Type.BaggySweater:
                    aiTotaal++;
                    break;
                case Type.Zijkant:
                    break;
                default:
                    break;
            }
        }

        if (spelerTotaal == 0 || aiTotaal == 0) {
            IsSpelVoorbij = true;
        }
    }
}