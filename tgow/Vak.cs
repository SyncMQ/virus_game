namespace tgow; 

public class Vak {
    public string Waarde { get; set; }
    public Type Type { get; set; }
    
    public Vak(string waarde, Type type) {
        Waarde = waarde;
        Type = type;
    }
    
    public string GetWaarde() {
        return Type == Type.Zijkant ? $"[ {Waarde} ]" : Waarde;
    }

    
}