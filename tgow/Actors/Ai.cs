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
        
        
        // X% kans om een willekeurige zet te kiezen waarvan X de willekeurig variabel is dat bepaald wat de AI doet;
        // Y% kans om de beste zet te kiezen waarvan Y de kans is dat de AI de beste zet uitvoert. 
        if (_random.Next(0, 100) < 100) {
            if (besteStartVakje == null || besteDoelVakje == null) return;
            // Voer de zet met de hoogste score uit.
            DoeZet(besteStartVakje.Item1, besteStartVakje.Item2, besteDoelVakje.Item1, besteDoelVakje.Item2);
        } else {
            var willekeurigeBaggySweaterVakje = baggySweaterVakken[_random.Next(baggySweaterVakken.Count)];
            var mogelijkeZetten =
                BerekenMogelijkeZetten(willekeurigeBaggySweaterVakje.Item1, willekeurigeBaggySweaterVakje.Item2);
            if (mogelijkeZetten.Count <= 0) return;
            var willekeurigeZet = mogelijkeZetten[_random.Next(mogelijkeZetten.Count)];
            DoeZet(willekeurigeBaggySweaterVakje.Item1, willekeurigeBaggySweaterVakje.Item2, willekeurigeZet.Item1,
                willekeurigeZet.Item2);
        }
    }

    private void DoeZet(int y1, int x1, int y2, int x2) {
        switch (Math.Abs(y2 - y1)) {
            case 0 when Math.Abs(x2 - x1) == 1:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 1 || Math.Abs(x2 - x1) == 0:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 2:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                _bord.Vakken[y1, x1].Type = Type.Leeg;
                _bord.Vakken[y1, x1].Waarde = "[ _ ]";
                Infecteer(y2,x2);
                break;
            case 2 when Math.Abs(x2 - x1) <= 2:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                _bord.Vakken[y1, x1].Type = Type.Leeg;
                _bord.Vakken[y1, x1].Waarde = "[ _ ]";
                Infecteer(y2,x2);
                break;
            case 0 when Math.Abs(x2 - x1) == 2:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                _bord.Vakken[y1, x1].Type = Type.Leeg;
                _bord.Vakken[y1, x1].Waarde = "[ _ ]";
                Infecteer(y2,x2);
                break;
            default:
                Console.WriteLine(".");
                break;
        }
    }

    private List<Tuple<int, int>> VindBaggySweaterVakken() {
        var baggySweaterVakken = new List<Tuple<int, int>>();

        for (var y = 0; y < _bord.Vakken.GetLength(0); y++) {
            for (var x = 0; x < _bord.Vakken.GetLength(1); x++) {
                if (_bord.Vakken[y, x].Type == Type.BaggySweater) {
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

            if (!IsGeldigeCoordinaat(newY, newX) || _bord.Vakken[newY, newX].Type != Type.Hoodie) continue;
            _bord.Vakken[newY, newX].Type = Type.BaggySweater;
            _bord.Vakken[newY, newX].Waarde = "[ B ]";
        }
    }

    private int BerekenZetScore(int y1, int x1, int y2, int x2) {
        var score = 0;

        // Tijdelijke zet wordt later weer naar leeg gegooid
        _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
        _bord.Vakken[y1, x1].Type = Type.Leeg;

        // Controleer gevaarlijke en veilige zetten
        int[] dy = {-1, -1, -1, 0, 1, 1, 1, 0};
        int[] dx = {-1, 0, 1, 1, 1, 0, -1, -1};

        for (var i = 0; i < 8; i++) {
            var newY = y2 + dy[i];
            var newX = x2 + dx[i];

            if (!IsGeldigeCoordinaat(newY, newX)) continue;

            if (_bord.Vakken[newY, newX].Type == Type.Hoodie)
                score -= 1; // Gevaarlijke zet
            else if (_bord.Vakken[newY, newX].Type == Type.Leeg) score += 1; // Veilige zet
        }

        // Maak de tijdelijke zet ongedaan
        _bord.Vakken[y1, x1].Type = _bord.Vakken[y2, x2].Type;
        _bord.Vakken[y2, x2].Type = Type.Leeg;

        return score;
    }

    private List<Tuple<int, int>> BerekenMogelijkeZetten(int y1, int x1) {
        var mogelijkeZetten = new List<Tuple<int, int>>();

        int[] dy = {-2, -2, -1, 1, 2, 2, 1, -1};
        int[] dx = {-1, 1, 2, 2, 1, -1, -2, -2};

        for (var i = 0; i < 8; i++) {
            var newY = y1 + dy[i];
            var newX = x1 + dx[i];

            if (IsGeldigeCoordinaat(newY, newX) && _bord.Vakken[newY, newX].Type is Type.Leeg) {
                mogelijkeZetten.Add(new Tuple<int, int>(newY, newX));
            }
        }

        return mogelijkeZetten;
    }
    
    private bool IsGeldigeCoordinaat(int y, int x) {
        return y >= 0 && y < _bord.Vakken.GetLength(0) && x >= 0 && x < _bord.Vakken.GetLength(1);
    }
}