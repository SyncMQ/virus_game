using tgow.Actors;

namespace tgow; 

public static class Program {
    public static void Main() {
        var spel = new Spel();
        spel.InitialiseerSpel();

        while (!spel.IsSpelVoorbij) {
            spel.SpeelBeurt();
        }

        Console.WriteLine(spel.Spelers[0].TotaalBezet > spel.Spelers[1].TotaalBezet
            ? "Speler 1 wint!"
            : "Computer wint!");
    }
}