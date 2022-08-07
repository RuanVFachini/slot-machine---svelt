namespace Api.Dices;

public class DiceResult {
    public int Dice1Steps { get; set; }
    public int Dice2Steps { get; set; }
    public int Dice3Steps { get; set; }
    public bool Winner => GetRealPosition(Dice1Steps) == GetRealPosition(Dice2Steps) && GetRealPosition(Dice2Steps) == GetRealPosition(Dice3Steps);

    public DiceResult(int dice1Steps, int dice2Steps, int dice3Steps)
    {
        Dice1Steps = dice1Steps;
        Dice2Steps = dice2Steps;
        Dice3Steps = dice3Steps;
    }

    public int GetRealPosition(int dice) => dice % 360;
}