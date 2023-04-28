namespace tgow;

public class Zet {
    public int StartRij { get; }
    public int StartKolom { get; }
    public int EindRij { get; }
    public int EindKolom { get; }
    public int Score { get; set; }

    public Zet(int startRij, int startKolom, int eindRij, int eindKolom, int score = 0) {
        StartRij = startRij;
        StartKolom = startKolom;
        EindRij = eindRij;
        EindKolom = eindKolom;
        Score = score;
    }
}