using DataStructures;
using tgow.Actors;

namespace tgow; 

public class Spel {
    public bool IsSpelVoorbij => false;
    private Bord Bord { get; }
    public Stapel<Bord> HuidigBordStaat = new Stapel<Bord>();
    private bool SpelerEenBeurt { get; set; } = true;
    public Speler[] Spelers = new Speler[2];

    public Spel() {
        Bord = new Bord(7,7);
    }

    public void InitialiseerSpel() {
        Bord.InitialiseerBord();
        Bord.InitialiseerSpelers("w" ,  "[ B ]");
        Spelers[0] = new Mens(Type.Hoodie, Bord);
        Spelers[1] = new Ai(Type.BaggySweater,Bord);
    }
    
    public void SpeelBeurt() {
        var beurtVoorbij = false;
        while (!beurtVoorbij) {
            //Speler beurt
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
                    }
                }
                catch (FormatException) {}
            }
            else {
                //Computer beurt
                ((Ai)Spelers[1]).DoeZet();
                Console.WriteLine("Computer maakt een zet...");
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

    public bool IsGeldigPad(int startRij, int startKolom, int eindRij, int eindKolom)
    {
        var afstandRij = Math.Abs(eindRij - startRij);
        var afstandKolom = Math.Abs(eindKolom - startKolom);

        if (afstandRij > 2 || afstandKolom > 2 || afstandRij + afstandKolom != 3) {
            return false;
        }

        if (afstandRij == 2 || afstandKolom == 2) {
            Bord.Vakken[startRij, startKolom].Type = Type.Leeg;
        }
        else {
            var nieuwRij = (startRij + eindRij) / 2;
            var nieuwKolom = (startKolom + eindKolom) / 2;
            Bord.Vakken[nieuwRij, nieuwKolom].Type = Bord.Vakken[startRij, startKolom].Type;
        }

        Bord.Vakken[eindRij, eindKolom].Type = Bord.Vakken[startRij, startKolom].Type;
        Bord.Vakken[startRij, startKolom].Type = Type.Leeg;

        return true;
    }
}