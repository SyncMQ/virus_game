namespace tgow.Actors; 

public class Mens : Speler {
    public Mens(Type type, Bord bord) : base(type, bord) {}

    public void Undo() {
        
    }
    
    public void DoeZet(string sy1, string sx1, string sy2, string sx2) {
        var y1 = int.Parse(sy1);
        var x1 = int.Parse(sx1);
        var y2 = int.Parse(sy2);
        var x2 = int.Parse(sx2);

        switch (Math.Abs(y2 - y1)) {
            case 0 when Math.Abs(x2 - x1) == 1:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                TotaalBezet += 1;
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 1 || Math.Abs(x2 - x1) == 0:
                _bord.Vakken[y2, x2].Type = _bord.Vakken[y1, x1].Type;
                _bord.Vakken[y2, x2].Waarde = _bord.Vakken[y1, x1].Waarde;
                TotaalBezet += 1;
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
                break;
        }
    }

    private void Infecteer(int y2, int x2) {
        int[] dy = {-1, -1, -1, 0, 1, 1, 1, 0};
        int[] dx = {-1, 0, 1, 1, 1, 0, -1, -1};

        for (var i = 0; i < 8; i++) {
            var newY = y2 + dy[i];
            var newX = x2 + dx[i];

            if (!IsGeldigeCoordinaat(newY, newX) || _bord.Vakken[newY, newX].Type != Type.BaggySweater) continue;
            _bord.Vakken[newY, newX].Type = Type.Hoodie;
            _bord.Vakken[newY, newX].Waarde = "[ H ]";
        }
    }
    
    private bool IsGeldigeCoordinaat(int y, int x) {
        return y >= 0 && y < _bord.Vakken.GetLength(0) && x >= 0 && x < _bord.Vakken.GetLength(1);
    }
}