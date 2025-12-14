using System.IO.Compression;
using System.Net.NetworkInformation;

namespace tgow; 

public class Bord : ICloneable {
    private readonly int _rij;
    private readonly int _kolom;
    public Vak[,] Vakken { get; }

    public Bord(int rij, int kolom) {
        _rij = rij+1;
        _kolom = kolom+1;
        Vakken = new Vak[_rij,_kolom];
    }

    public void HuidigBord() {
        for (var y = 0; y < _rij; y++) {
            var rij = "";
            for (var x = 0; x < _kolom; x++) {
                rij += Vakken[y, x].GetWaarde();
            }
            Console.WriteLine(rij);
        }
    }
    
    public void InitialiseerBord() {
        Console.Clear();
        
        for (var rij = 0; rij < _rij; rij++) {
            Vakken[rij, 0] = new Vak(rij.ToString(), Type.Zijkant);
            for (var kolom = 1; kolom < _rij; kolom++) {
                if (rij == 0) {
                    Vakken[rij, kolom] = new Vak(kolom.ToString(), Type.Zijkant);
                }
                else {
                    Vakken[rij, kolom] = new Vak("[ _ ]", Type.Leeg);
                }
            }
        }
    }

    public void InitialiseerSpelers(string spelerEen, string spelerTwee) {
        //editor notitie: speler 1 is linksonder & Speler 2 is rechtsboven in het spel*
        for (var rij = _rij - 1; rij >= _rij - 2; rij--) {
            for (var kolom = 1; kolom < 3; kolom++) {
                Vakken[rij, kolom].Waarde = spelerEen;
                Vakken[rij, kolom].Type = Type.Hoodie;
            }
        }
        for (var rij = 1; rij < 3; rij++) {
            for (var kolom = _kolom - 1; kolom >= _kolom - 2; kolom--) {
                Vakken[rij, kolom].Waarde = spelerTwee;
                Vakken[rij, kolom].Type = Type.BaggySweater;
            }
        }
    }

    public bool HeeftLegeVakjes() {
        for (var rij = 1; rij < _rij; rij++) {
            for (var kolom = 1; kolom < _kolom; kolom++) {
                if (Vakken[rij, kolom].Type == Type.Leeg) {
                    return true;
                }
            }
        }
        return false;
    }
    
    public object Clone() {
        var kopie = new Bord(_rij - 1, _kolom - 1);
        for (var i = 0; i < _rij; i++) {
            for (var j = 0; j < _kolom; j++) {
                kopie.Vakken[i, j] = (Vak)Vakken[i, j].Clone();
            }
        }
        return kopie;
    }
}
