
class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine(Days.Sunday);
        Console.WriteLine((int)Days.Saturday);
        Console.WriteLine((int)Days.Sunday); 

        int tempature = 32;
        if (tempature <= (int)WheatherTempature.Cold)
            Console.WriteLine("Please wait for tempature its still cold.");
        else if (tempature >= (int)WheatherTempature.Normal)
            Console.WriteLine("It's so warm today");
        else if (tempature >= (int)WheatherTempature.Normal && tempature < (int)WheatherTempature.VeryHot)
            Console.WriteLine("Let's go outside! It's very hot today.");

    }

    enum Days
    {
        Monday = 1, 
        Tuesday,
        Wednesday, 
        Thursday, 
        Friday,
        Saturday,
        Sunday 
    }
}

enum WheatherTempature
{
    Cold = 5,
    Normal = 20,
    Hot = 25,
    VeryHot = 30
}