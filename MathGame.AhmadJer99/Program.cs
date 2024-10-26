using System.ComponentModel;

string SessionHistory = "";
string difficultyLevel = "easy";
int availableOptions = 7;
ShowMenu();

void ShowMenu()
{
    bool shouldExit = default;
    string userEntry = "";


    Console.Clear();
    Console.WriteLine("Enter an option of the game you want to play (or enter exit to quit the app):");
    do
    {

        Console.WriteLine("1)Summation");
        Console.WriteLine("2)Subtraction");
        Console.WriteLine("3)Division");
        Console.WriteLine("4)Multiplication");
        Console.WriteLine("5)Show Session History");
        Console.WriteLine("6)Change Difficulty Level");
        Console.WriteLine("7)Random Game");

        try
        {
            userEntry = GetUserInput(availableOptions);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Enter an option again (1 through {availableOptions}) (or exit to leave): ");
            continue;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Enter an option again (1 through {availableOptions}) (or exit to leave): ");
            continue;
        }
        if (userEntry == "exit")
            shouldExit = true;
        else
        {
            string gameResult = "";
            switch (userEntry)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("(+) The Summation Game");
                    gameResult = SummationGame(difficultyLevel);
                    Console.WriteLine(gameResult);
                    LogResult(gameResult);
                    Console.ReadLine();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("(-) The Subtraction Game");
                    gameResult = SubtractionGame(difficultyLevel);
                    Console.WriteLine(gameResult);
                    LogResult(gameResult);
                    Console.ReadLine();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("(/) The Division Game");
                    gameResult = DivisionGame(difficultyLevel);
                    Console.WriteLine(gameResult);
                    LogResult(gameResult);
                    Console.ReadLine();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("(X) The Multiplication Game");
                    gameResult = MultiplicationGame(difficultyLevel);
                    Console.WriteLine(gameResult);
                    LogResult(gameResult);
                    Console.ReadLine();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Session History");
                    ShowSessionHistory();
                    Console.ReadLine();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Available Difficulty Levels:");
                    Console.WriteLine("1)Easy\nYou have more tries and the questions are simple");
                    Console.WriteLine("2)Medium\nYou less tries and the questions have a bit of taste from hard!");
                    Console.WriteLine("3)Math Genius\nYou have 1 try only and the questions are pretty hard");
                    Console.WriteLine("4)Back");
                    ChangeDifficulty();
                    Console.ReadLine();
                    break;
                case "7":
                    RandomGame();
                    break;
            }
        }
    }
    while (!shouldExit);
}

string GetUserInput(int optionsNumber = 4, bool numericValueOnly = false)
{
    string userInput = "";
    string? readResult = default;
    int numericUserInput = default;

    readResult = Console.ReadLine();
    if (readResult != null)
        userInput = readResult.Trim().ToLower();

    if (!int.TryParse(userInput, out numericUserInput))
    {
        if (numericValueOnly)
        {
            throw new FormatException($"Error Ocurred: Expected (1-{optionsNumber}) input value yet recieved string input.");
        }
        if (userInput != "exit")
            throw new ArgumentException("Error Ocurred: String expected is \"Exit\" but recieved otherwise.");
        else
            return userInput;
    }

    else
    {
        if (numericUserInput < 0 || numericUserInput > optionsNumber)
            throw new ArgumentOutOfRangeException("Error Ocurred: user entered an invalid menu option");
        else
            return userInput;
    }

}

void ChangeDifficulty()
{
    int optionsNumber = 4;
    string userDifficultyChoice = "1";
    string[] difficultyLevels = ["easy", "medium", "math genius"];
    bool validEntry = default;
    do
    {
        try
        {
            userDifficultyChoice = GetUserInput(optionsNumber, true);
            validEntry = true;
        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Enter an option again (1 through {optionsNumber})");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Enter an option again (1 through {optionsNumber})");
        }
    }
    while (!validEntry);

    switch (userDifficultyChoice)
    {
        case "1":
            difficultyLevel = difficultyLevels[0];
            Console.WriteLine("Changing Difficulty...");
            break;
        case "2":
            difficultyLevel = difficultyLevels[1];
            Console.WriteLine("Changing Difficulty...");
            break;
        case "3":
            difficultyLevel = difficultyLevels[2];
            Console.WriteLine("Changing Difficulty...");
            break;
        case "4":
            break;

    }

}

int[] SetMagicNums(int[] magicNums, int lowerBound = 0, int upperBound = 101, bool integerQuotientOnly = false)
{
    Random random = new Random();
    if (integerQuotientOnly)
    {
        bool exit = default;
        int randomNum;
        int randomNum2;
        do
        {
            randomNum = random.Next(lowerBound, upperBound);
            randomNum2 = random.Next(lowerBound, upperBound);
            if (randomNum % randomNum2 == 0)
            {
                magicNums[0] = randomNum;
                magicNums[1] = randomNum2;
                exit = true;
            }
        }
        while (!exit);
    }
    else
    {
        for (int i = 0; i < magicNums.Length; i++)
            magicNums[i] = random.Next(lowerBound, upperBound);
    }
    return magicNums;
}

