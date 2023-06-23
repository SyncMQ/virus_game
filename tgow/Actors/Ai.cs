namespace tgow.Actors;

public class Ai : Speler {
    private readonly Random _random;

    public Ai(Type type, Bord bord) : base(type, bord) {
        _random = new Random();
    }

    public void BepaalZet() {
        var besteScore = int.MinValue;
        Tuple<int, int> besteStartVakje = null;
        Tuple<int, int> besteDoelVakje = null;

        var baggySweaterVakken = VindBaggySweaterVakken();

        foreach (var baggySweaterVakje in baggySweaterVakken) {
            var mogelijkeZetten = BerekenMogelijkeZetten(baggySweaterVakje.Item1, baggySweaterVakje.Item2);

            foreach (var mogelijkeZet in mogelijkeZetten) {
                var score = BerekenZetScore(baggySweaterVakje.Item1, baggySweaterVakje.Item2, mogelijkeZet.Item1,
                    mogelijkeZet.Item2);

                if (score <= besteScore) continue;
                besteScore = score;
                besteStartVakje = baggySweaterVakje;
                besteDoelVakje = mogelijkeZet;
            }
        }
        
        if (besteStartVakje == null || besteDoelVakje == null) return;
        // Voer de zet met de hoogste score uit.
        DoeZet(besteStartVakje.Item1, besteStartVakje.Item2, besteDoelVakje.Item1, besteDoelVakje.Item2);
    }

    private void DoeZet(int y1, int x1, int y2, int x2) {
        switch (Math.Abs(y2 - y1)) {
            case 0 when Math.Abs(x2 - x1) == 1: // Horizontale beweging van 1 vakje
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 1: // Diagonale beweging van 1 vakje
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Bord.Vakken[y1, x1].Type = Type.Leeg;
                Bord.Vakken[y1, x1].Waarde = "[ _ ]";
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 0: // Verticale beweging van 1 vakje
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 2: // Horizontale beweging van 2 vakjes
            case 2 when Math.Abs(x2 - x1) <= 2: // Verticale beweging van 2 vakjes of diagonale beweging van 2 vakjes
            case 0 when Math.Abs(x2 - x1) == 2: // Horizontale beweging van 2 vakjes
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Bord.Vakken[y1, x1].Type = Type.Leeg;
                Bord.Vakken[y1, x1].Waarde = "[ _ ]";
                Infecteer(y2,x2);
                break;
            default:
                Console.WriteLine(".");
                break;
        }
    }

    private List<Tuple<int, int>> VindBaggySweaterVakken() {
        var baggySweaterVakken = new List<Tuple<int, int>>();

        for (var y = 0; y < Bord.Vakken.GetLength(0); y++) {
            for (var x = 0; x < Bord.Vakken.GetLength(1); x++) {
                if (Bord.Vakken[y, x].Type == Type.BaggySweater) {
                    baggySweaterVakken.Add(new Tuple<int, int>(y, x));
                }
            }
        }

        return baggySweaterVakken;
    }
    
    private void Infecteer(int y2, int x2) {
        int[] dy = {-1, -1, -1, 0, 1, 1, 1, 0};
        int[] dx = {-1, 0, 1, 1, 1, 0, -1, -1};

        for (var i = 0; i < 8; i++) {
            var newY = y2 + dy[i];
            var newX = x2 + dx[i];

            if (!IsGeldigeCoordinaat(newY, newX) || Bord.Vakken[newY, newX].Type != Type.Hoodie) continue;
            Bord.Vakken[newY, newX].Type = Type.BaggySweater;
            Bord.Vakken[newY, newX].Waarde = "[ B ]";
        }
    }

    private int BerekenZetScore(int y1, int x1, int y2, int x2) {
        var score = 0;

        // Tijdelijke zet wordt gemaakt voor score evaluatie
        var origineelType = Bord.Vakken[y1, x1].Type;
        var origineleWaarde = Bord.Vakken[y1, x1].Waarde;

        // Voer de zet tijdelijk uit
        Bord.Vakken[y1, x1].Type = Type.Leeg;
        Bord.Vakken[y1, x1].Waarde = "[ _ ]";
        Bord.Vakken[y2, x2].Type = Type.BaggySweater;
        Bord.Vakken[y2, x2].Waarde = "[ B ]";

        // Simuleer de infectie na de zet
        int[] dy = {-1, -1, -1, 0, 1, 1, 1, 0};
        int[] dx = {-1, 0, 1, 1, 1, 0, -1, -1};

        for (var i = 0; i < 8; i++) {
            var newY = y2 + dy[i];
            var newX = x2 + dx[i];

            if (!IsGeldigeCoordinaat(newY, newX) || Bord.Vakken[newY, newX].Type != Type.Hoodie) continue;
            // Elk succesvol geïnfecteerd vakje voegt 10 punten toe aan de score
            score += 10;
        }

        // Zet terugdraaien
        Bord.Vakken[y1, x1].Type = origineelType;
        Bord.Vakken[y1, x1].Waarde = origineleWaarde;
        Bord.Vakken[y2, x2].Type = Type.Leeg;
        Bord.Vakken[y2, x2].Waarde = "[ _ ]";

        return score;
    }


    private List<Tuple<int, int>> BerekenMogelijkeZetten(int y1, int x1) {
        var mogelijkeZetten = new List<Tuple<int, int>>();

        int[] dy = {-2, -2, -1, 1, 2, 2, 1, -1};
        int[] dx = {-1, 1, 2, 2, 1, -1, -2, -2};

        for (var i = 0; i < 8; i++) {
            var newY = y1 + dy[i];
            var newX = x1 + dx[i];

            if (IsGeldigeCoordinaat(newY, newX) && Bord.Vakken[newY, newX].Type is Type.Leeg) {
                mogelijkeZetten.Add(new Tuple<int, int>(newY, newX));
            }
        }

        return mogelijkeZetten;
    }
    
    private bool IsGeldigeCoordinaat(int y, int x) {
        return y >= 0 && y < Bord.Vakken.GetLength(0) && x >= 0 && x < Bord.Vakken.GetLength(1);
    }
}