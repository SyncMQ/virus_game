namespace tgow; 

public static class Program {
    public static void Main() {
        var spel = new Spel();
        spel.InitialiseerSpel();

        while (!spel.IsSpelVoorbij) {
            spel.SpelStaat();
        }
    }
}