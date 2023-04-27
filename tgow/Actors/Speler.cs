namespace tgow.Actors;

public abstract class Speler {
    public Type Type { get; set; } 
    public int TotaalBezet { get; set; } = 4;
    protected readonly Bord _bord;
    

    protected Speler(Type type, Bord bord) {
        Type = type;
        _bord = bord;
    }
}