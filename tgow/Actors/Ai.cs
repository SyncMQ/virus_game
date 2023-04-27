namespace tgow.Actors;

public class Ai : Speler {
    private readonly Random _random;

    public Ai(Type type, Bord bord) : base(type, bord) {
        _random = new Random();
    }

    public new void DoeZet() {
        throw new NotImplementedException();
    }
    
}