void LogResult(string CurrentGameResult)
{
    SessionHistory += CurrentGameResult;
}

void ShowSessionHistory()
{
    string[] sessionHistoryFormatted = SessionHistory.Split('.');
    foreach (string session in sessionHistoryFormatted)
    {
        Console.WriteLine(session);
    }
    Console.WriteLine();
}

void RandomGame(string difficulty = "easy")
{
    string? userEntry = default;
    string gameResult;
    bool shouldExit = default;
    Random random = new Random();

    Console.Clear();
    Console.WriteLine("Randomizer!!!\n you will be presented with random questions from random games with random difficulties ranging from easy to a math genius !\n(Press Enter to continue.)");
    Console.ReadLine();

    do
    {
        int gameType = random.Next(1, 5);
        int gameDifficulty = random.Next(1, 4);

        switch (gameDifficulty)
        {
            case 1:
                difficultyLevel = "easy";
                break;
            case 2:
                difficultyLevel = "medium";
                break;
            case 3:
                difficultyLevel = "math genius";
                break;

        }

        switch (gameType)
        {
            case 1:
                Console.Clear();
                gameResult = SummationGame(difficultyLevel);
                Console.WriteLine(gameResult);
                LogResult(gameResult);
                Console.WriteLine("Press Enter to go to next question (or enter \"exit\" to leave)");
                userEntry = Console.ReadLine();
                if (userEntry != null)
                    if (userEntry.Trim().ToLower() == "exit")
                    {
                        shouldExit = true;
                        continue;
                    }

                break;
            case 2:
                Console.Clear();
                gameResult = SubtractionGame(difficultyLevel);
                Console.WriteLine(gameResult);
                LogResult(gameResult);
                Console.WriteLine("Press Enter to go to next question (or enter \"exit\" to leave)");
                userEntry = Console.ReadLine();
                if (userEntry != null)
                    if (userEntry.Trim().ToLower() == "exit")
                    {
                        shouldExit = true;
                        continue;
                    }
                break;
            case 3:
                Console.Clear();
                gameResult = DivisionGame(difficultyLevel);
                Console.WriteLine(gameResult);
                LogResult(gameResult);
                Console.WriteLine("Press Enter to go to next question (or enter \"exit\" to leave)");
                userEntry = Console.ReadLine();
                if (userEntry != null)
                    if (userEntry.Trim().ToLower() == "exit")
                    {
                        shouldExit = true;
                        continue;
                    }
                break;
            case 4:
                Console.Clear();
                gameResult = MultiplicationGame(difficultyLevel);
                Console.WriteLine(gameResult);
                LogResult(gameResult);
                Console.WriteLine("Press Enter to go to next question (or enter \"exit\" to leave)");
                userEntry = Console.ReadLine();
                if (userEntry != null)
                    if (userEntry.Trim().ToLower() == "exit")
                    {
                        shouldExit = true;
                        continue;
                    }
                break;

        }


    }
    while (!shouldExit);
}

string SummationGame(string difficulty = "easy")
{
    int timer;
    string gameResult = "";
    int[] magicNums = new int[2];

    switch (difficulty)
    {
        case "easy":
            timer = 3;
            SetMagicNums(magicNums, 0, 25);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Summation);
            break;
        case "medium":
            timer = 2;
            SetMagicNums(magicNums, 25, 70);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Summation);
            break;
        case "math genius":
            timer = 1;
            SetMagicNums(magicNums, 70, 101);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Summation);
            break;
        default:
            Console.WriteLine("Other difficulties will be added later, please come back later, ty.");
            break;
    }
    return gameResult;
}

string SubtractionGame(string difficulty = "easy")
{
    int timer;
    string gameResult = "";
    int[] magicNums = new int[2];

    switch (difficulty)
    {
        case "easy":
            timer = 3;
            SetMagicNums(magicNums, 0, 25);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Subtraction);

            break;
        case "medium":
            timer = 2;
            SetMagicNums(magicNums, 25, 70);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Subtraction);
            break;
        case "math genius":
            timer = 1;
            SetMagicNums(magicNums, 70, 101);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Subtraction);
            break;
        default:
            Console.WriteLine("Other difficulties will be added later, please come back later, ty.");
            break;
    }
    return gameResult;
}

