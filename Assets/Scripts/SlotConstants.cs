public static class SlotConstants
{
    // Layers
    public static int layerCount = 3; 

    // Reel symbols 
    public static string[] symbols = new string[] { "$", "%", "&" };

    // UI messages 
    public static string victoryText = "You have won a prize!";
    public static string defeatText = "You have lost your coins!";

    // Spin configuration 
    public static float spinSpeed = 2;
    public static int frameRate = 60; 

    // Todo: dynamically set the no. of symbols
    // Todo: dynamically create layers based on layer count 
    // Todo: increment layer count each round 
}
