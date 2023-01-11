#region var
double parkingTimeMinutes = 0;
int centsEntered = 0;
string input = "";
bool finished = false;
const string WELCOMEMESSAGE = "Parkscheinautomat mit Mindestparkdauer 30min und Höchstparkdauer 1:30h\r\nTarif pro Stunde : 1€\r\nZulässige Münzen: 5C, 10C, 20C, 50C, 1€, 2€\r\nParkschein drucken mit d oder D\r\n";
const string PARKDAUER = "Sie dürfen {0}:{1}h parken.";
const string PARKZEIT_BISHER = "Parkzeit bisher: {0}:{1}h";
#endregion

Console.Clear();
Console.OutputEncoding = System.Text.Encoding.Default;
PrintWelcome();
do
{
    PrintParkingTime();
    input = EnterCoins();
    if (input != "d")
    {
        AddParkingTime(int.Parse(input));
    }
    if ((input == "d" && centsEntered >= 50) || centsEntered >= 150)
    {
        PrintDonation();
    }
    else { Console.WriteLine($"Mindesteinwurf 50C, bisher haben Sie {centsEntered}C eingeworfen\r\n"); }
} while (finished == false);

void PrintWelcome()
{
    Console.WriteLine(WELCOMEMESSAGE);
}

string EnterCoins()
{
    string answer = "";
    do
    {
        Console.Write("Ihre Eingabe: ");
        answer = Console.ReadLine()!.ToLower();
    } while (answer != "5" && answer != "10" && answer != "20" && answer != "50" && answer != "1" && answer != "2" && answer != "d");
    return answer;
}

void AddParkingTime(int coinInput)
{
    if (coinInput == 1 || coinInput == 2) { centsEntered += coinInput * 100; }
    else { centsEntered += coinInput; }
}

void PrintParkingTime()
{
    parkingTimeMinutes = 0.6 * centsEntered;
    double parkingHoursTemp = Math.Floor(parkingTimeMinutes / 60);
    double parkingMinutesTemp = parkingTimeMinutes - parkingHoursTemp * 60;
    if (parkingTimeMinutes <= 90)
    {
        Console.WriteLine(PARKZEIT_BISHER, parkingHoursTemp, parkingMinutesTemp);
    }
    else { Console.WriteLine(PARKZEIT_BISHER, "1", "30"); }
}

void PrintDonation()
{

    if (centsEntered <= 90)
    {
        double parkingHoursTemp = Math.Floor(parkingTimeMinutes / 60);
        double parkingMinutesTemp = parkingTimeMinutes - parkingHoursTemp * 60;
        Console.WriteLine(PARKDAUER, parkingHoursTemp, parkingMinutesTemp);
    }
    else
    {
        int donation = centsEntered - 150;
        double donationEuro = Math.Floor(donation / 100d);
        double donationCent = donation - donationEuro * 100;
        Console.WriteLine(PARKDAUER, "1", "30");
        Console.WriteLine($"Danke für Ihre Spende von {donationEuro} Euro {donationCent} Cent");
    }
    finished = true;
}