string DivisionGame(string difficulty = "easy")
{

    int timer; // 3 tries only for easy mode!
    string gameResult = "";
    int[] magicNums = new int[2];

    switch (difficulty)
    {
        case "easy":
            timer = 3;
            SetMagicNums(magicNums, 1, 25, true);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Division);
            break;
        case "medium":
            timer = 2;
            SetMagicNums(magicNums, 25, 60, true);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Division);
            break;
        case "math genius":
            timer = 1;
            SetMagicNums(magicNums, 60, 101, true);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Division);
            break;
        default:
            Console.WriteLine("Other difficulties will be added later, please come back later, ty.");
            break;
    }
    return gameResult;
}

string MultiplicationGame(string difficulty = "easy")
{
    int timer;
    string gameResult = "";
    int[] magicNums = new int[2];

    switch (difficulty)
    {
        case "easy":
            timer = 3;
            SetMagicNums(magicNums, 1, 25);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Multiplication);

            break;
        case "medium":
            timer = 2;
            SetMagicNums(magicNums, 25, 70);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Multiplication);
            break;
        case "math genius":
            timer = 1;
            SetMagicNums(magicNums, 70);
            gameResult = GameLogic(magicNums[0], magicNums[1], timer, GameMode.Multiplication);
            break;
        default:
            Console.WriteLine("Other difficulties will be added later, please come back later, ty.");
            break;
    }
    return gameResult;
}

string GameLogic(int magicNum1, int magicNum2, int numOfTries, GameMode gameMode)
{
    string? readResult = default;
    bool exitGame = default;
    int tries = numOfTries;
    int userAnswer;

    string gameResult = "";

    switch (gameMode)
    {
        case GameMode.Summation:
            do
            {
                Console.Clear();
                Console.WriteLine($"{magicNum1} + {magicNum2} = ??");
                readResult = Console.ReadLine();
                Console.WriteLine();

                if (readResult != null)
                {
                    if (int.TryParse(readResult, out userAnswer))
                    {
                        tries--;
                        if (userAnswer == magicNum1 + magicNum2)
                        {
                            gameResult += "Win -> WP!";
                            exitGame = true;
                            continue;
                        }

                    }
                    else
                    {
                        tries--;
                    }
                }
                if (tries == 0)
                {
                    gameResult += "Lost -> No more tries left!";
                    exitGame = true;
                }
            }
            while (!exitGame);
            return $"{magicNum1} + {magicNum2} = {magicNum1 + magicNum2} => You {gameResult}.";

        case GameMode.Subtraction:
            do
            {
                Console.Clear();
                Console.WriteLine($"{magicNum1} - {magicNum2} = ??");
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    if (int.TryParse(readResult, out userAnswer))
                    {
                        tries--;
                        if (userAnswer == magicNum1 - magicNum2)
                        {
                            gameResult += "Win -> WP!";
                            exitGame = true;
                            continue;
                        }
                    }
                    else
                    {
                        tries--;
                    }
                }
                if (tries == 0)
                {
                    gameResult += "Lost -> No more tries left!";
                    exitGame = true;
                }
            }
            while (!exitGame);
            return $"{magicNum1} - {magicNum2} = {magicNum1 - magicNum2} => You {gameResult}.";

        case GameMode.Division:
            do
            {
                Console.Clear();
                Console.WriteLine($"{magicNum1} / {magicNum2} = ??");
                readResult = Console.ReadLine();
                Console.WriteLine();

                if (readResult != null)
                {
                    if (int.TryParse(readResult, out userAnswer))
                    {
                        tries--;
                        if (userAnswer == magicNum1 / magicNum2)
                        {
                            gameResult += "Win -> WP!";
                            exitGame = true;
                            continue;
                        }

                    }
                    else
                    {
                        tries--;
                    }
                }
                if (tries == 0)
                {
                    gameResult += "Lost -> No more tries left!";
                    exitGame = true;
                }

            }
            while (!exitGame);
            return $"{magicNum1} / {magicNum2} = {magicNum1 / magicNum2} => You {gameResult}.";

        case GameMode.Multiplication:
            do
            {
                Console.Clear();
                Console.Write($"{magicNum1} X {magicNum2} = ??");
                readResult = Console.ReadLine();
                Console.WriteLine();

                if (readResult != null)
                {
                    if (int.TryParse(readResult, out userAnswer))
                    {
                        tries--;
                        if (userAnswer == magicNum1 * magicNum2)
                        {
                            gameResult += "Win -> WP!";
                            exitGame = true;
                            continue;
                        }
                    }
                    else
                    {
                        tries--;
                    }
                }
                if (tries == 0)
                {
                    gameResult += "Lost -> No more tries left!";
                    exitGame = true;
                }
            }
            while (!exitGame);
            return $"{magicNum1} * {magicNum2} = {magicNum1 * magicNum2} => You {gameResult}.";
        default:
            // Handle unexpected gameMode values
            return "Invalid game mode selected.";
    }
}

internal enum GameMode
{
    Summation,
    Subtraction,
    Division,
    Multiplication
}