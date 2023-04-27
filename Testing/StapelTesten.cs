using DataStructures;

namespace Testing;

public class StapelTesten {
    [Fact]
    public void Test_Duw_Voegt_Element_Toe() {
        // Klaarzetten
        var stapel = new Stapel<int>();

        // Actie
        stapel.Duw(1);

        // Check
        Assert.Equal(1, stapel.PakLength());
    }

    [Fact]
    public void Test_Pak_Haalt_Laatst_Toegevoegd_Element_Uit_Stapel() {
        // Klaarzetten
        var stapel = new Stapel<string>();
        stapel.Duw("Eerste element");
        stapel.Duw("Tweede element");

        // Actie
        var resultaat = stapel.Pak();

        // Check
        Assert.Equal("Tweede element", resultaat);
    }

    [Fact]
    public void Test_PakLength_Geeft_Lengte_Stapel_Terug() {
        // Klaarzetten
        var stapel = new Stapel<char>();
        stapel.Duw('A');
        stapel.Duw('B');
        stapel.Duw('C');

        // Actie
        var resultaat = stapel.PakLength();

        // Check
        Assert.Equal(3, resultaat);
    }
}