namespace tgow.Actors;

public abstract class Speler {
    public Type Type { get; set; }
    public Bord Bord { get; set; }
    

    protected Speler(Type type, Bord bord) {
        Type = type;
        Bord = bord;
    }
}