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
        Bord.InitialiseerSpelers("[ H ]" ,  "[ B ]");
    }
    
    public void SpelStaat() {
        var beurtVoorbij = false;
        var selectieRij = "";
        var selectieKolom = "";
        var selectieRij2 = "";
        var selectieKolom2 = "";


        while (!beurtVoorbij) {
            Console.Clear();

            Bord.HuidigBord();
            if (SpelerEenBeurt && !beurtVoorbij) {
                Console.WriteLine("Speler 1, kies een [ H ]oodie vak.");
                Console.WriteLine("Rij: ");
                selectieRij = Console.ReadLine();
                Console.WriteLine("Kolom: ");
                selectieKolom = Console.ReadLine();
                try {
                    if (!IsValideSelectie(Convert.ToInt32(selectieRij), Convert.ToInt32(selectieKolom))) continue;
                    var IsKeuzeValide = false;
                    while (!IsKeuzeValide) {
                        Console.Clear();
                        Bord.HuidigBord();
                        Console.WriteLine("Kies een positie om je hoodie naartoe te verplaatsen.");
                        Console.WriteLine("Rij: ");
                        selectieRij2 = Console.ReadLine();
                        Console.WriteLine("Kolom: ");
                        selectieKolom2 = Console.ReadLine();
                        try {
                            if (!IsLegeSelectie(Convert.ToInt32(selectieRij2), Convert.ToInt32(selectieKolom2))) continue;
                        }
                        catch (FormatException) {
                            continue;
                        }
                        IsKeuzeValide = true;
                    }
                }
                catch (FormatException) {}
            }
            else {
                
                Console.WriteLine("Computer maakt een zet...");
            }
        }
    }

    public bool WinConditie() {
        return false;
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

    public bool IsGeldigPad(int startRij, int startKolom, int eindRij, int eindKolom) {
        var afstandRij = Math.Abs(eindRij - startRij);
        var afstandKolom = Math.Abs(eindKolom - startKolom);
    
        if (afstandRij > 2 || afstandKolom > 2 || afstandRij + afstandKolom != 3) {
            return false;
        }
    
        if (afstandRij == 2 || afstandKolom == 2) {
            Bord.Vakken[startRij, startKolom].Type = Type.Leeg;
        } else {
            var nieuwRij = (startRij + eindRij) / 2;
            var nieuwKolom = (startKolom + eindKolom) / 2;
            Bord.Vakken[nieuwRij, nieuwKolom].Type = Bord.Vakken[startRij, startKolom].Type;
        }
        Bord.Vakken[eindRij, eindKolom].Type = Bord.Vakken[startRij, startKolom].Type;
        Bord.Vakken[startRij, startKolom].Type = Type.Leeg;
    
        return true;
    }
    

}