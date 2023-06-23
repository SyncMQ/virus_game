namespace tgow.Actors;

public abstract class Speler {
    public Type Type { get; set; }
    protected readonly Bord _bord;
    

    protected Speler(Type type, Bord bord) {
        Type = type;
        _bord = bord;
    }
}