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
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Infecteer(y2,x2);
                break;
            case 1 when Math.Abs(x2 - x1) == 1 || Math.Abs(x2 - x1) == 0:
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Infecteer(y2,x2);
                break;
            case 2 when Math.Abs(x2 - x1) <= 2:
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Bord.Vakken[y1, x1].Type = Type.Leeg;
                Bord.Vakken[y1, x1].Waarde = "[ _ ]";
                Infecteer(y2,x2);
                break;
            case 0 when Math.Abs(x2 - x1) == 2:
                Bord.Vakken[y2, x2].Type = Bord.Vakken[y1, x1].Type;
                Bord.Vakken[y2, x2].Waarde = Bord.Vakken[y1, x1].Waarde;
                Bord.Vakken[y1, x1].Type = Type.Leeg;
                Bord.Vakken[y1, x1].Waarde = "[ _ ]";
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

            if (!IsGeldigeCoordinaat(newY, newX) || Bord.Vakken[newY, newX].Type != Type.BaggySweater) continue;
            Bord.Vakken[newY, newX].Type = Type.Hoodie;
            Bord.Vakken[newY, newX].Waarde = "[ H ]";
        }
    }
    
    private bool IsGeldigeCoordinaat(int y, int x) {
        return y >= 0 && y < Bord.Vakken.GetLength(0) && x >= 0 && x < Bord.Vakken.GetLength(1);
    }
}