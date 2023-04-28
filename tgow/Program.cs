using tgow.Actors;

namespace tgow; 

public static class Program {
    public static void Main() {
        var spel = new Spel();
        spel.InitialiseerSpel();

        while (!spel.IsSpelVoorbij) {
            spel.SpeelBeurt();
            spel.CheckSpelStatus();
        }

        var spelerTotaal = 0;
        var aiTotaal = 0;
        
        foreach (var vak in spel.Bord.Vakken) {
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
        Console.WriteLine(spelerTotaal > aiTotaal
            ? "Speler 1 wint!"
            : "Computer wint!");
    }
}